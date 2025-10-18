using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Opinio.API;
using Opinio.API.Filters;
using Opinio.API.Helper;
using Opinio.Core.Helpers;
using Opinio.Infrastructure.Data;
using Opinio.Infrastructure.Validators;
using Serilog;




var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables().AddConfiguration(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build());

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

builder.Host.UseSerilog(
    (context, services, loggerConfiguration) =>
    {
        loggerConfiguration.WriteTo.Console();
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
        loggerConfiguration.ReadFrom.Services(services);
    }
);
builder.Services.ConfigureApplicationServices(builder.Configuration);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
})
.AddJsonOptions(options =>
{
});

builder.Services.AddValidatorsFromAssemblyContaining<CategoryValidator>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder
    .Services.AddSwaggerGen(opts =>
    {
        //opts.CustomSchemaIds(opts => opts.FullName?.Replace("+", "."));
        //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //opts.IncludeXmlComments(xmlPath);
    })
    .AddEndpointsApiExplorer();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async ctx =>
            {
                var db = ctx.HttpContext.RequestServices.GetRequiredService<OpiniaDbContext>();

                var jti = ctx.Principal?.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                if (!string.IsNullOrEmpty(jti))
                {
                    var black = await db.JwtBlacklists.FirstOrDefaultAsync(b => b.Jti == jti, ctx.HttpContext.RequestAborted);
                    if (black is not null)
                    {
                        ctx.Fail("Token revoked");
                    }
                }
            },

            OnChallenge = async ctx =>
            {
                ctx.HandleResponse();

                var message =
                    !string.IsNullOrWhiteSpace(ctx.ErrorDescription) ? ctx.ErrorDescription :
                    ctx.AuthenticateFailure?.Message ?? "Unauthorized access.";

                var op = OperationResult<string>.Unauthorized(message);
                await OpResultWriter.WriteAsync(ctx.Response, op);
            },

            OnForbidden = async ctx =>
            {
                var op = OperationResult<string>.Forbidden("Forbidden access.");
                await OpResultWriter.WriteAsync(ctx.Response, op);
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddHealthChecks();
builder.Services.AddAuthorization();


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Opinia API v1"));
}



app.UseCors("AllowAll");

app.UseRouting();

app.MapHealthChecks("/health");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

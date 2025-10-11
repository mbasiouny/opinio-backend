using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Opinio.API;
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
{ })
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

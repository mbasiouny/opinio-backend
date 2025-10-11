using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Opinio.Infrastructure.Data;
using Opinio.Infrastructure.Extensions;
using Serilog;



namespace Opinio.API;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        var dbConnectionString = config.GetValue<string>("OpiniaDBConnection") ?? throw new Exception("DBConnection configuration not found");
        services.AddDbContext<OpiniaDbContext>(options => options.UseNpgsql(dbConnectionString).LogTo(log => Debug.WriteLine(log))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        services.AddInfrastructureServices();

        services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

        services.AddHttpContextAccessor();

        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
        services.AddTransient<Microsoft.Extensions.Logging.ILogger>(_ => _.GetRequiredService<ILogger<Program>>());

        return services;
    }
}

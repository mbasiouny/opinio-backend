namespace Opinio.API;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        //var dbConnectionString = config.GetValue<string>("TradingDBConnection") ?? throw new Exception("DBConnection configuration not found");
        //services.AddDbContext<TradingDbContext>(options => options.UseNpgsql(dbConnectionString).LogTo(log => Debug.WriteLine(log))
        //        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking))
        //        .AddScoped<BaseDbContext>(sp => sp.GetRequiredService<TradingDbContext>())


        //services.AddHttpContextAccessor();

        //Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
        //services.AddTransient<Microsoft.Extensions.Logging.ILogger>(_ => _.GetRequiredService<ILogger<Program>>());

        return services;
    }
}

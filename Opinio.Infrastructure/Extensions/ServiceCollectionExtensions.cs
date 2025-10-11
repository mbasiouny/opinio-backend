using Microsoft.Extensions.DependencyInjection;
using Opinio.Core.Repositories;
using Opinio.Core.Services;
using Opinio.Infrastructure.Data;
using Opinio.Infrastructure.Services;

namespace Opinio.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}

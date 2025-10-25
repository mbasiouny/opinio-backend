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
        services.AddScoped<IEntityRepository, EntityRepository>();
        services.AddScoped<IEntityService, EntityService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IReviewImageRepository, ReviewImageRepository>();
        services.AddScoped<IReviewService, ReviewService>();

        return services;
    }
}

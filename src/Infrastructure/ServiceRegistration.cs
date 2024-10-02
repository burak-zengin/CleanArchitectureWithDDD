using Domain.Products;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
    {
        serviceCollection.AddSingleton<DomainEventDispatcherInterceptor>();
        serviceCollection.AddScoped<IProductRepository, ProductRepository>();
        serviceCollection.AddDbContext<ProductDbContext>((serviceProvider, options) =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("PostgreSql"))
                .AddInterceptors(serviceProvider.GetRequiredService<DomainEventDispatcherInterceptor>());
        });
    }
}

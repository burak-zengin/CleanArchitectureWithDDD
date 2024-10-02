using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
    {
        var assembly = Assembly.GetExecutingAssembly();

        serviceCollection.AddAutoMapper(assembly);

        serviceCollection.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });
    }
}

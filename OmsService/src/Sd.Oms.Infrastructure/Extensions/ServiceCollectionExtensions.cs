using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sd.Oms.Core.Interfaces;
using Sd.Oms.Infrastructure.Repositories.Dish;
using Sd.Oms.Infrastructure.Repositories.Order;

namespace Sd.Oms.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDishRepository, DishRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
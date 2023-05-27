using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sd.Oms.Core.Interfaces;
using Sd.Oms.Core.Services;

namespace Sd.Oms.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
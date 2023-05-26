using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sd.Oms.Auth.Core.Interfaces;
using Sd.Oms.Auth.Core.Services;

namespace Sd.Oms.Auth.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}
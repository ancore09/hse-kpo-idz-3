using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sd.Oms.Auth.Core.Interfaces;
using Sd.Oms.Auth.Core.Services;
using Sd.Oms.Auth.Infrastructure.Repositories;

namespace Sd.Oms.Auth.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
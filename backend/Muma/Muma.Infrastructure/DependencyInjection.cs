using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muma.Application.Common;
using Muma.Infrastructure.Data.Persistence;

namespace Muma.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddMumaInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("MumaDB")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        return services;
    }
}

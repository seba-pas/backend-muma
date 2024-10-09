using Microsoft.Extensions.DependencyInjection;
using Muma.Application.Combos;
using Muma.Application.Mascotas;
using Muma.Application.Mascoteros;
using Muma.Application.Protectoras;
using Muma.Application.Usuarios;

namespace Muma.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddMumaApplication(this IServiceCollection services)
    {
        RegisterComboBoxes(services);
        RegisterServices(services);

        return services;
    }

    private static void RegisterComboBoxes(IServiceCollection services)
    {
        services.AddScoped<ComboService>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ProtectoraService>();
        services.AddScoped<MascoteroService>();
        services.AddScoped<UsuarioService>();
        services.AddScoped<MascotaService>();
    }
}

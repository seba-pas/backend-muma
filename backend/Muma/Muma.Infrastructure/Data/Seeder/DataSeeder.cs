using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Muma.Infrastructure.Data.Persistence;

namespace Muma.Infrastructure.Data.Seeder;

public static class DataSeeder
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        IServiceScope scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureCreated();

        await ExecuteCommand(context, SeedCommands.CreateTables);

        await ExecuteCommand(context, SeedCommands.SeedTipoRegistros);
        await ExecuteCommand(context, SeedCommands.SeedProvincias);
        await ExecuteCommand(context, SeedCommands.SeedCiudades);
    }

    private static async Task ExecuteCommand(ApplicationDbContext context, string command) 
        => await context.Database.ExecuteSqlRawAsync(command);
}

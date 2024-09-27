using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Muma.Application.Common;
using Muma.Domain.Entities;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace Muma.Infrastructure.Data.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<TipoRegistro> TipoRegistros { get; set; }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Protectora> Protectoras { get; set; }
    DatabaseFacade IApplicationDbContext.Database { get => base.Database; set { } }
}

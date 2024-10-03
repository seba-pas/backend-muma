using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Muma.Domain.Entities;

namespace Muma.Application.Common;

public interface IApplicationDbContext
{
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<TipoRegistro> TipoRegistros { get; set; }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Protectora> Protectoras { get; set; }
    public DbSet<Mascotero> Mascoteros { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    public DatabaseFacade Database { get; set; }
}

using Microsoft.EntityFrameworkCore;
using Muma.Application.Common;
using Muma.Application.Mascoteros.Dtos;
using Muma.Domain.Entities;

namespace Muma.Application.Mascoteros;

public sealed class MascoteroService(IApplicationDbContext context)
{
    public async Task<MascoteroDto?> GetMascotero(int id)
    {
        var mascotero = await context
            .Mascoteros
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (mascotero == null) { return null; }

        return MapToMascoteroDto(mascotero);
    }

    public async Task<IEnumerable<MascoteroDto>> GetAll()
    {
        var mascoteros = await context.Mascoteros
            .OrderBy(x => x.Nombre)
            .ToListAsync();

        return mascoteros
            .Select(MapToMascoteroDto)
            .ToList();
    }

    public async Task<OperationResult<MascoteroDto>> RegistrarMascotero(RegistrarMascoteroDto registroDto)
    {
        //Validaciones
        var result = new OperationResult<MascoteroDto>();

        #region validaciones

        var tipoRegistro = context.TipoRegistros.FirstOrDefault(x => x.Descripcion.Trim().ToLower() == "mascotero");


        if (tipoRegistro == null)
        {
            result.AddError("Error al obtener el tipo de registro 'Mascotero'");
        }

        //Email
        if (string.IsNullOrWhiteSpace(registroDto.Email))
        {
            result.AddError("El email es requerido");
        }
        else
        {
            if (context.Usuarios.Any(x => x.Email.Trim().ToLower() == registroDto.Email.Trim().ToLower()))
            {
                result.AddError("Ya existe un usuario registrado con esa dirección de email");
            }
        }

        //Password
        if (string.IsNullOrWhiteSpace(registroDto.Password))
        {
            result.AddError("El password es requerido");
        }
        else
        {
            //Validaciones password
        }

        if (string.IsNullOrWhiteSpace(registroDto.Nombre))
        {
            result.AddError("El nombre es requerido");
        }

        if (string.IsNullOrWhiteSpace(registroDto.Apellido))
        {
            result.AddError("El apellido es requerido");
        }

        #endregion

        if (!result.Success) return result;

        //Save data
        #region Savedata

        var t = context.Database.BeginTransaction();

        try
        {
            var userToAdd = new Usuario()
            {
                Email = registroDto.Email.Trim(),
                Password = registroDto.Password,
                Nombre = registroDto.Nombre.Trim(),
                Apellido = registroDto.Apellido.Trim(),
                IdTipoRegistro = tipoRegistro.Id
            };

            await context.Usuarios.AddAsync(userToAdd);
            await context.SaveChangesAsync(new CancellationToken());

            var mascoteroToAdd = new Mascotero()
            {
                UsuarioAsociadoId = userToAdd.Id,
                Nombre = (registroDto.Nombre ?? "").Trim(),
                Email = registroDto.Email.Trim(),
            };

            await context.Mascoteros.AddAsync(mascoteroToAdd);
            await context.SaveChangesAsync(new CancellationToken());

            t.Commit();

            var dto = MapToMascoteroDto(mascoteroToAdd);

            result.SetResult(dto);

        }
        catch (Exception e)
        {
            result.AddError(e.Message);
            t.Rollback();
            throw;
        }

        #endregion

        return result;

    }

    private static MascoteroDto MapToMascoteroDto(Mascotero m)
        => new MascoteroDto { Id = m.Id, Nombre = m.Nombre, Email = m.Email };
}

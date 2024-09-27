using Microsoft.EntityFrameworkCore;
using Muma.Application.Combos.Dtos;
using Muma.Application.Common;
using Muma.Application.Protectoras.Dtos;
using Muma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Application.Protectoras
{
    public class ProtectoraService(IApplicationDbContext context) 
    {
        public async Task<ProtectoraDto> GetProtectora(int idProtectora)
        {
            var protectora = await
                context.Protectoras.Where(x => x.Id == idProtectora)
                .Include(x => x.Ciudad).ThenInclude(c => c.Provincia)
                .FirstOrDefaultAsync();

            if (protectora == null) { return null; };

            var dto = ProtectoraMaps.MapToDto()(protectora);

            return dto;

        }

        public async Task<IEnumerable<ProtectoraDto>> GetAll()
        {
            var query =
                context.Protectoras
                .Include(p => p.Ciudad).ThenInclude(c => c.Provincia)
                .OrderBy(x => x.Nombre)
                .Select(ProtectoraMaps.MapToDto())
                .ToList();

            return await Task.FromResult(query);
                
        }

        public async Task<OperationResult<ProtectoraDto>> RegistrarProtectora(RegistroProtectoraDto input)
        {
            //Validaciones
            var result = new OperationResult<ProtectoraDto>();

            #region validaciones

            var tipoRegistro = context.TipoRegistros.FirstOrDefault(x => x.Descripcion.Trim().ToLower() == "protectora");


            if (tipoRegistro == null) 
            {
                result.AddError("Error al obtener el tipo de registro 'Protectora'");
            }

            //Email
            if (string.IsNullOrWhiteSpace(input.Email))
            {
                result.AddError("El email es requerido");
            }
            else
            {
                if (context.Usuarios.Any(x => x.Email.Trim().ToLower() == input.Email.Trim().ToLower()))
                {
                    result.AddError("Ya existe un usuario registrado con esa dirección de email");
                }
            }

            //Password
            if (string.IsNullOrWhiteSpace(input.Password))
            {
                result.AddError("El password es requerido");
            }
            else 
            {
                //Validaciones password
            }

            if (string.IsNullOrWhiteSpace(input.NombreUsuario))
            {
                result.AddError("Ingrese el nombre del usuario");
            }

            if (string.IsNullOrWhiteSpace(input.ApellidoUsuario))
            {
                result.AddError("Ingrese el apellido del usuario");
            }

            if (string.IsNullOrWhiteSpace(input.NombreProtectora)) 
            {
                result.AddError("Ingrese el nombre de la protectora");
            }

            //Ciudad
            var queryCiudad =
                await
                (from c in context.Ciudades.Where(x => x.Id == input.Direccion.IdCiudad)
                 from p in context.Provincias.Where(x => x.Id == c.IdProvincia)
                 select new { ciudad = c, provincia = p }).FirstOrDefaultAsync();

            if (queryCiudad == null)
            {
                result.AddError("Ingrese una ciudad");
            }

            if (input.CantidadDeMascotas <= 0) 
            {
                result.AddError("Ingrese la cantidad de mascotas que puede albergar la protectora");
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
                    Apellido = input.ApellidoUsuario.Trim(),
                    Email = input.Email.Trim(),
                    Password = input.Password,
                    Nombre = input.NombreUsuario.Trim(),
                    IdTipoRegistro = tipoRegistro.Id
                };

                await context.Usuarios.AddAsync(userToAdd);
                await context.SaveChangesAsync(new CancellationToken());

                var protectoraToAdd = new Protectora()
                {
                    UsuarioAsociadoId = userToAdd.Id,
                    Calle = (input.Direccion.Calle ?? "").Trim(),
                    CantidadDeMascotas = input.CantidadDeMascotas,
                    Departamento = (input.Direccion.Departamento ?? "").Trim(),
                    Descripcion = (input.Descripcion ?? "").Trim(),
                    Facebook = (input.Facebook ?? "").Trim(),
                    IdCiudad = (input.Direccion.IdCiudad),
                    Instagram = (input.Instagram ?? "").Trim(),
                    Nombre = (input.NombreProtectora ?? "").Trim(),
                    Numero = input.Direccion.Numero,
                    PaginaWeb = (input.SitioWeb ?? "").Trim(),
                    Piso = input.Direccion.Piso
                };

                await context.Protectoras.AddAsync(protectoraToAdd);
                await context.SaveChangesAsync(new CancellationToken());

                t.Commit();

                var dto = ProtectoraMaps.MapToDto()(protectoraToAdd);

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

        public async Task<OperationResult<bool>> DeleteProtectora(int idProtectora) 
        { 
            var result = new OperationResult<bool>();
            result.SetResult(false);

            var protectoraToDelete = 
                await
                context.Protectoras.FirstOrDefaultAsync(x => x.Id == idProtectora);

            if (protectoraToDelete == null)
            {
                result.AddError($"No existe una protectora con id {idProtectora}");
                return result;
            }

            //Elimino usuario relacionado
            var userToDelete = 
                await context.Usuarios.FirstOrDefaultAsync(x => x.Id == protectoraToDelete.UsuarioAsociadoId);

            var t = context.Database.BeginTransaction();

            try
            {
                context.Usuarios.Remove(userToDelete);
                context.Protectoras.Remove(protectoraToDelete);
                await context.SaveChangesAsync(new CancellationToken());

                t.Commit();
            }
            catch (Exception)
            {
                t.Rollback();
                throw;
            }

            result.SetResult(true);
            return result;

        }
    }
}

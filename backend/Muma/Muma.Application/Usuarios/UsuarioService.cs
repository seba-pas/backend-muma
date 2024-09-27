using Microsoft.EntityFrameworkCore;
using Muma.Application.Common;
using Muma.Application.Usuarios.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Application.Usuarios
{
    public class UsuarioService(IApplicationDbContext context)
    {
        public async Task<OperationResult<UsuarioDto>> Login(string email, string password)
        { 
            var result = new OperationResult<UsuarioDto>();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                result.AddError("Ingrese email y password");
                return result;
            }

            var usuario = await context.Usuarios.Include(u => u.TipoRegistro).FirstOrDefaultAsync(x => x.Email.Trim().ToLower() == email.Trim().ToLower() && x.Password == password);

            if (usuario == null) 
            {
                result.AddError("No se encontró un usuario con esa combinación de email y password");
                return result;
            }

            var dto = UsuarioMaps.MapToDto()(usuario);

            result.SetResult(dto);

            return result;
        }
    }
}

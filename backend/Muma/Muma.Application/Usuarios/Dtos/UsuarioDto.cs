using Muma.Application.Combos.Dtos;
using Muma.Application.Common.Dtos;
using Muma.Application.Protectoras.Dtos;
using Muma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Application.Usuarios.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Email { get; set; }

        public int IdTipoRegistro { get; set; }

        public TipoRegistroDto TipoRegistro { get; set; }

    }

    public static class UsuarioMaps 
    {
        public static Func<Usuario, UsuarioDto> MapToDto()
        {
            return u => new UsuarioDto()
            {
                Apellido = u.Apellido,
                Email = u.Email,
                Id = u.Id,
                Nombre = u.Nombre,
                IdTipoRegistro = u.IdTipoRegistro,
                TipoRegistro = TipoRegistroMaps.MapToDto()(u.TipoRegistro)
            };
        }
    }
}

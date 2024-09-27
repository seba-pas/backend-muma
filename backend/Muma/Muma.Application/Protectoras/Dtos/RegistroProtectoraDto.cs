using Muma.Application.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Application.Protectoras.Dtos
{
    public class RegistroProtectoraDto
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string NombreUsuario{ get; set; }

        public string ApellidoUsuario { get; set; }

        public string NombreProtectora { get; set; }

        public string Descripcion { get; set; }

        public string SitioWeb { get; set; }

        public string Instagram { get; set; }

        public string Facebook { get; set; }

        public int CantidadDeMascotas { get; set; }

        public DireccionDto Direccion { get; set; } = new DireccionDto();
    }
}

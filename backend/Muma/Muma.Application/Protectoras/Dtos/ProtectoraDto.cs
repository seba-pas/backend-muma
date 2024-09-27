using Muma.Application.Combos.Dtos;
using Muma.Application.Common.Dtos;
using Muma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Application.Protectoras.Dtos
{
    public class ProtectoraDto
    {
        public int Id { get; set; }

        public string NombreProtectora { get; set; }

        public string Descripcion { get; set; }

        public string SitioWeb { get; set; }

        public string Instagram { get; set; }

        public string Facebook { get; set; }

        public int CantidadDeMascotas { get; set; }

        public DireccionDto Direccion { get; set; }
    }

    public class ProtectoraListViewDto 
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int CantidadDeMascotas { get; set; }

        public DireccionDto? Direccion { get; set; }
    }

    public static class ProtectoraMaps
    {
        public static Func<Protectora, ProtectoraDto> MapToDto()
        {
            return p => new ProtectoraDto()
            {
                Id = p.Id,
                CantidadDeMascotas = p.CantidadDeMascotas,
                Descripcion = p.Descripcion,
                Direccion = new DireccionDto() 
                    {
                        Calle = p.Calle,
                        Departamento = p.Departamento,
                        IdCiudad = p.IdCiudad,
                        Numero = p.Numero,
                        Piso = p.Piso,
                        Ciudad = CiudadMaps.MapToDto()(p.Ciudad),
                        Provincia = ProvinciaMaps.MapToDto()(p.Ciudad.Provincia)
                    },
                Facebook = p.Facebook,
                Instagram = p.Instagram,
                NombreProtectora = p.Nombre,
                SitioWeb = p.PaginaWeb,
            };
        }

        public static Func<Protectora, ProtectoraListViewDto> MapToListViewDto()
        {
            return p => new ProtectoraListViewDto()
            {
                Id = p.Id,
                Nombre = p.Nombre,
                CantidadDeMascotas = p.CantidadDeMascotas,

            };
        }
    }
}

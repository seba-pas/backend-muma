using Muma.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Application.Combos.Dtos
{
    public class CiudadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public long IdProvincia { get; set; }
    }

    public static class CiudadMaps
    {
        public static Func<Ciudad, CiudadDto> MapToDto()
        {
            return c => new CiudadDto()
            {
                Id = c.Id,
                Nombre = c.Nombre,
                IdProvincia = c.IdProvincia
            };
        }
    }
}

using Muma.Application.Combos.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Application.Common.Dtos
{
    public class DireccionDto
    {
        public int IdCiudad { get; set; }
        public string Calle { get; set; } = "";

        public string Numero { get; set; } = "";

        public string Piso { get; set; } = "";

        public string Departamento { get; set; } = "";

        public ProvinciaDto? Provincia { get; set; }
        public CiudadDto? Ciudad { get; set; }
    }
}

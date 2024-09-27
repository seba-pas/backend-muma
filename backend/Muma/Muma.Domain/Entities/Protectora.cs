using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Domain.Entities
{
    public class Protectora
    {
        public int Id { get; set; }

        public int UsuarioAsociadoId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string PaginaWeb { get; set; }

        public string Instagram { get; set; }

        public string Facebook { get; set; }

        public int IdCiudad { get; set; }

        public string Calle { get; set; }

        public string Numero { get; set; }

        public string Piso { get; set; }

        public string Departamento { get; set; }

        public int CantidadDeMascotas { get; set; }

        public Ciudad Ciudad { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muma.Domain.Entities
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProvincia { get; set; }

        public Provincia Provincia { get; set; }
    }
}

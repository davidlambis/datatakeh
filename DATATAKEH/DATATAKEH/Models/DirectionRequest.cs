using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class DirectionRequest
    {
        public string Departamento { get; set; }

        public string Municipio { get; set; }

        public string Barrio { get; set; }

        public string Direccion { get; set; }

        public string Observaciones { get; set; }

        public int EquipoId { get; set; }
    }
}

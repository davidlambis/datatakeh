using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class EquipoRequest
    {
        public string Condicion { get; set; }

        public string IsAmplificador { get; set; }

        public string IsFuente { get; set; }

        public string IsCaja { get; set; }

        public string IsEnergia { get; set; }

        public string OtroEquipo { get; set; }

        public string CodigoPlaca { get; set; }

        public int VanoId { get; set; }
    }
}

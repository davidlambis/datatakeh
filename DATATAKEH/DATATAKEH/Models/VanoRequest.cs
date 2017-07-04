using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class VanoRequest
    {
        public string TipoVano { get; set; }

        public string Reserva { get; set; }

        public string LongitudVano { get; set; }

        public string TipoCableRed { get; set; }

        public string TipoCableComunicacion { get; set; }

        public int Poste_Id { get; set; }
    }
}

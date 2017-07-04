using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class FotoRequest
    {
        public string RutaFoto { get; set; }
     
        public byte[] ImageArray { get; set; }

        public string NombreFoto { get; set; }

        /*public string NombreFoto2 { get; set; }

        public string NombreFoto3 { get; set; }

        public string NombreFoto4 { get; set; }*/

        public int DirectionId { get; set; }

    }
}

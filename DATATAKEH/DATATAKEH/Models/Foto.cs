using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class Foto
    {
        [PrimaryKey,AutoIncrement]
        public int FotoIdLocal { get; set; }

        public int FotoId { get;set; }

        public string RutaFoto { get; set; }
        // public string Image { get; set; }

        public byte[] ImageArray { get; set; }

        public bool Estado { get; set; }

        public string NombreFoto { get; set; }

        /*public string NombreFoto2 { get; set; }

        public string NombreFoto3 { get; set; }

        public string NombreFoto4 { get; set; } */

        public int DirectionIdLocal { get; set; }

        [ManyToOne]
        public Direction Direction { get; set; } 

    }
}

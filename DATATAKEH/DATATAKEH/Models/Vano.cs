using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class Vano
    {
        //HACER UN VANO ID LOCALLLLL
        [PrimaryKey,AutoIncrement]
        public int VanoIdLocal { get; set; }

        public int VanoId { get; set; }

        public string TipoVano { get; set; }

        public string Reserva { get; set; }

        public string LongitudVano { get; set; }

        public string TipoCableRed { get; set; } 

        public string TipoCableComunicacion { get; set; }

        public bool Estado { get; set; }

        [ForeignKey(typeof(Poste))]
        public int PosteIdLocal { get; set; }

        [OneToOne]
        public Poste poste { get; set; }

       // public int NumeroApoyo { get; set; }

        //public int ProjectId { get; set; }

        /*[OneToOne("Poste", "Vano")]
        public Poste poste { get; set; } */

        /*[ManyToOne]
        public Project Project { get; set; } */

        public override int GetHashCode()
        {
            return VanoIdLocal;
        }
    }
}

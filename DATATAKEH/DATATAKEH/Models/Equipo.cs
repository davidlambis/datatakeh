using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class Equipo
    {
       [PrimaryKey,AutoIncrement]
       public int EquipoIdLocal { get; set; }

       public int EquipoId { get; set; }

       public string Condicion { get; set; }

       public string IsAmplificador { get; set; }

       public string IsFuente { get; set; }

       public string IsCaja { get; set; }

       public string IsEnergia { get; set; } 

       public string OtroEquipo { get; set; }

       public string CodigoPlaca { get; set; }

       public bool Estado { get; set; }

       [ForeignKey(typeof(Vano))]
       public int VanoIdLocal { get; set; }

       [OneToOne]
       public Vano vano { get; set; }

        public override int GetHashCode()
       {
            return EquipoIdLocal;
       }
    }
}

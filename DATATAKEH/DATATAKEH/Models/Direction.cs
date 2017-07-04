using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class Direction
    {
        [PrimaryKey,AutoIncrement]
        public int DirectionIdLocal { get; set; }

        public int DirectionId { get; set; }

        public string Departamento { get; set; }

        public string Municipio { get; set; }

        public string Barrio { get; set; }

        public string Direccion { get; set; }

        public string Observaciones { get; set; }

        public bool Estado { get; set; }

        [ForeignKey(typeof(Equipo))]
        public int EquipoIdLocal { get; set; }

        [OneToOne]
        public Equipo equipo { get; set; }

        /*
        public List<int> FotoId { get; set; }

        [OneToMany]
        public List<Foto> Fotos { get; set; } */

        public override int GetHashCode()
        {
            return DirectionIdLocal;
        }
    }
}

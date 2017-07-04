using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class Project
    {
        [PrimaryKey,AutoIncrement]
        public int ProjectIdLocal { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string Ciudad { get; set; }

        public string EmpresaPropietaria { get; set; }

        public string EmpresaOperadora { get; set; }

        public bool Estado { get; set; }

        public int UserId { get; set; }

        [ManyToOne]
        public User User { get; set; } 

        /*[OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Poste> Postes { get; set; } */

        public override int GetHashCode()
        {
            return ProjectIdLocal;
        }
    }
}

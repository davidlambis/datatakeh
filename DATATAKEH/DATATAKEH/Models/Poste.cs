﻿using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class Poste
    {
        [PrimaryKey,AutoIncrement]
        public int PosteIdLocal { get; set; }

        public int Poste_Id { get; set; }
        
        public string CodigoApoyo { get; set; }
  
        public string Condicion { get; set; }

        public string Material { get; set; }

        public string LongitudPoste { get; set; }

        public string ResistenciaMecanica { get; set; }

        public string Estado { get; set; }

        public string CantidadRetenidas { get; set; }

        public string Propiedad { get; set; }

        public string NivelTension { get; set; }

        public string AlturaDisponible { get; set; }

        public string AlturaMontaje { get; set; }

        public string TipoEstructura { get; set; }

        public string RedesBT { get; set; }

        public string Retenidas { get; set; }

        public string CablesOperador { get; set; }

        public string CablesComunicacionFinal { get; set; }

        public bool EstadoSubida { get; set; }

        public int ProjectIdLocal { get; set; }

        public double Latitud { get; set; }

        public double Longitud { get; set; }

        [ManyToOne]
        public Project Project { get; set; }

        public override int GetHashCode()
        {
            return PosteIdLocal;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    public class ProjectRequest
    {
        public string ProjectName { get; set; }

        public string Ciudad { get; set; }

        public string EmpresaPropietaria { get; set; }

        public string EmpresaOperadora { get; set; }

        public int UserId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Models
{
    //Clase genérica
    public class Response
    {
        //Retorna true o false si hace la petición bien
        public bool IsSuccess { get; set; }
        //El mensaje de descripción del proceso, si falla , por qué ? 
        public string Message { get; set; }
        //Devuelve el resultado, por ejemplo en el login el objeto User
        public object Result { get; set; }
    }
}

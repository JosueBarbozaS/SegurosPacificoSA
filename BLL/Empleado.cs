using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Empleado
    {
        
        public string Cedula { get; set; }

        
        public string NombreCompleto { get; set; }

        
        public string Departamento { get; set; }

        public int HorasNormales { get; set; }

        public int HorasExtras { get; set; }

        
        public decimal SalarioBruto { get; set; }

     
        public decimal Deducciones { get; set; }

       
        public decimal SalarioNeto { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Listas
{
    public class listaCliente
    {
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string empresa { get; set; }
        public string colonia { get; set; }
        public string codigoPostal { get; set; }
        public string telefono { get; set; }
        public string municipio { get; set; }
        public string estado { get; set; }
        public string rfc { get; set; }
        public int plazoPago { get; set; }
        public decimal adeudos { get; set; }

    }
}

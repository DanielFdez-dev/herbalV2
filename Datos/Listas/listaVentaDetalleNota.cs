using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Listas
{
    public class listaVentaDetalleNota
    {
        public string claveProducto { get; set; }
        public string codigo { get; set; }
        public string producto { get; set; }
        public string lote { get; set; }
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal importe { get; set; }
        public string tipoPrecio { get; set; }
        public DateTime caducidad { get; set; }

    }
}

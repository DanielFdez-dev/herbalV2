using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Listas
{
    public class listaVentaGeneralNota
    {
        public string folioVenta { get; set; }
        public string idCliente { get; set; }
        public DateTime fechaVenta { get; set; }
        public DateTime fechaEntrega { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int cantidad { get; set; }
        public decimal subtotal { get; set; }
        public decimal total { get; set; }
        public decimal precioDescuento { get; set; }
        public int descuento { get; set; }
        public string importeConLetra { get; set; }
        public int plazoPago { get; set; }
        public string tipoOperacion { get; set; }
    }
}

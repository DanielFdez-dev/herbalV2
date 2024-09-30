using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Listas
{
    public class listaDetalleUtilidad
    {
        public int folio { get; set; }
        public decimal subtotal { get; set; }
        public decimal totalVenta { get; set; }
        public decimal totalVentaCosto { get; set; } //
        public DateTime fechaVenta { get; set; }
        public DateTime fechaEntrega { get; set; }
        public DateTime fechaVencimiento { get; set; }
        public int cantidadTotal { get; set; }
        public decimal precioFlete { get; set; }
        public int porcentajeComision { get; set; }
        public decimal precioComision { get; set; }
        public decimal precioDescuento { get; set; }
        public int descuento { get; set; }
        public int plazoPago { get; set; }
        public string tipoOperacion { get; set; }
        public decimal totalUtilidad { get; set; }
    }
}

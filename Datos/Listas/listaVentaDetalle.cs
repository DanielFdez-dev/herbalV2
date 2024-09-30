
namespace Datos.Listas
{
    public class listaVentaDetalle
    {
        public int cantidad { get; set; }
        public decimal precioUnitario { get; set; }
        public decimal precioIva { get; set; }
        public decimal total { get; set; }
        public int idTipoPrecio { get; set; }
        public int idLote { get; set; }
        public int idVenta { get; set; }
    }
}

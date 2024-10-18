using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Listas
{
    public class listaProducto
    {
        public int idProducto { get; set; }
        public string codigo { get; set; }
        public string amecop { get; set; }
        public string descripcion { get; set; }
        public int stock { get; set; }
        public decimal precioCosto { get; set; }
        public decimal precioLab { get; set; }
        public decimal precioMayoreo { get; set; }
        public decimal precioLista { get; set; }
        public decimal precioDistribuidor { get; set; }
        public bool iva { get; set; }
        public int idClasificacion { get; set; }
        public string clasificacion { get; set; }
        public int idMarca { get; set; }
        public string marca { get; set; }

    }
}

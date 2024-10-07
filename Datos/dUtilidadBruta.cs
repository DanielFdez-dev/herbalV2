using Datos.Listas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dUtilidadBruta : conexion
    {
        public List<listaVentaDetalleNota> utilidadDetalle(int folio)
        {
            List<listaVentaDetalleNota> lista = new List<listaVentaDetalleNota>();
            using (var con = GetConnection())
            {
                con.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = "select p.amecop, p.descripcion, l.lote,vd.cantidad,vd.precioCostoVenta as precioUnitario, vd.precioCostoVenta*vd.cantidad as total, l.caducidad, codigo from ventaDetalle vd left join ventas v on vd.idVenta=v.idVenta left join lotes l on vd.idLote=l.idLote left join productos p on l.idProducto=p.idProducto  where v.folio=@folio;";
                    command.Parameters.AddWithValue("@folio", folio);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var listaTemp = new listaVentaDetalleNota()
                        {
                            codigo = reader["amecop"].ToString(),
                            producto = reader["descripcion"].ToString(),
                            lote = reader["lote"].ToString(),
                            cantidad = Convert.ToInt32(reader["cantidad"]),
                            precioUnitario = Convert.ToDecimal(reader["precioUnitario"]),
                            importe = Convert.ToDecimal(reader["total"]),
                            caducidad = Convert.ToDateTime(reader["caducidad"]),
                            claveProducto = reader["codigo"].ToString()
                        };
                        lista.Add(listaTemp);
                    }
                }
            }
            return lista;
        }
        public List<listaDetalleUtilidad> utilidadGeneral(int folio, decimal totalCosto)
        {
            List<listaDetalleUtilidad> lista = new List<listaDetalleUtilidad>();
            using (var con = GetConnection())
            {
                con.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = "select folio,total,subtotal,fechaVenta,fechaEntrega,fechaPago,cantidad,descuento,precioDescuento,costoFlete,porcentajeComisionVendedor,costoComisionVendedor, plazoPago,tipoVenta from ventas where folio=@folio;";
                    command.Parameters.AddWithValue("@folio", folio);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var listaTemp = new listaDetalleUtilidad()
                        {
                            folio = Convert.ToInt32(reader["folio"]),
                            subtotal = Convert.ToDecimal(reader["subtotal"]),
                            totalVenta = Convert.ToDecimal(reader["total"]),
                            fechaVenta = Convert.ToDateTime(reader["fechaVenta"]),
                            fechaEntrega = Convert.ToDateTime(reader["fechaEntrega"]),
                            fechaVencimiento = Convert.ToDateTime(reader["fechaPago"]),
                            cantidadTotal = Convert.ToInt32(reader["cantidad"]),
                            precioFlete = Convert.ToDecimal(reader["costoFlete"]),
                            porcentajeComision = Convert.ToInt32(reader["porcentajeComisionVendedor"]),
                            precioComision = Convert.ToDecimal(reader["costoComisionVendedor"]),
                            descuento = Convert.ToInt32(reader["descuento"]),
                            precioDescuento = Convert.ToDecimal(reader["precioDescuento"]),
                            plazoPago = Convert.ToInt32(reader["plazoPago"]),
                            tipoOperacion = reader["tipoVenta"].ToString(),
                            totalVentaCosto = totalCosto
                        };
                        lista.Add(listaTemp);
                    }
                }
            }
            return lista;
        }

        public List<listaDetalleUtilidad> utilidadPorFecha(DateTime fechaInicial, DateTime fechaFinal)
        {
            List<listaDetalleUtilidad> lista = new List<listaDetalleUtilidad>();
            using (var con = GetConnection())
            {
                con.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "selecT folio,cast(fechaVenta as date)as fechaVenta,subtotal, descuento, precioDescuento,total,porcentajeComisionVendedor,costoComisionVendedor, costoFlete, utilidadBruta from ventas where cast(fechaVenta as date) between @fechaInicial and @fechaFinal ORDER BY fechaVenta;";
                    cmd.Parameters.AddWithValue("@fechaInicial", fechaInicial.Date);
                    cmd.Parameters.AddWithValue("@fechaFinal", fechaFinal.Date);
                    cmd.CommandType = CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new listaDetalleUtilidad
                        {
                            folio = Convert.ToInt32(reader["folio"]),
                            fechaVenta = Convert.ToDateTime(reader["fechaVenta"]),
                            subtotal = Convert.ToDecimal(reader["subtotal"]),
                            descuento = Convert.ToInt32(reader["descuento"]),
                            precioDescuento = Convert.ToDecimal(reader["precioDescuento"]),
                            totalVenta = Convert.ToDecimal(reader["total"]),
                            porcentajeComision = Convert.ToInt32(reader["porcentajeComisionVendedor"]),
                            precioComision = Convert.ToDecimal(reader["costoComisionVendedor"]),
                            precioFlete = Convert.ToDecimal(reader["costoFlete"]),
                            totalUtilidad = Convert.ToDecimal(reader["utilidadBruta"])
                        });
                    }
                }
            }
            return lista;
        }
    }
}

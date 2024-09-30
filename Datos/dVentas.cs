using Datos.Listas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class dVentas : conexion
    {
        public bool agregarVenta(int folio, DateTime fechaPago, DateTime fechaEntrega, int plazoPago, int cantidad, decimal subtotal, decimal total, decimal totalIva, int descuento, decimal precioDescuento, 
            int idEmpleado, int idVendedor, int idCliente, List<listaVentaDetalle>listaDetalle, int idCotizacion, string tipoVenta)
        {
            bool result = false;
            using (var connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaccion = connection.BeginTransaction();
                try
                {

                    string script = "insert into ventas output inserted.idVenta values (@folio, getdate(),@fechaPago,@fechaEntrega,@plazoPago,@cantidad,@subtotal,@total,@totalIva,@descuento,@precioDescuento,@idEmpleado,@idVendedor,@idCliente,'PENDIENTE',@tipoVenta,-1,-1,-1,-1)";
                    SqlCommand venta = new SqlCommand(script, connection, transaccion);
                    venta.Parameters.AddWithValue("@folio", folio);
                    venta.Parameters.AddWithValue("@plazoPago", plazoPago);
                    venta.Parameters.AddWithValue("@fechaPago", fechaPago);
                    venta.Parameters.AddWithValue("@fechaEntrega", fechaEntrega);
                    venta.Parameters.AddWithValue("@cantidad", cantidad);
                    venta.Parameters.AddWithValue("@subtotal", subtotal);
                    venta.Parameters.AddWithValue("@total", total);
                    venta.Parameters.AddWithValue("@totalIva", totalIva);
                    venta.Parameters.AddWithValue("@descuento", descuento);
                    venta.Parameters.AddWithValue("@precioDescuento", precioDescuento);
                    venta.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    venta.Parameters.AddWithValue("@idVendedor", idVendedor);
                    venta.Parameters.AddWithValue("@idCliente", idCliente);
                    venta.Parameters.AddWithValue("@tipoVenta", tipoVenta);

                    // Obtener el ID de la venta insertada
                    int ventaID = (int)venta.ExecuteScalar();


                    
                    if (idCotizacion > 0)
                    {
                        SqlCommand detalleCommand = new SqlCommand("update cotizaciones set estatus='REALIZADA' where idCotizacion=@idCotizacion;", connection, transaccion);
                        
                        detalleCommand.Parameters.AddWithValue("@idCotizacion", idCotizacion);
                        detalleCommand.ExecuteNonQuery();
                    }
                    string scriptDetalle = "declare @precioCosto decimal(10,2) = (select precioCosto from productos p left join lotes l on p.idProducto=l.idProducto where l.idLote=@idLote); ";
                    scriptDetalle += "insert into ventaDetalle values(@cantidad,@precioUnitario,@precioIva, @total,@idTipoPrecio,@idLote,@idVenta,@precioCosto,(@precioCosto*@cantidad));" +
                        "update lotes set stock=stock-@cantidad where idLote=@idLote;" +
                        "update productos set stock=stock-@cantidad where idProducto=(select idProducto from lotes where idLote=@idLote);";
                    foreach (var detalle in listaDetalle)
                    {
                        SqlCommand detalleCommand = new SqlCommand(scriptDetalle, connection, transaccion);
                        detalleCommand.Parameters.AddWithValue("@cantidad", detalle.cantidad);
                        detalleCommand.Parameters.AddWithValue("@precioUnitario", detalle.precioUnitario);
                        detalleCommand.Parameters.AddWithValue("@precioIva", detalle.precioIva);
                        detalleCommand.Parameters.AddWithValue("@total", detalle.total);
                        detalleCommand.Parameters.AddWithValue("@idTipoPrecio", detalle.idTipoPrecio);
                        detalleCommand.Parameters.AddWithValue("@idLote", detalle.idLote);
                        detalleCommand.Parameters.AddWithValue("@idVenta", ventaID);

                        detalleCommand.ExecuteNonQuery();
                    }

                    string scriptCuentas = "insert into cuentasCobrar values((select folio from ventas where idVenta=@idVenta),null,null,'',@total,@total,1)";

                    SqlCommand cuentasCommand = new SqlCommand(scriptCuentas, connection, transaccion);
                    cuentasCommand.Parameters.AddWithValue("@idVenta", ventaID);
                    cuentasCommand.Parameters.AddWithValue("@total", total);
                    cuentasCommand.ExecuteNonQuery();


                    string scriptClientes = "update clientes set adeudo=adeudo+@total where idCliente=@idCliente";
                    SqlCommand clientesCommand = new SqlCommand(scriptClientes, connection, transaccion);
                    clientesCommand.Parameters.AddWithValue("@total", total);
                    clientesCommand.Parameters.AddWithValue("@idCliente", idCliente);
                    clientesCommand.ExecuteNonQuery();

                    // Confirmar la transacción
                    transaccion.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    transaccion.Rollback();
                    result = false;
                }
            }

            return result;
        }
        public int ultimoFolio()
        {
            int folio = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select isnull(max(folio),0)as folio from ventas";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        folio= Convert.ToInt32(reader["folio"]);
                    }
                    
                }
            }
            return folio + 1;
        }

        public List<listaVentaDetalleNota>ventaDetalleNota(int folioVenta)
        {
            List<listaVentaDetalleNota> lista = new List<listaVentaDetalleNota>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using(var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select p.amecop, p.descripcion, l.lote,vd.cantidad,vd.precioUnitario,vd.total,tp.descripcion as tipoPrecio, l.caducidad, codigo from ventaDetalle vd " +
                        "left join ventas v on vd.idVenta=v.idVenta left join lotes l on vd.idLote=l.idLote " +
                        "left join productos p on l.idProducto=p.idProducto left join tipoPrecio tp on vd.idTipoPrecio=tp.idTipoPrecio where v.folio=@folioVenta;";
                    command.Parameters.AddWithValue("@folioVenta", folioVenta);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        var listaTemp = new listaVentaDetalleNota()
                        {
                            codigo = reader["amecop"].ToString(),
                            producto = reader["descripcion"].ToString(),
                            lote = reader["lote"].ToString(),
                            cantidad = Convert.ToInt32(reader["cantidad"]),
                            precioUnitario = Convert.ToDecimal(reader["precioUnitario"]),
                            importe = Convert.ToDecimal(reader["total"]),
                            tipoPrecio = reader["tipoPrecio"].ToString(),
                            caducidad = Convert.ToDateTime(reader["caducidad"]),
                            claveProducto = reader["codigo"].ToString()
                        };
                        lista.Add(listaTemp);
                    }
                }
            }
            return lista;
        }
        public List<listaVentaGeneralNota> ventaGeneralNota(int folioVenta)
        {
            List<listaVentaGeneralNota> lista = new List<listaVentaGeneralNota>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select fechaVenta,fechaEntrega,fechaPago,cantidad,subtotal,total,precioDescuento,descuento,plazoPago,tipoVenta from ventas where folio=@folioVenta;";
                    command.Parameters.AddWithValue("@folioVenta", folioVenta);
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var listaTemp = new listaVentaGeneralNota()
                        {
                            folioVenta = folioVenta.ToString(),
                            idCliente = "0",
                            fechaVenta = Convert.ToDateTime(reader["fechaVenta"]),
                            fechaEntrega = Convert.ToDateTime(reader["fechaEntrega"]),
                            fechaVencimiento = Convert.ToDateTime(reader["fechaPago"]),
                            cantidad = Convert.ToInt32(reader["cantidad"]),
                            subtotal = Convert.ToDecimal(reader["subtotal"]),
                            total = Convert.ToDecimal(reader["total"]),
                            precioDescuento = Convert.ToDecimal(reader["precioDescuento"]),
                            descuento = Convert.ToInt32(reader["descuento"]),
                            plazoPago = Convert.ToInt32(reader["plazoPago"]),
                            importeConLetra = dConversor.NumeroALetras(Convert.ToDecimal(reader["total"])),
                            tipoOperacion = reader["tipoVenta"].ToString()
                        };
                        lista.Add(listaTemp);
                    }
                }
            }
            return lista;
        }

        public DataTable listarVentasPendientes()
        {
            DataTable tabla = new DataTable();
            using (var con = GetConnection())
            {
                con.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = "select folio, cast(fechaVenta as date) as fechaVenta, cantidad, subtotal, descuento, total from ventas where costoFlete=-1";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    tabla.Load(reader);
                }
            }
            return tabla;
        }
        public bool cerrarVenta(int folio, decimal costoFlete, int porcentajeComision, decimal costoComision)
        {
            bool result = false;
            using (var con = GetConnection())
            {
                con.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = "update ventas set costoFlete=@costoFlete, porcentajeComisionVendedor=@porcentajeComisionVendedor, costoComisionVendedor=@costoComisionVendedor where folio=@folio;" +
                        "exec calcularUtilidadBruta @folio;";
                    command.Parameters.AddWithValue("@costoFlete", costoFlete);
                    command.Parameters.AddWithValue("@porcentajeComisionVendedor", porcentajeComision);
                    command.Parameters.AddWithValue("@costoComisionVendedor", costoComision);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.CommandType = CommandType.Text;
                    int filas = command.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        public bool modificarComision(int folio, int porcentajeComision, decimal costoComision)
        {
            bool result = false;
            using (var con = GetConnection())
            {
                con.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = "update ventas set porcentajeComisionVendedor=@porcentajeComisionVendedor, costoComisionVendedor=@costoComisionVendedor where folio=@folio;" +
                        "exec calcularUtilidadBruta @folio";
                    command.Parameters.AddWithValue("@porcentajeComisionVendedor", porcentajeComision);
                    command.Parameters.AddWithValue("@costoComisionVendedor", costoComision);
                    command.Parameters.AddWithValue("@folio", folio);
                    command.CommandType = CommandType.Text;
                    int filas = command.ExecuteNonQuery();
                    if (filas > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}

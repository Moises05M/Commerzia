using Commerzia_API.Data;
using Commerzia_API.DTOs;
using Commerzia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public VentaController(CommerziaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearVenta([FromBody] CrearVentaDTO dto)
        {
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                return BadRequest("La venta debe tener al menos un producto.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var nuevaVenta = new Ventum
                {
                    NumeroComprobante = GenerarNumeroComprobante(), // Método de ayuda abajo
                    IdCliente = dto.IdCliente,
                    IdEstado = dto.IdEstado,
                    FechaVenta = DateTime.Now,
                    SubTotal = dto.SubTotal,
                    Impuestos = dto.Impuestos,
                    Total = dto.Total,
                    FechaCreacion = DateTime.Now
                };

                _context.Add(nuevaVenta);
                await _context.SaveChangesAsync(); // Guardamos para obtener el IdVenta generado

                // RECORRER LOS DETALLES
                foreach (var item in dto.Detalles)
                {
                    // Insertar en VentaDetalle
                    var detalle = new VentaDetalle
                    {
                        IdVenta = nuevaVenta.IdVenta,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario,
                        TotalLinea = item.Cantidad * item.PrecioUnitario
                    };
                    _context.VentaDetalles.Add(detalle);

                    // Insertar en MovimientoInventario (Descontar Stock)
                    var movimiento = new MovimientoInventario
                    {
                        IdProducto = item.IdProducto,
                        IdVenta = nuevaVenta.IdVenta,
                        IdTipoMovimiento = 2, // Asumiendo que 2 es "Salida por Venta" en tu base de datos
                        Cantidad = item.Cantidad * -1, // Negativo porque sale del inventario
                        Fecha = DateTime.Now,
                        // Para SaldoCantidad idealmente deberías consultar el stock actual primero, 
                        // pero por ahora lo dejamos referencial según tu modelo
                        SaldoCantidad = 0
                    };
                    _context.MovimientoInventarios.Add(movimiento);
                }

                await _context.SaveChangesAsync();

                // CONFIRMAR TRANSACCIÓN
                await transaction.CommitAsync();

                return Ok(new { message = "Venta procesada exitosamente", idVenta = nuevaVenta.IdVenta, comprobante = nuevaVenta.NumeroComprobante });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = "Error al procesar la venta.", details = ex.Message });
            }
        }

        // Orden tipo "INV-00123
        private string GenerarNumeroComprobante()
        {
            return "INV-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}

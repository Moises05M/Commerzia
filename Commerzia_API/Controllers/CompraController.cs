using Commerzia_API.Data;
using Commerzia_API.DTOs;
using Commerzia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public CompraController(CommerziaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearCompra([FromBody] CrearCompraDTO dto)
        {
            if (dto.Detalles == null || dto.Detalles.Count == 0)
                return BadRequest("La compra debe tener al menos un producto.");

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // CREAR LA CABECERA DE LA COMPRA
                var nuevaCompra = new Compra
                {
                    IdProveedor = dto.IdProveedor,
                    IdEstado = dto.IdEstado,
                    NumeroFactura = dto.NumeroComprobante,
                    FechaEmision = DateOnly.FromDateTime(DateTime.Now),
                    SubTotal = dto.SubTotal,
                    Impuestos = dto.Impuestos,
                    Total = dto.Total,
                    FechaCreacion = DateTime.Now
                };

                _context.Add(nuevaCompra);
                await _context.SaveChangesAsync();

                // RECORRER LOS DETALLES
                foreach (var item in dto.Detalles)
                {
                    var detalle = new CompraDetalle
                    {
                        IdCompra = nuevaCompra.IdCompra,
                        IdProducto = item.IdProducto,
                        Cantidad = item.Cantidad,
                        CostoUnitario = item.PrecioUnitario,
                        TotalLinea = item.Cantidad * item.PrecioUnitario
                    };
                    _context.Add(detalle);

                    // Insertar en MovimientoInventario (SUMAR Stock)
                    var movimiento = new MovimientoInventario
                    {
                        IdProducto = item.IdProducto,
                        IdCompra = nuevaCompra.IdCompra,
                        IdTipoMovimiento = 1, // 1 es Entrada/Compra
                        Cantidad = item.Cantidad,
                        Fecha = DateTime.Now,
                        CostoUnitario = item.PrecioUnitario,
                        SaldoCantidad = 0,
                        SaldoCostoTotal = 0
                    };
                    _context.Add(movimiento);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Compra procesada exitosamente", idCompra = nuevaCompra.IdCompra });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = "Error al procesar la compra.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}

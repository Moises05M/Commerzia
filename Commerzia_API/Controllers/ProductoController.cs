using Commerzia_API.DTOs;
using Commerzia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commerzia_API.Data;
using Microsoft.EntityFrameworkCore;

namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public ProductoController(CommerziaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] CrearProductoDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existeSku = await _context.Productos.AnyAsync(p => p.CodigoSku == dto.CodigoSKU);
            if (existeSku)
            {
                return BadRequest(new { message = "El Código SKU ya existe en el sistema." });
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var nuevoProducto = new Producto
                {
                    Nombre = dto.Nombre,
                    CodigoSku = dto.CodigoSKU,
                    Descripcion = dto.Descripcion,
                    PrecioVenta = dto.PrecioVenta,
                    CostoPromedio = 0,
                    IdCategoria = dto.IdCategoria,
                    IdMarca = dto.IdMarca,
                    IdEstado = dto.IdEstado,
                    FechaCreacion = DateTime.Now
                };

                _context.Productos.Add(nuevoProducto);
                await _context.SaveChangesAsync();

                if (dto.StockInicial > 0)
                {
                    var movimiento = new MovimientoInventario
                    {
                        IdProducto = nuevoProducto.IdProducto,
                        Cantidad = dto.StockInicial,
                        CostoUnitario = 0,
                        SaldoCantidad = dto.StockInicial,
                        SaldoCostoTotal = 0,
                        IdTipoMovimiento = 1,
                        Fecha = DateTime.Now
                    };

                    _context.MovimientoInventarios.Add(movimiento);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return Ok(new { message = "Producto creado exitosamente", idProducto = nuevoProducto.IdProducto });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = "Error interno.", details = ex.Message });
            }
        }
    }
}

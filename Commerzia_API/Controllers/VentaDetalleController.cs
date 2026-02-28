using Commerzia_API.Data;
using Commerzia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// SOLO PARA MOSTRAR LOS DETALLES DE UNA VENTA, NO PARA CREAR O MODIFICAR DETALLES
namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaDetalleController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public VentaDetalleController(CommerziaContext context)
        {
            _context = context;
        }

        [HttpGet("Venta/{idVenta}")]
        public IActionResult GetDetallesPorVenta(int idVenta)
        {
            var ventaExiste = _context.Set<Ventum>().Any(v => v.IdVenta == idVenta);

            if (!ventaExiste)
            {
                return NotFound(new { message = $"La venta con ID {idVenta} no existe." });
            }

            var detalles = _context.Set<VentaDetalle>()
                .Where(d => d.IdVenta == idVenta)
                .Select(d => new
                {
                    IdVentaDetalle = d.IdVentaDetalle,
                    IdProducto = d.IdProducto,
                    NombreProducto = d.IdProductoNavigation.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario,
                    TotalLinea = d.TotalLinea
                })
                .ToList();

            return Ok(detalles);
        }
    }
}

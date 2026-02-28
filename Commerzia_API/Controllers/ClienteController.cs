using Commerzia_API.Data;
using Commerzia_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public ClienteController(CommerziaContext context)
        {
            _context = context;
        }

        // ENDPOINT PARA BUSCADOR 
        [HttpGet("Buscar")]
        public IActionResult BuscarClientes([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { message = "El parámetro de búsqueda no puede estar vacío." });
            }

            var busqueda = query.ToLower();

            var clientes = _context.Set<Cliente>()
                .Where(c => c.NombreCompleto.ToLower().Contains(busqueda)
                         || (c.Correo != null && c.Correo.ToLower().Contains(busqueda))
                         || (c.Telefono != null && c.Telefono.Contains(busqueda)))
                .Select(c => new
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.NombreCompleto,
                    Email = c.Correo,
                    Telefono = c.Telefono
                })
                .Take(10)
                .ToList();

            return Ok(clientes);
        }

        // ENDPOINT PARA OBTENER TODOS 
        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _context.Set<Cliente>()
                .Select(c => new
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.NombreCompleto,
                    Email = c.Correo,
                    Telefono = c.Telefono
                })
                .Take(50)
                .ToList();

            return Ok(clientes);
        }
    }
}

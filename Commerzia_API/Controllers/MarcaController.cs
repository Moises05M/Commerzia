using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commerzia_API.Data;

namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public MarcaController(CommerziaContext context) => _context = context;

        [HttpGet]
        public IActionResult GetMarcas()
        {
            var marcas = _context.Marcas
                .Select(m => new { m.IdMarca, m.Nombre })
                .ToList();

            return Ok(marcas);
        }
    }
}

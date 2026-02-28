using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commerzia_API.Data;

namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public EstadoController(CommerziaContext context) => _context = context;

        [HttpGet]
        public IActionResult GetEstados()
        {
            var estados = _context.Estados
                .Select(e => new { e.IdEstado, e.Nombre })
                .ToList();

            return Ok(estados);
        }
    }
}

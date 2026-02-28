using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commerzia_API.Data;

namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public CategoriaController(CommerziaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            // Id y el Nombre de las categorías activas
            var categorias = _context.Categoria
                .Select(c => new { c.IdCategoria, c.Nombre })
                .ToList();

            return Ok(categorias);
        }
    }
}

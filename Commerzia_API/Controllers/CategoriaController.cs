using Commerzia_API.Data;
using Commerzia_API.Models;
using Commerzia_API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Commerzia_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly CommerziaContext _context;

        public CategoriaController(CommerziaContext context)
        {
            _context = context;
        }

        // 1. GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            var categorias = await _context.Categoria
                .Select(c => new CategoriaDTO
                {
                    IdCategoria = c.IdCategoria,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion
                })
                .ToListAsync();

            return Ok(categorias);
        }

        // 2. GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada" });

            var categoriaDto = new CategoriaDTO
            {
                IdCategoria = categoria.IdCategoria,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };

            return Ok(categoriaDto);
        }

        // 3. POST: api/Categoria
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> PostCategoria([FromBody] CategoriaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaCategoria = new Categorium
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                UsuarioCreacion = dto.UsuarioCreacion ?? "Sistema",
                FechaCreacion = DateTime.Now
            };

            _context.Categoria.Add(nuevaCategoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoria), new { id = nuevaCategoria.IdCategoria }, new CategoriaDTO
            {
                IdCategoria = nuevaCategoria.IdCategoria,
                Nombre = nuevaCategoria.Nombre,
                Descripcion = nuevaCategoria.Descripcion
            });
        }

        // 4. PUT: api/Categoria/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, [FromBody] CategoriaUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriaDb = await _context.Categoria.FindAsync(id);
            if (categoriaDb == null)
                return NotFound(new { message = "Categoría no encontrada" });

            categoriaDb.Nombre = dto.Nombre;
            categoriaDb.Descripcion = dto.Descripcion;
            categoriaDb.UsuarioModificacion = dto.UsuarioModificacion ?? "Sistema";
            categoriaDb.FechaModificacion = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id)) return NotFound();
                else throw;
            }

            return Ok(new { message = "Categoría actualizada correctamente" });
        }

        // 5. DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
                return NotFound(new { message = "Categoría no encontrada" });

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Categoría eliminada correctamente" });
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.IdCategoria == id);
        }
    }
}
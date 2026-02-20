using System.ComponentModel.DataAnnotations;

namespace Commerzia_API.DTOs
{
    public class CategoriaCreateDTO
    {
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = null!;

        [StringLength(200, ErrorMessage = "La descripción no puede superar los 200 caracteres.")]
        public string? Descripcion { get; set; }

        // Puedes pedir quién lo creó, o tomarlo luego automáticamente del Token JWT
        public string? UsuarioCreacion { get; set; }
    }
}

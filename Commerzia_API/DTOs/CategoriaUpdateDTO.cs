using System.ComponentModel.DataAnnotations;

namespace Commerzia_API.DTOs
{
    public class CategoriaUpdateDTO
    {
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        [StringLength(200)]
        public string? Descripcion { get; set; }

        public string? UsuarioModificacion { get; set; }
    }
}

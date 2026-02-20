namespace Commerzia_API.DTOs
{
    public class CategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        // Auditoria estara pendiente hasta que haga la interfaz con WinUI
    }
}

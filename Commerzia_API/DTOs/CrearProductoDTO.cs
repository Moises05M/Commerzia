namespace Commerzia_API.DTOs
{
    public class CrearProductoDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string CodigoSKU { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public int IdEstado { get; set; }
        public int StockInicial { get; set; } // El stock que generará el movimiento
    }
}

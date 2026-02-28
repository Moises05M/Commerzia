namespace Commerzia_API.DTOs
{
    public class CrearVentaDTO
    {
        public int IdCliente { get; set; }
        public int IdEstado { get; set; } // Ej: Pagado, Pendiente
        public decimal SubTotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public string? Notas { get; set; }

        // La lista de productos que se están vendiendo
        public List<CrearVentaDetalleDTO> Detalles { get; set; } = new List<CrearVentaDetalleDTO>();
    }
}

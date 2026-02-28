namespace Commerzia_API.DTOs
{
    public class CrearCompraDTO
    {
        public int IdProveedor { get; set; }
        public int IdEstado { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public string? NumeroComprobante { get; set; }

        public List<CrearCompraDetalleDTO> Detalles { get; set; } = new List<CrearCompraDetalleDTO>();
    }
}

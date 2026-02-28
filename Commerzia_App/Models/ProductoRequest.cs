using System;
using System.Collections.Generic;
using System.Text;

namespace Commerzia_App.Models
{
    public class ProductoRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string CodigoSKU { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int IdCategoria { get; set; }
        public int? IdMarca { get; set; }
        public int IdEstado { get; set; }

        // Propiedad extra para enviar el stock inicial al backend
        // (Tu API debería recibir esto e insertar un registro en MovimientoInventario)
        public int StockInicial { get; set; }
    }
}

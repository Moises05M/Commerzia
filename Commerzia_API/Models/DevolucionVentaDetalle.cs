using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class DevolucionVentaDetalle
{
    public int IdDevolucionDetalle { get; set; }

    public int IdDevolucion { get; set; }

    public int? IdVentaDetalle { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal TotalLinea { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public virtual DevolucionVentum IdDevolucionNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual VentaDetalle? IdVentaDetalleNavigation { get; set; }
}

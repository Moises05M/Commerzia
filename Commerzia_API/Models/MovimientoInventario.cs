using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class MovimientoInventario
{
    public int IdMovimiento { get; set; }

    public DateTime? Fecha { get; set; }

    public int IdProducto { get; set; }

    public int IdTipoMovimiento { get; set; }

    public int Cantidad { get; set; }

    public decimal? CostoUnitario { get; set; }

    public int SaldoCantidad { get; set; }

    public decimal? SaldoCostoTotal { get; set; }

    public int? IdVenta { get; set; }

    public int? IdCompra { get; set; }

    public int? IdDevolucion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual DevolucionVentum? IdDevolucionNavigation { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual TipoMovimientoInventario IdTipoMovimientoNavigation { get; set; } = null!;

    public virtual Ventum? IdVentaNavigation { get; set; }
}

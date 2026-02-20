using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int IdProveedor { get; set; }

    public string? NumeroFactura { get; set; }

    public DateOnly FechaEmision { get; set; }

    public DateTime? FechaRecepcion { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Impuestos { get; set; }

    public decimal? Total { get; set; }

    public decimal? SaldoPendiente { get; set; }

    public int? IdEstado { get; set; }

    public int? IdAsiento { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<CompraDetalle> CompraDetalles { get; set; } = new List<CompraDetalle>();

    public virtual Asiento? IdAsientoNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();

    public virtual ICollection<PagoProveedor> PagoProveedors { get; set; } = new List<PagoProveedor>();
}

using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class PagoProveedor
{
    public int IdPago { get; set; }

    public int IdCompra { get; set; }

    public DateTime? FechaPago { get; set; }

    public decimal Monto { get; set; }

    public int IdFormaPago { get; set; }

    public string? NumeroReferencia { get; set; }

    public string? Observaciones { get; set; }

    public int? IdAsiento { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Asiento? IdAsientoNavigation { get; set; }

    public virtual Compra IdCompraNavigation { get; set; } = null!;

    public virtual FormaPago IdFormaPagoNavigation { get; set; } = null!;
}

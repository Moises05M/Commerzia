using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class Gasto
{
    public int IdGasto { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime? FechaGasto { get; set; }

    public decimal MontoTotal { get; set; }

    public string? NumeroComprobante { get; set; }

    public int? IdProveedor { get; set; }

    public int IdFormaPago { get; set; }

    public int? IdEstado { get; set; }

    public int? IdAsiento { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Asiento? IdAsientoNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual FormaPago IdFormaPagoNavigation { get; set; } = null!;

    public virtual Proveedor? IdProveedorNavigation { get; set; }
}

using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class AsientoDetalle
{
    public int IdAsientoDetalle { get; set; }

    public int IdAsiento { get; set; }

    public int IdCuenta { get; set; }

    public decimal? Debe { get; set; }

    public decimal? Haber { get; set; }

    public string? Referencia { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual Asiento IdAsientoNavigation { get; set; } = null!;

    public virtual CuentaContable IdCuentaNavigation { get; set; } = null!;
}

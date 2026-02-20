using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class CuentaContable
{
    public int IdCuenta { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public int Nivel { get; set; }

    public bool? EsCuentaMovimiento { get; set; }

    public int? IdCuentaPadre { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<ActivoFijo> ActivoFijoIdCuentaActivoNavigations { get; set; } = new List<ActivoFijo>();

    public virtual ICollection<ActivoFijo> ActivoFijoIdCuentaDepreciacionNavigations { get; set; } = new List<ActivoFijo>();

    public virtual ICollection<ActivoFijo> ActivoFijoIdCuentaGastoNavigations { get; set; } = new List<ActivoFijo>();

    public virtual ICollection<AsientoDetalle> AsientoDetalles { get; set; } = new List<AsientoDetalle>();

    public virtual CuentaContable? IdCuentaPadreNavigation { get; set; }

    public virtual ICollection<CuentaContable> InverseIdCuentaPadreNavigation { get; set; } = new List<CuentaContable>();
}

using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class ActivoFijo
{
    public int IdActivo { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? CodigoActivo { get; set; }

    public DateOnly FechaAdquisicion { get; set; }

    public decimal ValorAdquisicion { get; set; }

    public decimal? ValorResidual { get; set; }

    public int VidaUtilMeses { get; set; }

    public int? IdEstado { get; set; }

    public int? IdCuentaActivo { get; set; }

    public int? IdCuentaDepreciacion { get; set; }

    public int? IdCuentaGasto { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<DepreciacionActivo> DepreciacionActivos { get; set; } = new List<DepreciacionActivo>();

    public virtual CuentaContable? IdCuentaActivoNavigation { get; set; }

    public virtual CuentaContable? IdCuentaDepreciacionNavigation { get; set; }

    public virtual CuentaContable? IdCuentaGastoNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }
}

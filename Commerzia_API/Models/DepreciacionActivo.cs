using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class DepreciacionActivo
{
    public int IdDepreciacion { get; set; }

    public int IdActivo { get; set; }

    public DateOnly FechaCalculo { get; set; }

    public decimal MontoDepreciado { get; set; }

    public decimal ValorLibroRemanente { get; set; }

    public int? IdAsiento { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public virtual ActivoFijo IdActivoNavigation { get; set; } = null!;

    public virtual Asiento? IdAsientoNavigation { get; set; }
}

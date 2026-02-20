using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class PeriodoContable
{
    public int IdPeriodo { get; set; }

    public int Anio { get; set; }

    public int Mes { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public bool? Cerrado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();
}

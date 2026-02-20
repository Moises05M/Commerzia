using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class TipoMovimientoInventario
{
    public int IdTipoMovimiento { get; set; }

    public string Nombre { get; set; } = null!;

    public bool EsEntrada { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();
}

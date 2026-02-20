using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class TipoDocumento
{
    public int IdTipoDocumento { get; set; }

    public string Nombre { get; set; } = null!;

    public string? CodigoFiscal { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<Asiento> Asientos { get; set; } = new List<Asiento>();
}

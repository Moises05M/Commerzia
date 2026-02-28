using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class Marca
{
    public int IdMarca { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }
    public int? IdEstado { get; set; }
    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}

using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Cargo { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public DateOnly? FechaContratacion { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<DevolucionVentum> DevolucionVenta { get; set; } = new List<DevolucionVentum>();

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}

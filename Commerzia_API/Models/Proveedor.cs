using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string RazonSocial { get; set; } = null!;

    public string? DocumentoIdentidad { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

    public virtual Estado? IdEstadoNavigation { get; set; }
}

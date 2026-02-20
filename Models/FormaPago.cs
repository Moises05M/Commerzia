using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class FormaPago
{
    public int IdFormaPago { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<CobroCliente> CobroClientes { get; set; } = new List<CobroCliente>();

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

    public virtual ICollection<PagoProveedor> PagoProveedors { get; set; } = new List<PagoProveedor>();
}

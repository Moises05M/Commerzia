using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class DevolucionVentum
{
    public int IdDevolucion { get; set; }

    public int IdVenta { get; set; }

    public DateTime? FechaDevolucion { get; set; }

    public string? NumeroNotaCredito { get; set; }

    public string? Motivo { get; set; }

    public decimal TotalDevuelto { get; set; }

    public int? IdEmpleado { get; set; }

    public int? IdAsiento { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<DevolucionVentaDetalle> DevolucionVentaDetalles { get; set; } = new List<DevolucionVentaDetalle>();

    public virtual Asiento? IdAsientoNavigation { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Ventum IdVentaNavigation { get; set; } = null!;

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();
}

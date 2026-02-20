using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class Ventum
{
    public int IdVenta { get; set; }

    public int IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime? FechaVenta { get; set; }

    public string? NumeroComprobante { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Impuestos { get; set; }

    public decimal? Total { get; set; }

    public decimal? SaldoPendiente { get; set; }

    public int? IdEstado { get; set; }

    public int? IdAsiento { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<CobroCliente> CobroClientes { get; set; } = new List<CobroCliente>();

    public virtual ICollection<DevolucionVentum> DevolucionVenta { get; set; } = new List<DevolucionVentum>();

    public virtual Asiento? IdAsientoNavigation { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}

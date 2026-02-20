using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class Asiento
{
    public int IdAsiento { get; set; }

    public string NumeroAsiento { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string? Glosa { get; set; }

    public int IdPeriodo { get; set; }

    public int? IdTipoDocumento { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<AsientoDetalle> AsientoDetalles { get; set; } = new List<AsientoDetalle>();

    public virtual ICollection<CobroCliente> CobroClientes { get; set; } = new List<CobroCliente>();

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<DepreciacionActivo> DepreciacionActivos { get; set; } = new List<DepreciacionActivo>();

    public virtual ICollection<DevolucionVentum> DevolucionVenta { get; set; } = new List<DevolucionVentum>();

    public virtual ICollection<Gasto> Gastos { get; set; } = new List<Gasto>();

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual PeriodoContable IdPeriodoNavigation { get; set; } = null!;

    public virtual TipoDocumento? IdTipoDocumentoNavigation { get; set; }

    public virtual ICollection<PagoProveedor> PagoProveedors { get; set; } = new List<PagoProveedor>();

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}

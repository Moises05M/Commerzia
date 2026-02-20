using System;
using System.Collections.Generic;

namespace Commerzia_API.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string? CodigoSku { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal PrecioVenta { get; set; }

    public decimal? CostoPromedio { get; set; }

    public int? IdMarca { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdEstado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? UsuarioCreacion { get; set; }

    public DateTime? FechaModificacion { get; set; }

    public string? UsuarioModificacion { get; set; }

    public virtual ICollection<CompraDetalle> CompraDetalles { get; set; } = new List<CompraDetalle>();

    public virtual ICollection<DevolucionVentaDetalle> DevolucionVentaDetalles { get; set; } = new List<DevolucionVentaDetalle>();

    public virtual Categorium? IdCategoriaNavigation { get; set; }

    public virtual Estado? IdEstadoNavigation { get; set; }

    public virtual Marca? IdMarcaNavigation { get; set; }

    public virtual ICollection<MovimientoInventario> MovimientoInventarios { get; set; } = new List<MovimientoInventario>();

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}

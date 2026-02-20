using System;
using System.Collections.Generic;
using Commerzia_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Commerzia_API.Data;

public partial class CommerziaContext : IdentityDbContext<IdentityUser>
{
    public CommerziaContext()
    {
    }

    public CommerziaContext(DbContextOptions<CommerziaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivoFijo> ActivoFijos { get; set; }

    public virtual DbSet<Asiento> Asientos { get; set; }

    public virtual DbSet<AsientoDetalle> AsientoDetalles { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<CobroCliente> CobroClientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<CompraDetalle> CompraDetalles { get; set; }

    public virtual DbSet<CuentaContable> CuentaContables { get; set; }

    public virtual DbSet<DepreciacionActivo> DepreciacionActivos { get; set; }

    public virtual DbSet<DevolucionVentaDetalle> DevolucionVentaDetalles { get; set; }

    public virtual DbSet<DevolucionVentum> DevolucionVenta { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<FormaPago> FormaPagos { get; set; }

    public virtual DbSet<Gasto> Gastos { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<MovimientoInventario> MovimientoInventarios { get; set; }

    public virtual DbSet<PagoProveedor> PagoProveedors { get; set; }

    public virtual DbSet<PeriodoContable> PeriodoContables { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoMovimientoInventario> TipoMovimientoInventarios { get; set; }

    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Moises-Mendez\\SQLEXPRESS;Database=Commerzia;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ActivoFijo>(entity =>
        {
            entity.HasKey(e => e.IdActivo).HasName("PK__ActivoFi__146481C0DD3BC655");

            entity.ToTable("ActivoFijo");

            entity.Property(e => e.CodigoActivo).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
            entity.Property(e => e.ValorAdquisicion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorResidual)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCuentaActivoNavigation).WithMany(p => p.ActivoFijoIdCuentaActivoNavigations)
                .HasForeignKey(d => d.IdCuentaActivo)
                .HasConstraintName("FK_Activo_CtaActivo");

            entity.HasOne(d => d.IdCuentaDepreciacionNavigation).WithMany(p => p.ActivoFijoIdCuentaDepreciacionNavigations)
                .HasForeignKey(d => d.IdCuentaDepreciacion)
                .HasConstraintName("FK_Activo_CtaDeprec");

            entity.HasOne(d => d.IdCuentaGastoNavigation).WithMany(p => p.ActivoFijoIdCuentaGastoNavigations)
                .HasForeignKey(d => d.IdCuentaGasto)
                .HasConstraintName("FK_Activo_CtaGasto");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.ActivoFijos)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Activo_Estado");
        });

        modelBuilder.Entity<Asiento>(entity =>
        {
            entity.HasKey(e => e.IdAsiento).HasName("PK__Asiento__F567872146099DEB");

            entity.ToTable("Asiento");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Glosa).HasMaxLength(200);
            entity.Property(e => e.NumeroAsiento).HasMaxLength(20);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Asiento_Estado");

            entity.HasOne(d => d.IdPeriodoNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdPeriodo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asiento_Periodo");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Asientos)
                .HasForeignKey(d => d.IdTipoDocumento)
                .HasConstraintName("FK_Asiento_TipoDoc");
        });

        modelBuilder.Entity<AsientoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdAsientoDetalle).HasName("PK__AsientoD__984DDE920B9D85B0");

            entity.ToTable("AsientoDetalle");

            entity.Property(e => e.Debe)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Haber)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Referencia).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.AsientoDetalles)
                .HasForeignKey(d => d.IdAsiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AsientoDet_Asiento");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.AsientoDetalles)
                .HasForeignKey(d => d.IdCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AsientoDet_Cuenta");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10F365E64F");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D594664229FCDACA");

            entity.ToTable("Cliente");

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.DocumentoIdentidad).HasMaxLength(20);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Cliente_Estado");
        });

        modelBuilder.Entity<CobroCliente>(entity =>
        {
            entity.HasKey(e => e.IdCobro).HasName("PK__CobroCli__D0B0CD00314ABF45");

            entity.ToTable("CobroCliente");

            entity.Property(e => e.FechaCobro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroReferencia).HasMaxLength(50);
            entity.Property(e => e.Observaciones).HasMaxLength(200);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.CobroClientes)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("FK_Cobro_Asiento");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.CobroClientes)
                .HasForeignKey(d => d.IdFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cobro_FormaPago");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.CobroClientes)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cobro_Venta");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compra__0A5CDB5C380B9542");

            entity.ToTable("Compra");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaRecepcion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Impuestos)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroFactura).HasMaxLength(50);
            entity.Property(e => e.SaldoPendiente)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubTotal)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("FK_Compra_Asiento");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Compra_Estado");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compra_Proveedor");
        });

        modelBuilder.Entity<CompraDetalle>(entity =>
        {
            entity.HasKey(e => e.IdCompraDetalle).HasName("PK__CompraDe__A1B840C502553A2E");

            entity.ToTable("CompraDetalle");

            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TotalLinea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.CompraDetalles)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompraDet_Compra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.CompraDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompraDet_Producto");
        });

        modelBuilder.Entity<CuentaContable>(entity =>
        {
            entity.HasKey(e => e.IdCuenta).HasName("PK__CuentaCo__D41FD70665D9FE74");

            entity.ToTable("CuentaContable");

            entity.HasIndex(e => e.Codigo, "UQ__CuentaCo__06370DAC8971D129").IsUnique();

            entity.Property(e => e.Codigo).HasMaxLength(20);
            entity.Property(e => e.EsCuentaMovimiento).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdCuentaPadreNavigation).WithMany(p => p.InverseIdCuentaPadreNavigation)
                .HasForeignKey(d => d.IdCuentaPadre)
                .HasConstraintName("FK_Cuenta_Padre");
        });

        modelBuilder.Entity<DepreciacionActivo>(entity =>
        {
            entity.HasKey(e => e.IdDepreciacion).HasName("PK__Deprecia__64EADB742DAE95ED");

            entity.ToTable("DepreciacionActivo");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MontoDepreciado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.ValorLibroRemanente).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdActivoNavigation).WithMany(p => p.DepreciacionActivos)
                .HasForeignKey(d => d.IdActivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Deprec_Activo");

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.DepreciacionActivos)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("FK_Deprec_Asiento");
        });

        modelBuilder.Entity<DevolucionVentaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDevolucionDetalle).HasName("PK__Devoluci__ADB5A41C94FAD61F");

            entity.ToTable("DevolucionVentaDetalle");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalLinea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);

            entity.HasOne(d => d.IdDevolucionNavigation).WithMany(p => p.DevolucionVentaDetalles)
                .HasForeignKey(d => d.IdDevolucion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DevDet_Devolucion");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DevolucionVentaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DevDet_Producto");

            entity.HasOne(d => d.IdVentaDetalleNavigation).WithMany(p => p.DevolucionVentaDetalles)
                .HasForeignKey(d => d.IdVentaDetalle)
                .HasConstraintName("FK_DevDet_VentaDetalle");
        });

        modelBuilder.Entity<DevolucionVentum>(entity =>
        {
            entity.HasKey(e => e.IdDevolucion).HasName("PK__Devoluci__7B3585A2E46478DB");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaDevolucion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Motivo).HasMaxLength(200);
            entity.Property(e => e.NumeroNotaCredito).HasMaxLength(50);
            entity.Property(e => e.TotalDevuelto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.DevolucionVenta)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("FK_Devolucion_Asiento");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.DevolucionVenta)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK_Devolucion_Empleado");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.DevolucionVenta)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Devolucion_Estado");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DevolucionVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Devolucion_Venta");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E6AF86C6E");

            entity.ToTable("Empleado");

            entity.Property(e => e.Cargo).HasMaxLength(50);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Empleado_Estado");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC19E17FE01");

            entity.ToTable("Estado");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Modulo).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<FormaPago>(entity =>
        {
            entity.HasKey(e => e.IdFormaPago).HasName("PK__FormaPag__C777CA684F96B044");

            entity.ToTable("FormaPago");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<Gasto>(entity =>
        {
            entity.HasKey(e => e.IdGasto).HasName("PK__Gasto__C630244D597F453B");

            entity.ToTable("Gasto");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaGasto).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroComprobante).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("FK_Gasto_Asiento");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Gasto_Estado");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.IdFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Gasto_FormaPago");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Gastos)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_Gasto_Proveedor");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A887429E9CF9");

            entity.ToTable("Marca");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<MovimientoInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__881A6AE0FB64E25D");

            entity.ToTable("MovimientoInventario");

            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Fecha).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.SaldoCostoTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK_MovInv_Compra");

            entity.HasOne(d => d.IdDevolucionNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdDevolucion)
                .HasConstraintName("FK_MovInv_Devolucion");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovInv_Producto");

            entity.HasOne(d => d.IdTipoMovimientoNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdTipoMovimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovInv_TipoMov");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.MovimientoInventarios)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK_MovInv_Venta");
        });

        modelBuilder.Entity<PagoProveedor>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__PagoProv__FC851A3ADAB2C9A9");

            entity.ToTable("PagoProveedor");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaPago).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroReferencia).HasMaxLength(50);
            entity.Property(e => e.Observaciones).HasMaxLength(200);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.PagoProveedors)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("FK_PagoProv_Asiento");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.PagoProveedors)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PagoProv_Compra");

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.PagoProveedors)
                .HasForeignKey(d => d.IdFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PagoProv_FormaPago");
        });

        modelBuilder.Entity<PeriodoContable>(entity =>
        {
            entity.HasKey(e => e.IdPeriodo).HasName("PK__PeriodoC__B44699FE534B3628");

            entity.ToTable("PeriodoContable");

            entity.Property(e => e.Cerrado).HasDefaultValue(false);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210931CA8C5");

            entity.ToTable("Producto");

            entity.HasIndex(e => e.CodigoSku, "UQ__Producto__F02F03F986E8036F").IsUnique();

            entity.Property(e => e.CodigoSku)
                .HasMaxLength(50)
                .HasColumnName("CodigoSKU");
            entity.Property(e => e.CostoPromedio)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Producto_Categoria");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Producto_Estado");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK_Producto_Marca");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__E8B631AFA3EC6526");

            entity.ToTable("Proveedor");

            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.DocumentoIdentidad).HasMaxLength(20);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.RazonSocial).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Proveedors)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Proveedor_Estado");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK__TipoDocu__3AB3332F235FE0C8");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.CodigoFiscal).HasMaxLength(20);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<TipoMovimientoInventario>(entity =>
        {
            entity.HasKey(e => e.IdTipoMovimiento).HasName("PK__TipoMovi__820D7FC282BEEF2E");

            entity.ToTable("TipoMovimientoInventario");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);
        });

        modelBuilder.Entity<VentaDetalle>(entity =>
        {
            entity.HasKey(e => e.IdVentaDetalle).HasName("PK__VentaDet__2787211D5A62EC0F");

            entity.ToTable("VentaDetalle");

            entity.Property(e => e.Descuento)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalLinea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VentaDet_Producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VentaDet_Venta");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__BC1240BD74160A58");

            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FechaVenta).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Impuestos).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NumeroComprobante).HasMaxLength(50);
            entity.Property(e => e.SaldoPendiente)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioCreacion).HasMaxLength(50);
            entity.Property(e => e.UsuarioModificacion).HasMaxLength(50);

            entity.HasOne(d => d.IdAsientoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdAsiento)
                .HasConstraintName("FK_Venta_Asiento");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Venta_Cliente");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Venta_Empleado");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK_Venta_Estado");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

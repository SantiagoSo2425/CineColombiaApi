using CineColombiaApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace apiSIA.Models;

public partial class CineColombiaContext : DbContext
{
    public CineColombiaContext()
    {
    }

    public CineColombiaContext(DbContextOptions<CineColombiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Boletica> Boleticas { get; set; }

    public virtual DbSet<BoleticaSilla> BoleticaSillas { get; set; }

    public virtual DbSet<Clasificacion> Clasificacions { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Formato> Formatos { get; set; }

    public virtual DbSet<Funcion> Funcions { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Silla> Sillas { get; set; }

    public virtual DbSet<TarjetaFidelizacion> TarjetaFidelizacions { get; set; }

    public virtual DbSet<Teatro> Teatros { get; set; }

    public virtual DbSet<TipoCliente> TipoClientes { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoSala> TipoSalas { get; set; }

    public virtual DbSet<TipoSilla> TipoSillas { get; set; }

    public virtual DbSet<UsuarioSistema> UsuarioSistemas { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

   /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CineColombia;Trusted_Connection=True;TrustServerCertificate=True;");
   */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Boletica>(entity =>
        {
            entity.HasKey(e => e.IdBoletica).HasName("PK__BOLETICA__DBAF297938D62F8F");

            entity.ToTable("BOLETICA");

            entity.Property(e => e.IdBoletica).HasColumnName("id_boletica");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.IdFuncion).HasColumnName("id_funcion");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");

            /* entity.HasOne(d => d.IdFuncionNavigation).WithMany(p => p.Boleticas)
                .HasForeignKey(d => d.IdFuncion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BOLETICA__id_fun__17F790F9");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Boleticas)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BOLETICA__id_ven__17036CC0");
            */
        });

        modelBuilder.Entity<BoleticaSilla>(entity =>
        {
            entity.HasKey(e => e.IdBoleticaSilla).HasName("PK__BOLETICA__A7B5BD1B00E8F210");

            entity.ToTable("BOLETICA_SILLA");

            entity.HasIndex(e => new { e.IdBoletica, e.IdSilla }, "UQ_BOLETICA_SILLA").IsUnique();

            entity.Property(e => e.IdBoleticaSilla).HasColumnName("id_boletica_silla");
            entity.Property(e => e.Descuento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.IdBoletica).HasColumnName("id_boletica");
            entity.Property(e => e.IdSilla).HasColumnName("id_silla");
            entity.Property(e => e.PrecioFinal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_final");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_unitario");

            /* entity.HasOne(d => d.IdBoleticaNavigation).WithMany(p => p.BoleticaSillas)
                 .HasForeignKey(d => d.IdBoletica)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__BOLETICA___id_bo__1CBC4616");

             entity.HasOne(d => d.IdSillaNavigation).WithMany(p => p.BoleticaSillas)
                 .HasForeignKey(d => d.IdSilla)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__BOLETICA___id_si__1DB06A4F");
            */
        });

        modelBuilder.Entity<Clasificacion>(entity =>
        {
            entity.HasKey(e => e.IdClasificacion).HasName("PK__CLASIFIC__257A4E1BD8F6B854");

            entity.ToTable("CLASIFICACION");

            entity.Property(e => e.IdClasificacion)
                .ValueGeneratedNever()
                .HasColumnName("id_clasificacion");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__677F38F5AA53C2FD");

            entity.ToTable("CLIENTE");

            entity.HasIndex(e => new { e.IdTipoDoc, e.NumDocumento }, "UQ_CLIENTE").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.IdTipoCliente).HasColumnName("id_tipo_cliente");
            entity.Property(e => e.IdTipoDoc).HasColumnName("id_tipo_doc");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("num_documento");

            /* entity.HasOne(d => d.IdTipoClienteNavigation).WithMany(p => p.Clientes)
                 .HasForeignKey(d => d.IdTipoCliente)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__CLIENTE__id_tipo__778AC167");

             entity.HasOne(d => d.IdTipoDocNavigation).WithMany(p => p.Clientes)
                 .HasForeignKey(d => d.IdTipoDoc)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__CLIENTE__id_tipo__787EE5A0");
            */
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__EMPLEADO__88B513944E9E3970");

            entity.ToTable("EMPLEADO");

            entity.HasIndex(e => new { e.IdTipoDoc, e.NumDocumento }, "UQ_EMPLEADO").IsUnique();

            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.IdTeatro).HasColumnName("id_teatro");
            entity.Property(e => e.IdTipoDoc).HasColumnName("id_tipo_doc");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("num_documento");

            /* entity.HasOne(d => d.IdTeatroNavigation).WithMany(p => p.Empleados)
                 .HasForeignKey(d => d.IdTeatro)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__EMPLEADO__id_tea__03F0984C");


             entity.HasOne(d => d.IdTipoDocNavigation).WithMany(p => p.Empleados)
                 .HasForeignKey(d => d.IdTipoDoc)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__EMPLEADO__id_tip__04E4BC85");
            */
        });

        modelBuilder.Entity<Formato>(entity =>
        {
            entity.HasKey(e => e.IdFormato).HasName("PK__FORMATO__8C0C34B7B8322F79");

            entity.ToTable("FORMATO");

            entity.Property(e => e.IdFormato)
                .ValueGeneratedNever()
                .HasColumnName("id_formato");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Funcion>(entity =>
        {
            entity.HasKey(e => e.IdFuncion).HasName("PK__FUNCION__F6DE6FC9F03EECA8");

            entity.ToTable("FUNCION");

            entity.HasIndex(e => new { e.IdSala, e.FechaFuncion, e.HoraInicio }, "UQ_FUNCION").IsUnique();

            entity.Property(e => e.IdFuncion).HasColumnName("id_funcion");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaFuncion).HasColumnName("fecha_funcion");
            entity.Property(e => e.HoraFin)
                .HasColumnType("datetime")
                .HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("hora_inicio");
            entity.Property(e => e.IdFormato).HasColumnName("id_formato");
            entity.Property(e => e.IdIdioma).HasColumnName("id_idioma");
            entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");
            entity.Property(e => e.IdSala).HasColumnName("id_sala");
            entity.Property(e => e.PrecioBase)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_base");

            /* entity.HasOne(d => d.IdFormatoNavigation).WithMany(p => p.Funcions)
                 .HasForeignKey(d => d.IdFormato)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__FUNCION__id_form__72C60C4A");

             entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.Funcions)
                 .HasForeignKey(d => d.IdIdioma)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__FUNCION__id_idio__71D1E811");

             entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Funcions)
                 .HasForeignKey(d => d.IdPelicula)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__FUNCION__id_peli__70DDC3D8");

             entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Funcions)
                 .HasForeignKey(d => d.IdSala)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__FUNCION__id_sala__6FE99F9F");
            */
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__GENERO__99A8E4F928A151FF");

            entity.ToTable("GENERO");

            entity.Property(e => e.IdGenero)
                .ValueGeneratedNever()
                .HasColumnName("id_genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.IdIdioma).HasName("PK__IDIOMA__0BA1525F9F908952");

            entity.ToTable("IDIOMA");

            entity.Property(e => e.IdIdioma)
                .ValueGeneratedNever()
                .HasColumnName("id_idioma");
            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__METODO_P__85BE0EBC41B77C5E");

            entity.ToTable("METODO_PAGO");

            entity.Property(e => e.IdMetodoPago)
                .ValueGeneratedNever()
                .HasColumnName("id_metodo_pago");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("PK__PELICULA__B5017F4DD40B7874");

            entity.ToTable("PELICULA");

            entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");
            entity.Property(e => e.AnioEstreno).HasColumnName("anio_estreno");
            entity.Property(e => e.DuracionMin).HasColumnName("duracion_min");
            entity.Property(e => e.IdClasificacion).HasColumnName("id_clasificacion");
            entity.Property(e => e.IdGenero).HasColumnName("id_genero");
            entity.Property(e => e.NombreOferta)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre_oferta");
            entity.Property(e => e.Resumen)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("resumen");
            entity.Property(e => e.TituloOriginal)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("titulo_original");
            entity.Property(e => e.TrailerLink)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("trailer_link");

            /*  entity.HasOne(d => d.IdClasificacionNavigation).WithMany(p => p.Peliculas)
                  .HasForeignKey(d => d.IdClasificacion)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__PELICULA__id_cla__6C190EBB");

              entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Peliculas)
                  .HasForeignKey(d => d.IdGenero)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__PELICULA__id_gen__6B24EA82");
            */
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__6ABCB5E0CA51B5C4");

            entity.ToTable("ROL");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("PK__SALA__D18B015BC1E2FC80");

            entity.ToTable("SALA");

            entity.Property(e => e.IdSala).HasColumnName("id_sala");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.CapacidadTotal).HasColumnName("capacidad_total");
            entity.Property(e => e.IdTeatro).HasColumnName("id_teatro");
            entity.Property(e => e.IdTipoSala).HasColumnName("id_tipo_sala");
            entity.Property(e => e.NombreSala)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_sala");

            /*  entity.HasOne(d => d.IdTeatroNavigation).WithMany(p => p.Salas)
                  .HasForeignKey(d => d.IdTeatro)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__SALA__id_teatro__5CD6CB2B");

              entity.HasOne(d => d.IdTipoSalaNavigation).WithMany(p => p.Salas)
                  .HasForeignKey(d => d.IdTipoSala)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK__SALA__id_tipo_sa__5DCAEF64");
            */
        });

        modelBuilder.Entity<Silla>(entity =>
        {
            entity.HasKey(e => e.IdSilla).HasName("PK__SILLA__223FEA6BEE0FE398");

            entity.ToTable("SILLA");

            entity.HasIndex(e => new { e.IdSala, e.Fila, e.Numero }, "UQ_SILLA").IsUnique();

            entity.Property(e => e.IdSilla).HasColumnName("id_silla");
            entity.Property(e => e.Estado)
                .HasDefaultValue(1)
                .HasColumnName("estado");
            entity.Property(e => e.Fila)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("fila");
            entity.Property(e => e.IdSala).HasColumnName("id_sala");
            entity.Property(e => e.IdTipoSilla).HasColumnName("id_tipo_silla");
            entity.Property(e => e.Numero).HasColumnName("numero");

            /* entity.HasOne(d => d.IdSalaNavigation).WithMany(p => p.Sillas)
                 .HasForeignKey(d => d.IdSala)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__SILLA__id_sala__628FA481");

             entity.HasOne(d => d.IdTipoSillaNavigation).WithMany(p => p.Sillas)
                 .HasForeignKey(d => d.IdTipoSilla)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__SILLA__id_tipo_s__6383C8BA");
            */
        });

        modelBuilder.Entity<TarjetaFidelizacion>(entity =>
        {
            entity.HasKey(e => e.IdTarjeta).HasName("PK__TARJETA___E92BCFEA881CB32F");

            entity.ToTable("TARJETA_FIDELIZACION");

            entity.HasIndex(e => e.IdCliente, "UQ_TARJETA").IsUnique();

            entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta");
            entity.Property(e => e.DescuentoPorcentaje)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("descuento_porcentaje");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaEmision).HasColumnName("fecha_emision");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_tarjeta");
            entity.Property(e => e.PuntosAcumulados)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("puntos_acumulados");

            /* entity.HasOne(d => d.IdClienteNavigation).WithOne(p => p.TarjetaFidelizacion)
                 .HasForeignKey<TarjetaFidelizacion>(d => d.IdCliente)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__TARJETA_F__id_cl__7D439ABD");
            */
        });

        modelBuilder.Entity<Teatro>(entity =>
        {
            entity.HasKey(e => e.IdTeatro).HasName("PK__TEATRO__B34DF8E59F78F712");

            entity.ToTable("TEATRO");

            entity.Property(e => e.IdTeatro).HasColumnName("id_teatro");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoCliente>(entity =>
        {
            entity.HasKey(e => e.IdTipoCliente).HasName("PK__TIPO_CLI__69D671C543547835");

            entity.ToTable("TIPO_CLIENTE");

            entity.Property(e => e.IdTipoCliente)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_cliente");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDoc).HasName("PK__TIPO_DOC__B0A524EA7B0347D8");

            entity.ToTable("TIPO_DOCUMENTO");

            entity.Property(e => e.IdTipoDoc)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_doc");
            entity.Property(e => e.Codigo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoSala>(entity =>
        {
            entity.HasKey(e => e.IdTipoSala).HasName("PK__TIPO_SAL__1C51F013F2ECE55A");

            entity.ToTable("TIPO_SALA");

            entity.Property(e => e.IdTipoSala)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_sala");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoSilla>(entity =>
        {
            entity.HasKey(e => e.IdTipoSilla).HasName("PK__TIPO_SIL__245C2E3487516069");

            entity.ToTable("TIPO_SILLA");

            entity.Property(e => e.IdTipoSilla)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_silla");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioBase)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio_base");
        });

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO___4E3E04AD1AF3D443");

            entity.ToTable("USUARIO_SISTEMA");

            entity.HasIndex(e => e.Username, "UQ_USERNAME").IsUnique();

            entity.HasIndex(e => e.IdEmpleado, "UQ_USUARIO").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.UltimoLogin)
                .HasColumnType("datetime")
                .HasColumnName("ultimo_login");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            /* entity.HasOne(d => d.IdEmpleadoNavigation).WithOne(p => p.UsuarioSistema)
                 .HasForeignKey<UsuarioSistema>(d => d.IdEmpleado)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__USUARIO_S__id_em__0A9D95DB");

             entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UsuarioSistemas)
                 .HasForeignKey(d => d.IdRol)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__USUARIO_S__id_ro__0B91BA14");
            */
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__VENTA__459533BF887C12DC");

            entity.ToTable("VENTA");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHora)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdMetodoPago).HasColumnName("id_metodo_pago");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.TotalDescuento)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_descuento");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_venta");

            /* entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                 .HasForeignKey(d => d.IdCliente)
                 .HasConstraintName("FK__VENTA__id_client__0F624AF8");

             entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Venta)
                 .HasForeignKey(d => d.IdEmpleado)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__VENTA__id_emplea__10566F31");

             entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Venta)
                 .HasForeignKey(d => d.IdMetodoPago)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__VENTA__id_metodo__114A936A");
            */
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

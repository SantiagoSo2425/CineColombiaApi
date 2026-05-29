using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CineColombiaApi.Models;

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

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Clasificacion> Clasificacions { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<DireccionCliente> DireccionClientes { get; set; }

    public virtual DbSet<DireccionEmpleado> DireccionEmpleados { get; set; }

    public virtual DbSet<Distribuidora> Distribuidoras { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EmpleadoProfesion> EmpleadoProfesions { get; set; }

    public virtual DbSet<Formato> Formatos { get; set; }

    public virtual DbSet<Funcion> Funcions { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Pelicula> Peliculas { get; set; }

    public virtual DbSet<PeliculaDistribuidora> PeliculaDistribuidoras { get; set; }

    public virtual DbSet<PeliculaFormato> PeliculaFormatos { get; set; }

    public virtual DbSet<PeliculaIdioma> PeliculaIdiomas { get; set; }

    public virtual DbSet<PeliculaProductora> PeliculaProductoras { get; set; }

    public virtual DbSet<Productora> Productoras { get; set; }

    public virtual DbSet<Profesion> Profesions { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Silla> Sillas { get; set; }

    public virtual DbSet<TarjetaFidelizacion> TarjetaFidelizacions { get; set; }

    public virtual DbSet<Teatro> Teatros { get; set; }

    public virtual DbSet<TelefonoCliente> TelefonoClientes { get; set; }

    public virtual DbSet<TelefonoEmpleado> TelefonoEmpleados { get; set; }

    public virtual DbSet<TipoCliente> TipoClientes { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoSala> TipoSalas { get; set; }

    public virtual DbSet<TipoSilla> TipoSillas { get; set; }

    public virtual DbSet<TipoTelefono> TipoTelefonos { get; set; }

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
            entity.HasKey(e => e.IdBoletica).HasName("PK__BOLETICA__DBAF29793A38A4A1");

            entity.ToTable("BOLETICA");

            entity.Property(e => e.IdBoletica)
                .ValueGeneratedNever()
                .HasColumnName("id_boletica");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdFuncion).HasColumnName("id_funcion");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");

        });

        modelBuilder.Entity<BoleticaSilla>(entity =>
        {
            entity.HasKey(e => e.IdBoleticaSilla).HasName("PK__BOLETICA__A7B5BD1BF89FF8D2");

            entity.ToTable("BOLETICA_SILLA");

            entity.HasIndex(e => new { e.IdBoletica, e.IdSilla }, "BOLETICA_SILLA_index_12").IsUnique();

            entity.Property(e => e.IdBoleticaSilla)
                .ValueGeneratedNever()
                .HasColumnName("id_boletica_silla");
            entity.Property(e => e.Descuento)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("descuento");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdBoletica).HasColumnName("id_boletica");
            entity.Property(e => e.IdSilla).HasColumnName("id_silla");
            entity.Property(e => e.PrecioFinal)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio_final");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio_unitario");

        });

        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK__CIUDAD__B7DC4CD54DC45FFC");

            entity.ToTable("CIUDAD");

            entity.Property(e => e.IdCiudad)
                .ValueGeneratedNever()
                .HasColumnName("id_ciudad");
            entity.Property(e => e.IdDepartamento).HasColumnName("id_departamento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

        });

        modelBuilder.Entity<Clasificacion>(entity =>
        {
            entity.HasKey(e => e.IdClasificacion).HasName("PK__CLASIFIC__257A4E1B485CE888");

            entity.ToTable("CLASIFICACION");

            entity.Property(e => e.IdClasificacion)
                .ValueGeneratedNever()
                .HasColumnName("id_clasificacion");
            entity.Property(e => e.Codigo)
                .HasMaxLength(255)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTE__677F38F59EB54FA6");

            entity.ToTable("CLIENTE");

            entity.HasIndex(e => new { e.IdTipoDoc, e.NumDocumento }, "CLIENTE_index_6").IsUnique();

            entity.Property(e => e.IdCliente)
                .ValueGeneratedNever()
                .HasColumnName("id_cliente");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(255)
                .HasColumnName("apellidos");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdTipoCliente).HasColumnName("id_tipo_cliente");
            entity.Property(e => e.IdTipoDoc).HasColumnName("id_tipo_doc");
            entity.Property(e => e.Nombres)
                .HasMaxLength(255)
                .HasColumnName("nombres");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(255)
                .HasColumnName("num_documento");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__DEPARTAM__64F37A16FED3248F");

            entity.ToTable("DEPARTAMENTO");

            entity.Property(e => e.IdDepartamento)
                .ValueGeneratedNever()
                .HasColumnName("id_departamento");
            entity.Property(e => e.IdPais).HasColumnName("id_pais");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

        });

        modelBuilder.Entity<DireccionCliente>(entity =>
        {
            entity.HasKey(e => e.IdDireccionCli).HasName("PK__DIRECCIO__89EF21EAA8EC43CB");

            entity.ToTable("DIRECCION_CLIENTE");

            entity.Property(e => e.IdDireccionCli)
                .ValueGeneratedNever()
                .HasColumnName("id_direccion_cli");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

        });

        modelBuilder.Entity<DireccionEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdDireccionEmp).HasName("PK__DIRECCIO__89633DF074D18DF0");

            entity.ToTable("DIRECCION_EMPLEADO");

            entity.Property(e => e.IdDireccionEmp)
                .ValueGeneratedNever()
                .HasColumnName("id_direccion_emp");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

        });

        modelBuilder.Entity<Distribuidora>(entity =>
        {
            entity.HasKey(e => e.IdDistribuidora).HasName("PK__DISTRIBU__207487ACE34449A4");

            entity.ToTable("DISTRIBUIDORA");

            entity.Property(e => e.IdDistribuidora)
                .ValueGeneratedNever()
                .HasColumnName("id_distribuidora");
            entity.Property(e => e.IdPais).HasColumnName("id_pais");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__EMPLEADO__88B51394B0FE0CED");

            entity.ToTable("EMPLEADO");

            entity.HasIndex(e => new { e.IdTipoDoc, e.NumDocumento }, "EMPLEADO_index_8").IsUnique();

            entity.HasIndex(e => e.CodigoEmpleado, "UQ__EMPLEADO__CDEF1DDF273366BA").IsUnique();

            entity.Property(e => e.IdEmpleado)
                .ValueGeneratedNever()
                .HasColumnName("id_empleado");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(255)
                .HasColumnName("apellidos");
            entity.Property(e => e.CodigoEmpleado)
                .HasMaxLength(255)
                .HasColumnName("codigo_empleado");
            entity.Property(e => e.FechaIngreso).HasColumnName("fecha_ingreso");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdTeatro).HasColumnName("id_teatro");
            entity.Property(e => e.IdTipoDoc).HasColumnName("id_tipo_doc");
            entity.Property(e => e.Nombres)
                .HasMaxLength(255)
                .HasColumnName("nombres");
            entity.Property(e => e.NumDocumento)
                .HasMaxLength(255)
                .HasColumnName("num_documento");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

        });

        modelBuilder.Entity<EmpleadoProfesion>(entity =>
        {
            entity.HasKey(e => e.IdEmpProfesion).HasName("PK__EMPLEADO__EAB3E774D5A59793");

            entity.ToTable("EMPLEADO_PROFESION");

            entity.HasIndex(e => new { e.IdEmpleado, e.IdProfesion }, "EMPLEADO_PROFESION_index_9").IsUnique();

            entity.Property(e => e.IdEmpProfesion)
                .ValueGeneratedNever()
                .HasColumnName("id_emp_profesion");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdProfesion).HasColumnName("id_profesion");

        });

        modelBuilder.Entity<Formato>(entity =>
        {
            entity.HasKey(e => e.IdFormato).HasName("PK__FORMATO__8C0C34B7CFAF7851");

            entity.ToTable("FORMATO");

            entity.Property(e => e.IdFormato)
                .ValueGeneratedNever()
                .HasColumnName("id_formato");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Funcion>(entity =>
        {
            entity.HasKey(e => e.IdFuncion).HasName("PK__FUNCION__F6DE6FC9F904C986");

            entity.ToTable("FUNCION");

            entity.HasIndex(e => new { e.IdSala, e.FechaFuncion, e.HoraInicio }, "FUNCION_index_5").IsUnique();

            entity.Property(e => e.IdFuncion)
                .ValueGeneratedNever()
                .HasColumnName("id_funcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaFuncion).HasColumnName("fecha_funcion");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
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
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio_base");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__GENERO__99A8E4F921A276C0");

            entity.ToTable("GENERO");

            entity.Property(e => e.IdGenero)
                .ValueGeneratedNever()
                .HasColumnName("id_genero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.IdIdioma).HasName("PK__IDIOMA__0BA1525FD74610C4");

            entity.ToTable("IDIOMA");

            entity.Property(e => e.IdIdioma)
                .ValueGeneratedNever()
                .HasColumnName("id_idioma");
            entity.Property(e => e.Codigo)
                .HasMaxLength(255)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__METODO_P__85BE0EBCB9FAB6BE");

            entity.ToTable("METODO_PAGO");

            entity.Property(e => e.IdMetodoPago)
                .ValueGeneratedNever()
                .HasColumnName("id_metodo_pago");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__PAIS__0941A3A7765CA89D");

            entity.ToTable("PAIS");

            entity.Property(e => e.IdPais)
                .ValueGeneratedNever()
                .HasColumnName("id_pais");
            entity.Property(e => e.Codigo)
                .HasMaxLength(255)
                .HasColumnName("codigo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.IdPelicula).HasName("PK__PELICULA__B5017F4D8D7B1345");

            entity.ToTable("PELICULA");

            entity.Property(e => e.IdPelicula)
                .ValueGeneratedNever()
                .HasColumnName("id_pelicula");
            entity.Property(e => e.AnioEstreno).HasColumnName("anio_estreno");
            entity.Property(e => e.DuracionMin).HasColumnName("duracion_min");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdClasificacion).HasColumnName("id_clasificacion");
            entity.Property(e => e.IdGenero).HasColumnName("id_genero");
            entity.Property(e => e.NombreOferta)
                .HasMaxLength(255)
                .HasColumnName("nombre_oferta");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");
            entity.Property(e => e.Resumen)
                .HasMaxLength(255)
                .HasColumnName("resumen");
            entity.Property(e => e.TituloOriginal)
                .HasMaxLength(255)
                .HasColumnName("titulo_original");
            entity.Property(e => e.TrailerLink)
                .HasMaxLength(255)
                .HasColumnName("trailer_link");

        });

        modelBuilder.Entity<PeliculaDistribuidora>(entity =>
        {
            entity.HasKey(e => e.IdPeliculaDistribuidora).HasName("PK__PELICULA__B5329463105DA791");

            entity.ToTable("PELICULA_DISTRIBUIDORA");

            entity.HasIndex(e => new { e.IdPelicula, e.IdDistribuidora }, "PELICULA_DISTRIBUIDORA_index_4").IsUnique();

            entity.Property(e => e.IdPeliculaDistribuidora)
                .ValueGeneratedNever()
                .HasColumnName("id_pelicula_distribuidora");
            entity.Property(e => e.IdDistribuidora).HasColumnName("id_distribuidora");
            entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");

        });

        modelBuilder.Entity<PeliculaFormato>(entity =>
        {
            entity.HasKey(e => e.IdPeliculaFormato).HasName("PK__PELICULA__21CCCA7BF275EE57");

            entity.ToTable("PELICULA_FORMATO");

            entity.HasIndex(e => new { e.IdPelicula, e.IdFormato }, "PELICULA_FORMATO_index_2").IsUnique();

            entity.Property(e => e.IdPeliculaFormato)
                .ValueGeneratedNever()
                .HasColumnName("id_pelicula_formato");
            entity.Property(e => e.IdFormato).HasColumnName("id_formato");
            entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");

        });

        modelBuilder.Entity<PeliculaIdioma>(entity =>
        {
            entity.HasKey(e => e.IdPeliculaIdioma).HasName("PK__PELICULA__C2D009BE27815F13");

            entity.ToTable("PELICULA_IDIOMA");

            entity.HasIndex(e => new { e.IdPelicula, e.IdIdioma }, "PELICULA_IDIOMA_index_1").IsUnique();

            entity.Property(e => e.IdPeliculaIdioma)
                .ValueGeneratedNever()
                .HasColumnName("id_pelicula_idioma");
            entity.Property(e => e.EsOriginal).HasColumnName("es_original");
            entity.Property(e => e.IdIdioma).HasColumnName("id_idioma");
            entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");

        });

        modelBuilder.Entity<PeliculaProductora>(entity =>
        {
            entity.HasKey(e => e.IdPeliculaProductora).HasName("PK__PELICULA__C2308A63CB02B727");

            entity.ToTable("PELICULA_PRODUCTORA");

            entity.HasIndex(e => new { e.IdPelicula, e.IdProductora }, "PELICULA_PRODUCTORA_index_3").IsUnique();

            entity.Property(e => e.IdPeliculaProductora)
                .ValueGeneratedNever()
                .HasColumnName("id_pelicula_productora");
            entity.Property(e => e.IdPelicula).HasColumnName("id_pelicula");
            entity.Property(e => e.IdProductora).HasColumnName("id_productora");

        });

        modelBuilder.Entity<Productora>(entity =>
        {
            entity.HasKey(e => e.IdProductora).HasName("PK__PRODUCTO__0E4350475B30AEAF");

            entity.ToTable("PRODUCTORA");

            entity.Property(e => e.IdProductora)
                .ValueGeneratedNever()
                .HasColumnName("id_productora");
            entity.Property(e => e.IdPais).HasColumnName("id_pais");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");

        });

        modelBuilder.Entity<Profesion>(entity =>
        {
            entity.HasKey(e => e.IdProfesion).HasName("PK__PROFESIO__6D67E978FF961CB0");

            entity.ToTable("PROFESION");

            entity.Property(e => e.IdProfesion)
                .ValueGeneratedNever()
                .HasColumnName("id_profesion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROL__6ABCB5E0C03DE524");

            entity.ToTable("ROL");

            entity.Property(e => e.IdRol)
                .ValueGeneratedNever()
                .HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("PK__SALA__D18B015B6F61F211");

            entity.ToTable("SALA");

            entity.Property(e => e.IdSala)
                .ValueGeneratedNever()
                .HasColumnName("id_sala");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.CapacidadTotal).HasColumnName("capacidad_total");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdTeatro).HasColumnName("id_teatro");
            entity.Property(e => e.IdTipoSala).HasColumnName("id_tipo_sala");
            entity.Property(e => e.NombreSala)
                .HasMaxLength(255)
                .HasColumnName("nombre_sala");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

        });

        modelBuilder.Entity<Silla>(entity =>
        {
            entity.HasKey(e => e.IdSilla).HasName("PK__SILLA__223FEA6B0A574622");

            entity.ToTable("SILLA");

            entity.HasIndex(e => new { e.IdSala, e.Fila, e.Numero }, "SILLA_index_0").IsUnique();

            entity.Property(e => e.IdSilla)
                .ValueGeneratedNever()
                .HasColumnName("id_silla");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.Fila)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("fila");
            entity.Property(e => e.IdSala).HasColumnName("id_sala");
            entity.Property(e => e.IdTipoSilla).HasColumnName("id_tipo_silla");
            entity.Property(e => e.Numero).HasColumnName("numero");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

        });

        modelBuilder.Entity<TarjetaFidelizacion>(entity =>
        {
            entity.HasKey(e => e.IdTarjeta).HasName("PK__TARJETA___E92BCFEA3E659F42");

            entity.ToTable("TARJETA_FIDELIZACION");

            entity.HasIndex(e => e.IdCliente, "TARJETA_FIDELIZACION_index_7").IsUnique();

            entity.Property(e => e.IdTarjeta)
                .ValueGeneratedNever()
                .HasColumnName("id_tarjeta");
            entity.Property(e => e.DescuentoPorcentaje)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("descuento_porcentaje");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaEmision).HasColumnName("fecha_emision");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.FechaVencimiento).HasColumnName("fecha_vencimiento");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.NumeroTarjeta)
                .HasMaxLength(255)
                .HasColumnName("numero_tarjeta");
            entity.Property(e => e.PuntosAcumulados)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("puntos_acumulados");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");

        });

        modelBuilder.Entity<Teatro>(entity =>
        {
            entity.HasKey(e => e.IdTeatro).HasName("PK__TEATRO__B34DF8E56A212A08");

            entity.ToTable("TEATRO");

            entity.Property(e => e.IdTeatro)
                .ValueGeneratedNever()
                .HasColumnName("id_teatro");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdCiudad).HasColumnName("id_ciudad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");
            entity.Property(e => e.Telefono)
                .HasMaxLength(255)
                .HasColumnName("telefono");

        });

        modelBuilder.Entity<TelefonoCliente>(entity =>
        {
            entity.HasKey(e => e.IdTelefono).HasName("PK__TELEFONO__28CD68022586C3CF");

            entity.ToTable("TELEFONO_CLIENTE");

            entity.Property(e => e.IdTelefono)
                .ValueGeneratedNever()
                .HasColumnName("id_telefono");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdTipoTelefono).HasColumnName("id_tipo_telefono");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .HasColumnName("numero");

        });

        modelBuilder.Entity<TelefonoEmpleado>(entity =>
        {
            entity.HasKey(e => e.IdTelefonoEmp).HasName("PK__TELEFONO__DBC0D8B90CA53081");

            entity.ToTable("TELEFONO_EMPLEADO");

            entity.Property(e => e.IdTelefonoEmp)
                .ValueGeneratedNever()
                .HasColumnName("id_telefono_emp");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdTipoTelefono).HasColumnName("id_tipo_telefono");
            entity.Property(e => e.Numero)
                .HasMaxLength(255)
                .HasColumnName("numero");

        });

        modelBuilder.Entity<TipoCliente>(entity =>
        {
            entity.HasKey(e => e.IdTipoCliente).HasName("PK__TIPO_CLI__69D671C5A2EC691C");

            entity.ToTable("TIPO_CLIENTE");

            entity.Property(e => e.IdTipoCliente)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_cliente");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDoc).HasName("PK__TIPO_DOC__B0A524EAA15FBA9C");

            entity.ToTable("TIPO_DOCUMENTO");

            entity.Property(e => e.IdTipoDoc)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_doc");
            entity.Property(e => e.Codigo)
                .HasMaxLength(255)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoSala>(entity =>
        {
            entity.HasKey(e => e.IdTipoSala).HasName("PK__TIPO_SAL__1C51F01335EC0399");

            entity.ToTable("TIPO_SALA");

            entity.Property(e => e.IdTipoSala)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_sala");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoSilla>(entity =>
        {
            entity.HasKey(e => e.IdTipoSilla).HasName("PK__TIPO_SIL__245C2E34A1F39F23");

            entity.ToTable("TIPO_SILLA");

            entity.Property(e => e.IdTipoSilla)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_silla");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioBase)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precio_base");
        });

        modelBuilder.Entity<TipoTelefono>(entity =>
        {
            entity.HasKey(e => e.IdTipoTelefono).HasName("PK__TIPO_TEL__69C5933FC5C16072");

            entity.ToTable("TIPO_TELEFONO");

            entity.Property(e => e.IdTipoTelefono)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_telefono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<UsuarioSistema>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIO___4E3E04ADB25BB3A0");

            entity.ToTable("USUARIO_SISTEMA");

            entity.HasIndex(e => e.IdEmpleado, "USUARIO_SISTEMA_index_10").IsUnique();

            entity.HasIndex(e => e.Username, "USUARIO_SISTEMA_index_11").IsUnique();

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.RegistradoPor).HasColumnName("registrado_por");
            entity.Property(e => e.UltimoLogin)
                .HasColumnType("datetime")
                .HasColumnName("ultimo_login");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");

        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__VENTA__459533BF1CE03120");

            entity.ToTable("VENTA");

            entity.Property(e => e.IdVenta)
                .ValueGeneratedNever()
                .HasColumnName("id_venta");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdMetodoPago).HasColumnName("id_metodo_pago");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("subtotal");
            entity.Property(e => e.TotalDescuento)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total_descuento");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total_venta");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

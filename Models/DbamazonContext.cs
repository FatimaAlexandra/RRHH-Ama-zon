using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace amazon.Models;

public partial class DbamazonContext : DbContext
{
    public DbamazonContext()
    {
    }

    public DbamazonContext(DbContextOptions<DbamazonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acuerdo> Acuerdos { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Paise> Paises { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-RGSSVROO\\SQLEXPRESS; Database=dbamazon; Trusted_Connection=True; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acuerdo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__acuerdos__3213E83FA7BE4452");

            entity.ToTable("acuerdos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenido)
                .IsUnicode(false)
                .HasColumnName("contenido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Paisid).HasColumnName("paisid");
            entity.Property(e => e.Tipo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Pais).WithMany(p => p.Acuerdos)
                .HasForeignKey(d => d.Paisid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__acuerdos__paisid__29572725");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contrato__3213E83FC451393F");

            entity.ToTable("contratos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acuerdoid).HasColumnName("acuerdoid");
            entity.Property(e => e.Cargo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.FechaFin)
                .HasColumnType("date")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("date")
                .HasColumnName("fecha_inicio");

            entity.HasOne(d => d.Acuerdo).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.Acuerdoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contratos__acuer__2C3393D0");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83F31FB1BA4");

            entity.ToTable("documentos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero_documento");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_documento");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__empleado__3213E83FB3216C17");

            entity.ToTable("empleados");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contratoid).HasColumnName("contratoid");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Documentoid).HasColumnName("documentoid");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Sedeid).HasColumnName("sedeid");
            entity.Property(e => e.Telefono)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.Contratoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__empleados__contr__33D4B598");

            entity.HasOne(d => d.Documento).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.Documentoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__empleados__docum__34C8D9D1");

            entity.HasOne(d => d.Sede).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.Sedeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__empleados__sedei__32E0915F");
        });

        modelBuilder.Entity<Paise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__paises__3213E83FCBBBD71C");

            entity.ToTable("paises");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isocode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("isocode");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sedes__3213E83F80856750");

            entity.ToTable("sedes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigosede)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("codigosede");
            entity.Property(e => e.Logo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("logo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Paisid).HasColumnName("paisid");

            entity.HasOne(d => d.Pais).WithMany(p => p.Sedes)
                .HasForeignKey(d => d.Paisid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sedes__paisid__267ABA7A");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__usuarios__3213E83FCF404963");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clave)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol).HasColumnName("rol");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<Telefono> Telefonos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-RGSSVROO\\SQLEXPRESS; Database=dbamazon; Trusted_Connection=True; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acuerdo>(entity =>
        {
            entity.HasKey(e => e.Acuerdoid).HasName("PK__acuerdos__0FE72FFD295A40C4");

            entity.ToTable("acuerdos");

            entity.Property(e => e.Acuerdoid)
                .ValueGeneratedNever()
                .HasColumnName("acuerdoid");
            entity.Property(e => e.Contenido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contenido");
            entity.Property(e => e.Contratoid).HasColumnName("contratoid");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Paisid).HasColumnName("paisid");
            entity.Property(e => e.Pivoteid).HasColumnName("pivoteid");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Acuerdos)
                .HasForeignKey(d => d.Contratoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__acuerdos__contra__44FF419A");

            entity.HasOne(d => d.Pais).WithMany(p => p.Acuerdos)
                .HasForeignKey(d => d.Paisid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__acuerdos__paisid__440B1D61");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.Contratoid).HasName("PK__contrato__F7EE8A46056E8ED3");

            entity.ToTable("contrato");

            entity.Property(e => e.Contratoid)
                .ValueGeneratedNever()
                .HasColumnName("contratoid");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.Empleadoid).HasColumnName("empleadoid");
            entity.Property(e => e.FechaFin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.Pivoteid).HasColumnName("pivoteid");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.Empleadoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contrato__emplea__412EB0B6");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Empleadoid).HasName("PK__empleado__CCDA5420D385514B");

            entity.ToTable("empleados");

            entity.Property(e => e.Empleadoid)
                .ValueGeneratedNever()
                .HasColumnName("empleadoid");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.Paisid).HasName("PK__pais__456747A30BC7C63E");

            entity.ToTable("pais");

            entity.Property(e => e.Paisid)
                .ValueGeneratedNever()
                .HasColumnName("paisid");
            entity.Property(e => e.Idioma)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idioma");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.Sedeid).HasName("PK__sede__FFC4AC7790A8BFDA");

            entity.ToTable("sede");

            entity.Property(e => e.Sedeid)
                .ValueGeneratedNever()
                .HasColumnName("sedeid");
            entity.Property(e => e.Codigosede)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigosede");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Paisid).HasColumnName("paisid");

            entity.HasOne(d => d.Pais).WithMany(p => p.Sedes)
                .HasForeignKey(d => d.Paisid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sede__paisid__38996AB5");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => e.Telefonoid).HasName("PK__telefono__EC01EDD52D7D3ECB");

            entity.ToTable("telefono");

            entity.Property(e => e.Telefonoid)
                .ValueGeneratedNever()
                .HasColumnName("telefonoid");
            entity.Property(e => e.Empleadoid).HasColumnName("empleadoid");
            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.Sedeid).HasColumnName("sedeid");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.Empleadoid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__telefono__emplea__3E52440B");

            entity.HasOne(d => d.Sede).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.Sedeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__telefono__sedeid__3D5E1FD2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuario__645723A6B52DBE9B");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("idUsuario");
            entity.Property(e => e.Pass)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pass");
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

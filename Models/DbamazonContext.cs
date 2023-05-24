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
            entity.HasKey(e => e.Id).HasName("PK__acuerdos__3213E83F309F79B0");

            entity.ToTable("acuerdos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contenido");
            entity.Property(e => e.IdContrato).HasColumnName("idContrato");
            entity.Property(e => e.IdPais).HasColumnName("idPais");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Contrato).WithMany(p => p.Acuerdos)
                .HasForeignKey(d => d.IdContrato)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__acuerdos__idCont__44FF419A");

            entity.HasOne(d => d.Pais).WithMany(p => p.Acuerdos)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__acuerdos__idPais__440B1D61");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__contrato__3213E83F4EF2981A");

            entity.ToTable("contrato");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.FechaFin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idEmpleado");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__contrato__idEmpl__412EB0B6");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__empleado__3213E83F41E920E3");

            entity.ToTable("empleados");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
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
            entity.HasKey(e => e.Id).HasName("PK__pais__3213E83FC9A6A4D9");

            entity.ToTable("pais");

            entity.Property(e => e.Id).HasColumnName("id");
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
            entity.HasKey(e => e.Id).HasName("PK__sede__3213E83FE6814E3E");

            entity.ToTable("sede");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Codigosede)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigosede");
            entity.Property(e => e.IdPais).HasColumnName("idPais");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Sedes)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sede__idPais__38996AB5");
        });

        modelBuilder.Entity<Telefono>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__telefono__3213E83F82E04E9C");

            entity.ToTable("telefono");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEmpleado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idEmpleado");
            entity.Property(e => e.IdSede).HasColumnName("idSede");
            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("numero");

            entity.HasOne(d => d.Empleado).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__telefono__idEmpl__3E52440B");

            entity.HasOne(d => d.Sede).WithMany(p => p.Telefonos)
                .HasForeignKey(d => d.IdSede)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__telefono__idSede__3D5E1FD2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuario__645723A6A784EBC9");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
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

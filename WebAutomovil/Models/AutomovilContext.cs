using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAutomovil.Models;

public partial class AutomovilContext : DbContext
{
    public AutomovilContext()
    {
    }

    public AutomovilContext(DbContextOptions<AutomovilContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carro> Carros { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteCarro> ClienteCarros { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carro>(entity =>
        {
            entity.ToTable("Carro");

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

            entity.HasOne(d => d.MarcaNavigation).WithMany(p => p.Carros)
                .HasForeignKey(d => d.Marca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Carro__Marca__49C3F6B7");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.Property(e => e.Documento).HasMaxLength(200);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombres).HasMaxLength(200);
        });

        modelBuilder.Entity<ClienteCarro>(entity =>
        {
            entity.ToTable("ClienteCarro");

            entity.Property(e => e.Estado).HasDefaultValue(true);

            entity.HasOne(d => d.CarroNavigation).WithMany(p => p.ClienteCarros)
                .HasForeignKey(d => d.Carro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClienteCa__Carro__5629CD9C");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.ClienteCarros)
                .HasForeignKey(d => d.Cliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClienteCa__Clien__5535A963");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.ToTable("Marca");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

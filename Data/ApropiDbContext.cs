using Microsoft.EntityFrameworkCore;
using Apropio.API.Models;

namespace Apropio.API.Data;

public class ApropiDbContext : DbContext
{
    public ApropiDbContext(DbContextOptions<ApropiDbContext> options) : base(options)
    {
    }

    public DbSet<Inmueble> Inmuebles { get; set; }
    public DbSet<ImagenInmueble> ImagenesInmuebles { get; set; }
    public DbSet<Propietario> Propietarios { get; set; }
    public DbSet<Inquilino> Inquilinos { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Contrato> Contratos { get; set; }
    public DbSet<Visita> Visitas { get; set; }
    public DbSet<PagoAlquiler> PagosAlquiler { get; set; }
    public DbSet<ReferenciaInquilino> ReferenciasInquilinos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Inmueble
        modelBuilder.Entity<Inmueble>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Precio).HasPrecision(18, 2);
            entity.Property(e => e.Superficie).HasPrecision(10, 2);
            entity.Property(e => e.SuperficieCubierta).HasPrecision(10, 2);
            entity.HasIndex(e => e.Direccion);
            entity.HasIndex(e => new { e.Ciudad, e.TipoInmueble });
            entity.HasIndex(e => e.Precio);
        });

        // Configuración de ImagenInmueble
        modelBuilder.Entity<ImagenInmueble>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Inmueble)
                  .WithMany(i => i.Imagenes)
                  .HasForeignKey(e => e.InmuebleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de Propietario
        modelBuilder.Entity<Propietario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Dni).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configuración de Inquilino
        modelBuilder.Entity<Inquilino>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Salario).HasPrecision(18, 2);
            entity.Property(e => e.IngresosMensuales).HasPrecision(18, 2);
            entity.Property(e => e.OtrosIngresos).HasPrecision(18, 2);
            entity.HasIndex(e => e.Dni).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configuración de Empleado
        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SalarioBase).HasPrecision(18, 2);
            entity.Property(e => e.PorcentajeComision).HasPrecision(5, 2);
            entity.Property(e => e.ComisionFija).HasPrecision(18, 2);
            entity.HasIndex(e => e.Dni).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.TipoEmpleado);
        });

        // Configuración de Contrato
        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.MontoTotal).HasPrecision(18, 2);
            entity.Property(e => e.MontoMensual).HasPrecision(18, 2);
            entity.Property(e => e.Deposito).HasPrecision(18, 2);
            entity.Property(e => e.ComisionInmobiliaria).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Inmueble)
                  .WithMany(i => i.Contratos)
                  .HasForeignKey(e => e.InmuebleId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            entity.HasOne(e => e.Inquilino)
                  .WithMany(i => i.Contratos)
                  .HasForeignKey(e => e.InquilinoId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            entity.HasOne(e => e.Empleado)
                  .WithMany(e => e.Contratos)
                  .HasForeignKey(e => e.EmpleadoId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de Visita
        modelBuilder.Entity<Visita>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Inmueble)
                  .WithMany(i => i.Visitas)
                  .HasForeignKey(e => e.InmuebleId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.Inquilino)
                  .WithMany(i => i.Visitas)
                  .HasForeignKey(e => e.InquilinoId)
                  .OnDelete(DeleteBehavior.SetNull);
                  
            entity.HasOne(e => e.Empleado)
                  .WithMany(e => e.Visitas)
                  .HasForeignKey(e => e.EmpleadoId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Configuración de PagoAlquiler
        modelBuilder.Entity<PagoAlquiler>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Monto).HasPrecision(18, 2);
            entity.HasOne(e => e.Contrato)
                  .WithMany(c => c.Pagos)
                  .HasForeignKey(e => e.ContratoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de ReferenciaInquilino
        modelBuilder.Entity<ReferenciaInquilino>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Inquilino)
                  .WithMany(i => i.Referencias)
                  .HasForeignKey(e => e.InquilinoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configuración de relación Inmueble-Propietario
        modelBuilder.Entity<Inmueble>()
            .HasOne(i => i.Propietario)
            .WithMany(p => p.Inmuebles)
            .HasForeignKey(i => i.PropietarioId)
            .OnDelete(DeleteBehavior.SetNull);

        // Configuración de Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.NombreUsuario).IsUnique();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
        });
    }
} 
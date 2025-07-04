﻿// <auto-generated />
using System;
using Apropio.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Apropio.API.Migrations
{
    [DbContext(typeof(ApropiDbContext))]
    [Migration("20250703144736_InitialCreateWithFase2")]
    partial class InitialCreateWithFase2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.6");

            modelBuilder.Entity("Apropio.API.Models.Contrato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("ComisionInmobiliaria")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Condiciones")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Deposito")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("DuracionMeses")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EmpleadoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("TEXT");

                    b.Property<int>("InmuebleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InquilinoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("MontoMensual")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MontoTotal")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoContrato")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("InmuebleId");

                    b.HasIndex("InquilinoId");

                    b.ToTable("Contratos");
                });

            modelBuilder.Entity("Apropio.API.Models.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Banco")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cargo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cbu")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Celular")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Ciudad")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("ComisionFija")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Cuil")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("EstadoCivil")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaEgreso")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaIngreso")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nacionalidad")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("NumeroCuenta")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("PorcentajeComision")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Provincia")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("SalarioBase")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoContrato")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoCuenta")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoEmpleado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Dni")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("TipoEmpleado");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("Apropio.API.Models.ImagenInmueble", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EsPrincipal")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("FechaSubida")
                        .HasColumnType("TEXT");

                    b.Property<int>("InmuebleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Orden")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InmuebleId");

                    b.ToTable("ImagenesInmuebles");
                });

            modelBuilder.Entity("Apropio.API.Models.Inmueble", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Baños")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Ciudad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("Cocheras")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Descripcion")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Habitaciones")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Operacion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Precio")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("PropietarioId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Provincia")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Superficie")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("SuperficieCubierta")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("TieneAscensor")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TieneBalcon")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TieneGimnasio")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TieneJardin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TieneParrilla")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TienePiscina")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TienePortero")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TieneSalon")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TieneSeguridad")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TieneTerraza")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TipoInmueble")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Direccion");

                    b.HasIndex("Precio");

                    b.HasIndex("PropietarioId");

                    b.HasIndex("Ciudad", "TipoInmueble");

                    b.ToTable("Inmuebles");
                });

            modelBuilder.Entity("Apropio.API.Models.Inquilino", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Banco")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cargo")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cbu")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Celular")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Ciudad")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("DireccionTrabajo")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Empresa")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("EstadoCivil")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaInicioTrabajo")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("IngresosMensuales")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nacionalidad")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("NumeroCuenta")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("OtrosIngresos")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Provincia")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("Salario")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("TelefonoTrabajo")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoContrato")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoCuenta")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Dni")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Inquilinos");
                });

            modelBuilder.Entity("Apropio.API.Models.PagoAlquiler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ContratoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaPago")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Monto")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumeroComprobante")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("PeriodoPago")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContratoId");

                    b.ToTable("PagosAlquiler");
                });

            modelBuilder.Entity("Apropio.API.Models.Propietario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Alias")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Banco")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cbu")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Celular")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Ciudad")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("NumeroCuenta")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Provincia")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoCuenta")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Dni")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Propietarios");
                });

            modelBuilder.Entity("Apropio.API.Models.ReferenciaInquilino", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellido")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Cargo")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Comentarios")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Empresa")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("EstadoVerificacion")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaVerificacion")
                        .HasColumnType("TEXT");

                    b.Property<int>("InquilinoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Relacion")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InquilinoId");

                    b.ToTable("ReferenciasInquilinos");
                });

            modelBuilder.Entity("Apropio.API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("TEXT");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<int>("TipoUsuario")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("UltimoAcceso")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NombreUsuario")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Apropio.API.Models.Visita", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Activo")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Calificacion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comentarios")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailVisitante")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<int?>("EmpleadoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Estado")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaActualizacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaEliminacion")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("TEXT");

                    b.Property<int>("InmuebleId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InquilinoId")
                        .HasColumnType("INTEGER");

                    b.Property<bool?>("Interesado")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NombreVisitante")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Observaciones")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<string>("TelefonoVisitante")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("InmuebleId");

                    b.HasIndex("InquilinoId");

                    b.ToTable("Visitas");
                });

            modelBuilder.Entity("Apropio.API.Models.Contrato", b =>
                {
                    b.HasOne("Apropio.API.Models.Empleado", "Empleado")
                        .WithMany("Contratos")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Apropio.API.Models.Inmueble", "Inmueble")
                        .WithMany("Contratos")
                        .HasForeignKey("InmuebleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Apropio.API.Models.Inquilino", "Inquilino")
                        .WithMany("Contratos")
                        .HasForeignKey("InquilinoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Inmueble");

                    b.Navigation("Inquilino");
                });

            modelBuilder.Entity("Apropio.API.Models.Empleado", b =>
                {
                    b.HasOne("Apropio.API.Models.Usuario", null)
                        .WithMany("Empleados")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("Apropio.API.Models.ImagenInmueble", b =>
                {
                    b.HasOne("Apropio.API.Models.Inmueble", "Inmueble")
                        .WithMany("Imagenes")
                        .HasForeignKey("InmuebleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inmueble");
                });

            modelBuilder.Entity("Apropio.API.Models.Inmueble", b =>
                {
                    b.HasOne("Apropio.API.Models.Propietario", "Propietario")
                        .WithMany("Inmuebles")
                        .HasForeignKey("PropietarioId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Propietario");
                });

            modelBuilder.Entity("Apropio.API.Models.PagoAlquiler", b =>
                {
                    b.HasOne("Apropio.API.Models.Contrato", "Contrato")
                        .WithMany("Pagos")
                        .HasForeignKey("ContratoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contrato");
                });

            modelBuilder.Entity("Apropio.API.Models.ReferenciaInquilino", b =>
                {
                    b.HasOne("Apropio.API.Models.Inquilino", "Inquilino")
                        .WithMany("Referencias")
                        .HasForeignKey("InquilinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inquilino");
                });

            modelBuilder.Entity("Apropio.API.Models.Visita", b =>
                {
                    b.HasOne("Apropio.API.Models.Empleado", "Empleado")
                        .WithMany("Visitas")
                        .HasForeignKey("EmpleadoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Apropio.API.Models.Inmueble", "Inmueble")
                        .WithMany("Visitas")
                        .HasForeignKey("InmuebleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Apropio.API.Models.Inquilino", "Inquilino")
                        .WithMany("Visitas")
                        .HasForeignKey("InquilinoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Empleado");

                    b.Navigation("Inmueble");

                    b.Navigation("Inquilino");
                });

            modelBuilder.Entity("Apropio.API.Models.Contrato", b =>
                {
                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("Apropio.API.Models.Empleado", b =>
                {
                    b.Navigation("Contratos");

                    b.Navigation("Visitas");
                });

            modelBuilder.Entity("Apropio.API.Models.Inmueble", b =>
                {
                    b.Navigation("Contratos");

                    b.Navigation("Imagenes");

                    b.Navigation("Visitas");
                });

            modelBuilder.Entity("Apropio.API.Models.Inquilino", b =>
                {
                    b.Navigation("Contratos");

                    b.Navigation("Referencias");

                    b.Navigation("Visitas");
                });

            modelBuilder.Entity("Apropio.API.Models.Propietario", b =>
                {
                    b.Navigation("Inmuebles");
                });

            modelBuilder.Entity("Apropio.API.Models.Usuario", b =>
                {
                    b.Navigation("Empleados");
                });
#pragma warning restore 612, 618
        }
    }
}

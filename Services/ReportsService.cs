using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Apropio.API.Data;
using Apropio.API.DTOs;
using Apropio.API.Models;

namespace Apropio.API.Services
{
    public class ReportsService : IReportsService
    {
        private readonly ApropiDbContext _context;

        public ReportsService(ApropiDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var fechaActual = DateTime.UtcNow;
            var inicioMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
            var inicioAño = new DateTime(fechaActual.Year, 1, 1);

            var totalInmuebles = await _context.Inmuebles
                .Where(i => i.FechaEliminacion == null)
                .CountAsync();

            var inmueblesDisponibles = await _context.Inmuebles
                .Where(i => i.FechaEliminacion == null && i.Estado == "Disponible")
                .CountAsync();

            var inmueblesAlquilados = await _context.Inmuebles
                .Where(i => i.FechaEliminacion == null && i.Estado == "Alquilado")
                .CountAsync();

            var totalInquilinos = await _context.Inquilinos
                .Where(i => i.FechaEliminacion == null)
                .CountAsync();

            var totalEmpleados = await _context.Empleados
                .Where(e => e.FechaEliminacion == null)
                .CountAsync();

            var contratosActivos = await _context.Contratos
                .Where(c => c.FechaEliminacion == null && 
                           c.FechaInicio <= fechaActual && 
                           c.FechaFin >= fechaActual)
                .CountAsync();

            var visitasProgramadas = await _context.Visitas
                .Where(v => v.FechaEliminacion == null && 
                           v.FechaHora >= fechaActual && 
                           v.Estado == "Programada")
                .CountAsync();

            var pagosPendientes = await _context.PagosAlquiler
                .Where(p => p.FechaEliminacion == null && 
                           p.FechaPago == null && 
                           p.FechaVencimiento <= fechaActual)
                .CountAsync();

            var ingresosMensuales = await _context.PagosAlquiler
                .Where(p => p.FechaEliminacion == null && 
                           p.FechaPago >= inicioMes && 
                           p.FechaPago <= fechaActual)
                .SumAsync(p => p.Monto);

            var ingresosAnuales = await _context.PagosAlquiler
                .Where(p => p.FechaEliminacion == null && 
                           p.FechaPago >= inicioAño && 
                           p.FechaPago <= fechaActual)
                .SumAsync(p => p.Monto);

            var promedioAlquiler = await _context.Contratos
                .Where(c => c.FechaEliminacion == null && c.TipoContrato == "Alquiler")
                .AverageAsync(c => (decimal?)c.MontoAlquiler) ?? 0;

            return new DashboardStatsDto
            {
                TotalInmuebles = totalInmuebles,
                InmueblesDisponibles = inmueblesDisponibles,
                InmueblesAlquilados = inmueblesAlquilados,
                TotalInquilinos = totalInquilinos,
                TotalEmpleados = totalEmpleados,
                ContratosActivos = contratosActivos,
                VisitasProgramadas = visitasProgramadas,
                PagosPendientes = pagosPendientes,
                IngresosMensuales = ingresosMensuales,
                IngresosAnuales = ingresosAnuales,
                PromedioAlquiler = promedioAlquiler,
                FechaUltimaActualizacion = fechaActual
            };
        }

        public async Task<RevenueReportDto> GetRevenueReportAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var contratos = await _context.Contratos
                .Where(c => c.FechaEliminacion == null && 
                           c.FechaInicio >= fechaInicio && 
                           c.FechaInicio <= fechaFin)
                .ToListAsync();

            var pagos = await _context.PagosAlquiler
                .Where(p => p.FechaEliminacion == null && 
                           p.FechaPago >= fechaInicio && 
                           p.FechaPago <= fechaFin)
                .ToListAsync();

            var ingresosAlquiler = contratos
                .Where(c => c.TipoContrato == "Alquiler")
                .Sum(c => c.MontoAlquiler);

            var ingresosVentas = contratos
                .Where(c => c.TipoContrato == "Venta")
                .Sum(c => c.MontoAlquiler); // En ventas, este campo representa el precio

            var comisiones = await _context.Empleados
                .Where(e => e.FechaEliminacion == null)
                .SumAsync(e => e.Comision ?? 0);

            var ingresosMensuales = pagos
                .GroupBy(p => new { p.FechaPago?.Year, p.FechaPago?.Month })
                .Select(g => new MonthlyRevenueDto
                {
                    Año = g.Key.Year ?? 0,
                    Mes = g.Key.Month ?? 0,
                    NombreMes = g.Key.Month.HasValue ? 
                        CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month.Value) : "",
                    Ingresos = g.Sum(p => p.Monto),
                    CantidadContratos = g.Count()
                })
                .OrderBy(m => m.Año)
                .ThenBy(m => m.Mes)
                .ToList();

            return new RevenueReportDto
            {
                Periodo = $"{fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}",
                IngresosTotales = ingresosAlquiler + ingresosVentas + comisiones,
                IngresosAlquiler = ingresosAlquiler,
                IngresosVentas = ingresosVentas,
                Comisiones = comisiones,
                CantidadContratos = contratos.Count,
                Ingresosmensuales = ingresosMensuales
            };
        }

        public async Task<PropertyReportDto> GetPropertyReportAsync()
        {
            var inmuebles = await _context.Inmuebles
                .Where(i => i.FechaEliminacion == null)
                .ToListAsync();

            var totalPropiedades = inmuebles.Count;
            var propiedadesDisponibles = inmuebles.Count(i => i.Estado == "Disponible");
            var propiedadesAlquiladas = inmuebles.Count(i => i.Estado == "Alquilado");
            var propiedadesVendidas = inmuebles.Count(i => i.Estado == "Vendido");

            var propiedadesPorCiudad = inmuebles
                .GroupBy(i => i.Ciudad)
                .Select(g => new PropertyByCityDto
                {
                    Ciudad = g.Key,
                    Cantidad = g.Count(),
                    PrecioPromedio = g.Average(i => i.Precio)
                })
                .OrderByDescending(p => p.Cantidad)
                .ToList();

            var propiedadesPorTipo = inmuebles
                .GroupBy(i => i.TipoInmueble)
                .Select(g => new PropertyByTypeDto
                {
                    TipoInmueble = g.Key,
                    Cantidad = g.Count(),
                    PrecioPromedio = g.Average(i => i.Precio)
                })
                .OrderByDescending(p => p.Cantidad)
                .ToList();

            var propiedadesPorRango = GetPropertyByPriceRange(inmuebles);

            return new PropertyReportDto
            {
                TotalPropiedades = totalPropiedades,
                PropiedadesDisponibles = propiedadesDisponibles,
                PropiedadesAlquiladas = propiedadesAlquiladas,
                PropiedadesVendidas = propiedadesVendidas,
                PropiedadesPorCiudad = propiedadesPorCiudad,
                PropiedadesPorTipo = propiedadesPorTipo,
                PropiedadesPorRangoPrecio = propiedadesPorRango
            };
        }

        public async Task<ContractsReportDto> GetContractsReportAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var fechaActual = DateTime.UtcNow;
            var contratos = await _context.Contratos
                .Where(c => c.FechaEliminacion == null)
                .ToListAsync();

            var contratosActivos = contratos.Count(c => 
                c.FechaInicio <= fechaActual && c.FechaFin >= fechaActual);

            var contratosVencidos = contratos.Count(c => c.FechaFin < fechaActual);

            var contratosPorVencer = contratos.Count(c => 
                c.FechaFin >= fechaActual && c.FechaFin <= fechaActual.AddDays(30));

            var contratosPorTipo = contratos
                .GroupBy(c => c.TipoContrato)
                .Select(g => new ContractsByTypeDto
                {
                    TipoContrato = g.Key,
                    Cantidad = g.Count(),
                    MontoPromedio = g.Average(c => c.MontoAlquiler)
                })
                .ToList();

            var contratosPorMes = contratos
                .Where(c => c.FechaInicio >= fechaInicio && c.FechaInicio <= fechaFin)
                .GroupBy(c => new { c.FechaInicio.Year, c.FechaInicio.Month })
                .Select(g => new ContractsByMonthDto
                {
                    Año = g.Key.Year,
                    Mes = g.Key.Month,
                    NombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                    Cantidad = g.Count(),
                    MontoTotal = g.Sum(c => c.MontoAlquiler)
                })
                .OrderBy(c => c.Año)
                .ThenBy(c => c.Mes)
                .ToList();

            return new ContractsReportDto
            {
                ContratosActivos = contratosActivos,
                ContratosVencidos = contratosVencidos,
                ContratosPorVencer = contratosPorVencer,
                ContratosPorTipo = contratosPorTipo,
                ContratosPorMes = contratosPorMes
            };
        }

        public async Task<PaymentsReportDto> GetPaymentsReportAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var pagos = await _context.PagosAlquiler
                .Where(p => p.FechaEliminacion == null)
                .ToListAsync();

            var fechaActual = DateTime.UtcNow;

            var pagosPendientes = pagos.Count(p => p.FechaPago == null && p.FechaVencimiento >= fechaActual);
            var pagosRealizados = pagos.Count(p => p.FechaPago.HasValue);
            var pagosVencidos = pagos.Count(p => p.FechaPago == null && p.FechaVencimiento < fechaActual);

            var montoPendiente = pagos
                .Where(p => p.FechaPago == null && p.FechaVencimiento >= fechaActual)
                .Sum(p => p.Monto);

            var montoRecaudado = pagos
                .Where(p => p.FechaPago.HasValue && 
                           p.FechaPago >= fechaInicio && 
                           p.FechaPago <= fechaFin)
                .Sum(p => p.Monto);

            var montoVencido = pagos
                .Where(p => p.FechaPago == null && p.FechaVencimiento < fechaActual)
                .Sum(p => p.Monto);

            var pagosPorMetodo = pagos
                .Where(p => p.FechaPago.HasValue && 
                           p.FechaPago >= fechaInicio && 
                           p.FechaPago <= fechaFin)
                .GroupBy(p => p.MetodoPago)
                .Select(g => new PaymentsByMethodDto
                {
                    MetodoPago = g.Key,
                    Cantidad = g.Count(),
                    MontoTotal = g.Sum(p => p.Monto)
                })
                .ToList();

            var pagosPorMes = pagos
                .Where(p => p.FechaPago.HasValue && 
                           p.FechaPago >= fechaInicio && 
                           p.FechaPago <= fechaFin)
                .GroupBy(p => new { p.FechaPago?.Year, p.FechaPago?.Month })
                .Select(g => new PaymentsByMonthDto
                {
                    Año = g.Key.Year ?? 0,
                    Mes = g.Key.Month ?? 0,
                    NombreMes = g.Key.Month.HasValue ? 
                        CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month.Value) : "",
                    Cantidad = g.Count(),
                    Monto = g.Sum(p => p.Monto)
                })
                .OrderBy(p => p.Año)
                .ThenBy(p => p.Mes)
                .ToList();

            return new PaymentsReportDto
            {
                PagosPendientes = pagosPendientes,
                PagosRealizados = pagosRealizados,
                PagosVencidos = pagosVencidos,
                MontoPendiente = montoPendiente,
                MontoRecaudado = montoRecaudado,
                MontoVencido = montoVencido,
                PagosPorMetodo = pagosPorMetodo,
                PagosPorMes = pagosPorMes
            };
        }

        public async Task<List<EmployeePerformanceDto>> GetEmployeePerformanceAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var empleados = await _context.Empleados
                .Where(e => e.FechaEliminacion == null)
                .Include(e => e.Contratos)
                .Include(e => e.Visitas)
                .ToListAsync();

            var performance = empleados.Select(e => new EmployeePerformanceDto
            {
                EmpleadoId = e.Id,
                NombreCompleto = $"{e.Nombres} {e.Apellidos}",
                TipoEmpleado = e.TipoEmpleado,
                ContratosRealizados = e.Contratos?.Count(c => 
                    c.FechaInicio >= fechaInicio && 
                    c.FechaInicio <= fechaFin) ?? 0,
                VisitasRealizadas = e.Visitas?.Count(v => 
                    v.FechaHora >= fechaInicio && 
                    v.FechaHora <= fechaFin) ?? 0,
                ComisionesGeneradas = e.Comision ?? 0,
                VentasTotales = e.Contratos?.Where(c => 
                    c.FechaInicio >= fechaInicio && 
                    c.FechaInicio <= fechaFin)
                    .Sum(c => c.MontoAlquiler) ?? 0,
                PromedioComision = e.Contratos?.Any() == true ? 
                    (e.Comision ?? 0) / e.Contratos.Count : 0
            }).ToList();

            return performance.OrderByDescending(p => p.VentasTotales).ToList();
        }

        public async Task<byte[]> ExportReportToPdfAsync(string reportType, DateTime fechaInicio, DateTime fechaFin)
        {
            // Implementación básica - en una aplicación real usarías librerías como iTextSharp
            var reportData = await GetReportDataAsync(reportType, fechaInicio, fechaFin);
            return GeneratePdfBytes(reportData, reportType);
        }

        public async Task<byte[]> ExportReportToExcelAsync(string reportType, DateTime fechaInicio, DateTime fechaFin)
        {
            // Implementación básica - en una aplicación real usarías librerías como EPPlus
            var reportData = await GetReportDataAsync(reportType, fechaInicio, fechaFin);
            return GenerateExcelBytes(reportData, reportType);
        }

        private List<PropertyByPriceRangeDto> GetPropertyByPriceRange(List<Inmueble> inmuebles)
        {
            var ranges = new List<PropertyByPriceRangeDto>();

            var ranges_values = new[]
            {
                (Min: 0m, Max: 500000m, Label: "Hasta $500,000"),
                (Min: 500000m, Max: 1000000m, Label: "$500,000 - $1,000,000"),
                (Min: 1000000m, Max: 2000000m, Label: "$1,000,000 - $2,000,000"),
                (Min: 2000000m, Max: 5000000m, Label: "$2,000,000 - $5,000,000"),
                (Min: 5000000m, Max: decimal.MaxValue, Label: "Más de $5,000,000")
            };

            foreach (var range in ranges_values)
            {
                var propiedadesEnRango = inmuebles
                    .Where(i => i.Precio >= range.Min && i.Precio < range.Max)
                    .ToList();

                if (propiedadesEnRango.Any())
                {
                    ranges.Add(new PropertyByPriceRangeDto
                    {
                        RangoPrecio = range.Label,
                        Cantidad = propiedadesEnRango.Count,
                        PrecioMinimo = propiedadesEnRango.Min(p => p.Precio),
                        PrecioMaximo = propiedadesEnRango.Max(p => p.Precio)
                    });
                }
            }

            return ranges;
        }

        private async Task<object> GetReportDataAsync(string reportType, DateTime fechaInicio, DateTime fechaFin)
        {
            return reportType.ToLower() switch
            {
                "revenue" => await GetRevenueReportAsync(fechaInicio, fechaFin),
                "properties" => await GetPropertyReportAsync(),
                "contracts" => await GetContractsReportAsync(fechaInicio, fechaFin),
                "payments" => await GetPaymentsReportAsync(fechaInicio, fechaFin),
                "employees" => await GetEmployeePerformanceAsync(fechaInicio, fechaFin),
                _ => await GetDashboardStatsAsync()
            };
        }

        private byte[] GeneratePdfBytes(object data, string reportType)
        {
            // Implementación básica - retorna datos dummy
            var content = $"Reporte {reportType} generado el {DateTime.Now:dd/MM/yyyy HH:mm}";
            return System.Text.Encoding.UTF8.GetBytes(content);
        }

        private byte[] GenerateExcelBytes(object data, string reportType)
        {
            // Implementación básica - retorna datos dummy
            var content = $"Reporte Excel {reportType} generado el {DateTime.Now:dd/MM/yyyy HH:mm}";
            return System.Text.Encoding.UTF8.GetBytes(content);
        }
    }
} 
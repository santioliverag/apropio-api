using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Apropio.API.DTOs;
using Apropio.API.Services;

namespace Apropio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;

        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        /// <summary>
        /// Obtener estadísticas generales del dashboard
        /// </summary>
        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats()
        {
            var stats = await _reportsService.GetDashboardStatsAsync();
            return Ok(stats);
        }

        /// <summary>
        /// Obtener reporte de ingresos por período
        /// </summary>
        [HttpGet("revenue")]
        public async Task<ActionResult<RevenueReportDto>> GetRevenueReport(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio == default || fechaFin == default)
            {
                fechaFin = DateTime.UtcNow;
                fechaInicio = fechaFin.AddMonths(-12);
            }

            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio no puede ser mayor a la fecha de fin");

            var report = await _reportsService.GetRevenueReportAsync(fechaInicio, fechaFin);
            return Ok(report);
        }

        /// <summary>
        /// Obtener reporte de propiedades
        /// </summary>
        [HttpGet("properties")]
        public async Task<ActionResult<PropertyReportDto>> GetPropertyReport()
        {
            var report = await _reportsService.GetPropertyReportAsync();
            return Ok(report);
        }

        /// <summary>
        /// Obtener reporte de contratos por período
        /// </summary>
        [HttpGet("contracts")]
        public async Task<ActionResult<ContractsReportDto>> GetContractsReport(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio == default || fechaFin == default)
            {
                fechaFin = DateTime.UtcNow;
                fechaInicio = fechaFin.AddMonths(-12);
            }

            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio no puede ser mayor a la fecha de fin");

            var report = await _reportsService.GetContractsReportAsync(fechaInicio, fechaFin);
            return Ok(report);
        }

        /// <summary>
        /// Obtener reporte de pagos por período
        /// </summary>
        [HttpGet("payments")]
        public async Task<ActionResult<PaymentsReportDto>> GetPaymentsReport(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio == default || fechaFin == default)
            {
                fechaFin = DateTime.UtcNow;
                fechaInicio = fechaFin.AddMonths(-12);
            }

            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio no puede ser mayor a la fecha de fin");

            var report = await _reportsService.GetPaymentsReportAsync(fechaInicio, fechaFin);
            return Ok(report);
        }

        /// <summary>
        /// Obtener reporte de rendimiento de empleados
        /// </summary>
        [HttpGet("employees/performance")]
        public async Task<ActionResult<List<EmployeePerformanceDto>>> GetEmployeePerformance(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio == default || fechaFin == default)
            {
                fechaFin = DateTime.UtcNow;
                fechaInicio = fechaFin.AddMonths(-12);
            }

            if (fechaInicio > fechaFin)
                return BadRequest("La fecha de inicio no puede ser mayor a la fecha de fin");

            var performance = await _reportsService.GetEmployeePerformanceAsync(fechaInicio, fechaFin);
            return Ok(performance);
        }

        /// <summary>
        /// Exportar reporte a PDF
        /// </summary>
        [HttpGet("export/pdf/{reportType}")]
        public async Task<IActionResult> ExportToPdf(
            string reportType,
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            var validReportTypes = new[] { "dashboard", "revenue", "properties", "contracts", "payments", "employees" };
            if (!validReportTypes.Contains(reportType.ToLower()))
                return BadRequest("Tipo de reporte inválido");

            if (fechaInicio == default || fechaFin == default)
            {
                fechaFin = DateTime.UtcNow;
                fechaInicio = fechaFin.AddMonths(-12);
            }

            var pdfBytes = await _reportsService.ExportReportToPdfAsync(reportType, fechaInicio, fechaFin);
            var fileName = $"reporte_{reportType}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

            return File(pdfBytes, "application/pdf", fileName);
        }

        /// <summary>
        /// Exportar reporte a Excel
        /// </summary>
        [HttpGet("export/excel/{reportType}")]
        public async Task<IActionResult> ExportToExcel(
            string reportType,
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            var validReportTypes = new[] { "dashboard", "revenue", "properties", "contracts", "payments", "employees" };
            if (!validReportTypes.Contains(reportType.ToLower()))
                return BadRequest("Tipo de reporte inválido");

            if (fechaInicio == default || fechaFin == default)
            {
                fechaFin = DateTime.UtcNow;
                fechaInicio = fechaFin.AddMonths(-12);
            }

            var excelBytes = await _reportsService.ExportReportToExcelAsync(reportType, fechaInicio, fechaFin);
            var fileName = $"reporte_{reportType}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        /// <summary>
        /// Obtener resumen ejecutivo
        /// </summary>
        [HttpGet("executive-summary")]
        public async Task<ActionResult<object>> GetExecutiveSummary(
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            if (fechaInicio == default || fechaFin == default)
            {
                fechaFin = DateTime.UtcNow;
                fechaInicio = fechaFin.AddMonths(-3);
            }

            var dashboard = await _reportsService.GetDashboardStatsAsync();
            var revenue = await _reportsService.GetRevenueReportAsync(fechaInicio, fechaFin);
            var properties = await _reportsService.GetPropertyReportAsync();
            var contracts = await _reportsService.GetContractsReportAsync(fechaInicio, fechaFin);
            var payments = await _reportsService.GetPaymentsReportAsync(fechaInicio, fechaFin);

            var summary = new
            {
                Dashboard = dashboard,
                Revenue = revenue,
                Properties = new
                {
                    properties.TotalPropiedades,
                    properties.PropiedadesDisponibles,
                    properties.PropiedadesAlquiladas,
                    TopCities = properties.PropiedadesPorCiudad.Take(5)
                },
                Contracts = new
                {
                    contracts.ContratosActivos,
                    contracts.ContratosVencidos,
                    contracts.ContratosPorVencer
                },
                Payments = new
                {
                    payments.PagosPendientes,
                    payments.PagosVencidos,
                    payments.MontoPendiente,
                    payments.MontoVencido
                },
                FechaGeneracion = DateTime.UtcNow
            };

            return Ok(summary);
        }
    }
} 
using Apropio.API.DTOs;

namespace Apropio.API.Services
{
    public interface IReportsService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
        Task<RevenueReportDto> GetRevenueReportAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<PropertyReportDto> GetPropertyReportAsync();
        Task<ContractsReportDto> GetContractsReportAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<PaymentsReportDto> GetPaymentsReportAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<List<EmployeePerformanceDto>> GetEmployeePerformanceAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<byte[]> ExportReportToPdfAsync(string reportType, DateTime fechaInicio, DateTime fechaFin);
        Task<byte[]> ExportReportToExcelAsync(string reportType, DateTime fechaInicio, DateTime fechaFin);
    }
} 
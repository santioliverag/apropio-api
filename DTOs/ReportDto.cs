namespace Apropio.API.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalInmuebles { get; set; }
        public int InmueblesDisponibles { get; set; }
        public int InmueblesAlquilados { get; set; }
        public int TotalInquilinos { get; set; }
        public int TotalEmpleados { get; set; }
        public int ContratosActivos { get; set; }
        public int VisitasProgramadas { get; set; }
        public int PagosPendientes { get; set; }
        public decimal IngresosMensuales { get; set; }
        public decimal IngresosAnuales { get; set; }
        public decimal PromedioAlquiler { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
    }

    public class RevenueReportDto
    {
        public string Periodo { get; set; } = string.Empty;
        public decimal IngresosTotales { get; set; }
        public decimal IngresosAlquiler { get; set; }
        public decimal IngresosVentas { get; set; }
        public decimal Comisiones { get; set; }
        public int CantidadContratos { get; set; }
        public List<MonthlyRevenueDto> Ingresosmensuales { get; set; } = new();
    }

    public class MonthlyRevenueDto
    {
        public int Año { get; set; }
        public int Mes { get; set; }
        public string NombreMes { get; set; } = string.Empty;
        public decimal Ingresos { get; set; }
        public int CantidadContratos { get; set; }
    }

    public class PropertyReportDto
    {
        public int TotalPropiedades { get; set; }
        public int PropiedadesDisponibles { get; set; }
        public int PropiedadesAlquiladas { get; set; }
        public int PropiedadesVendidas { get; set; }
        public List<PropertyByCityDto> PropiedadesPorCiudad { get; set; } = new();
        public List<PropertyByTypeDto> PropiedadesPorTipo { get; set; } = new();
        public List<PropertyByPriceRangeDto> PropiedadesPorRangoPrecio { get; set; } = new();
    }

    public class PropertyByCityDto
    {
        public string Ciudad { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioPromedio { get; set; }
    }

    public class PropertyByTypeDto
    {
        public string TipoInmueble { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioPromedio { get; set; }
    }

    public class PropertyByPriceRangeDto
    {
        public string RangoPrecio { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioMinimo { get; set; }
        public decimal PrecioMaximo { get; set; }
    }

    public class ContractsReportDto
    {
        public int ContratosActivos { get; set; }
        public int ContratosVencidos { get; set; }
        public int ContratosPorVencer { get; set; }
        public List<ContractsByTypeDto> ContratosPorTipo { get; set; } = new();
        public List<ContractsByMonthDto> ContratosPorMes { get; set; } = new();
    }

    public class ContractsByTypeDto
    {
        public string TipoContrato { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal MontoPromedio { get; set; }
    }

    public class ContractsByMonthDto
    {
        public int Año { get; set; }
        public int Mes { get; set; }
        public string NombreMes { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal MontoTotal { get; set; }
    }

    public class PaymentsReportDto
    {
        public int PagosPendientes { get; set; }
        public int PagosRealizados { get; set; }
        public int PagosVencidos { get; set; }
        public decimal MontoPendiente { get; set; }
        public decimal MontoRecaudado { get; set; }
        public decimal MontoVencido { get; set; }
        public List<PaymentsByMethodDto> PagosPorMetodo { get; set; } = new();
        public List<PaymentsByMonthDto> PagosPorMes { get; set; } = new();
    }

    public class PaymentsByMethodDto
    {
        public string MetodoPago { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal MontoTotal { get; set; }
    }

    public class PaymentsByMonthDto
    {
        public int Año { get; set; }
        public int Mes { get; set; }
        public string NombreMes { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }
    }

    public class EmployeePerformanceDto
    {
        public int EmpleadoId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string TipoEmpleado { get; set; } = string.Empty;
        public int ContratosRealizados { get; set; }
        public int VisitasRealizadas { get; set; }
        public decimal ComisionesGeneradas { get; set; }
        public decimal VentasTotales { get; set; }
        public decimal PromedioComision { get; set; }
    }
} 
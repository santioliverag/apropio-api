using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apropio.API.Models;

public class Contrato
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string TipoContrato { get; set; } = string.Empty; // Alquiler, Venta, AlquilerTemporal
    
    [Required]
    public int InmuebleId { get; set; }
    
    [Required]
    public int InquilinoId { get; set; }
    
    public int? EmpleadoId { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal MontoTotal { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? MontoMensual { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Deposito { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? ComisionInmobiliaria { get; set; }
    
    public DateTime FechaInicio { get; set; }
    
    public DateTime? FechaFin { get; set; }
    
    public int? DuracionMeses { get; set; }
    
    [MaxLength(50)]
    public string? Estado { get; set; } = "Activo"; // Activo, Finalizado, Cancelado, Suspendido
    
    [MaxLength(1000)]
    public string? Condiciones { get; set; }
    
    [MaxLength(1000)]
    public string? Observaciones { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    public DateTime? FechaActualizacion { get; set; }
    
    public DateTime? FechaEliminacion { get; set; }
    
    public bool Activo { get; set; } = true;
    
    // Property for compatibility with reports
    public decimal MontoAlquiler => MontoMensual ?? MontoTotal;
    
    // Relaciones
    public virtual Inmueble Inmueble { get; set; } = null!;
    public virtual Inquilino Inquilino { get; set; } = null!;
    public virtual Empleado? Empleado { get; set; }
    
    public virtual ICollection<PagoAlquiler> Pagos { get; set; } = new List<PagoAlquiler>();
} 
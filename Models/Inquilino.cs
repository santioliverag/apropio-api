using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apropio.API.Models;

public class Inquilino
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Apellido { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string Dni { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Email { get; set; }
    
    [MaxLength(20)]
    public string? Telefono { get; set; }
    
    [MaxLength(20)]
    public string? Celular { get; set; }
    
    [MaxLength(300)]
    public string? Direccion { get; set; }
    
    [MaxLength(100)]
    public string? Ciudad { get; set; }
    
    [MaxLength(50)]
    public string? Provincia { get; set; }
    
    [MaxLength(10)]
    public string? CodigoPostal { get; set; }
    
    public DateTime? FechaNacimiento { get; set; }
    
    [MaxLength(50)]
    public string? EstadoCivil { get; set; }
    
    [MaxLength(50)]
    public string? Nacionalidad { get; set; }
    
    // Información laboral
    [MaxLength(200)]
    public string? Empresa { get; set; }
    
    [MaxLength(100)]
    public string? Cargo { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Salario { get; set; }
    
    [MaxLength(50)]
    public string? TipoContrato { get; set; }
    
    public DateTime? FechaInicioTrabajo { get; set; }
    
    [MaxLength(20)]
    public string? TelefonoTrabajo { get; set; }
    
    [MaxLength(300)]
    public string? DireccionTrabajo { get; set; }
    
    // Información financiera
    [MaxLength(100)]
    public string? Banco { get; set; }
    
    [MaxLength(50)]
    public string? TipoCuenta { get; set; }
    
    [MaxLength(50)]
    public string? NumeroCuenta { get; set; }
    
    [MaxLength(50)]
    public string? Cbu { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? IngresosMensuales { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? OtrosIngresos { get; set; }
    
    [MaxLength(500)]
    public string? Observaciones { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    public DateTime? FechaActualizacion { get; set; }
    
    public DateTime? FechaEliminacion { get; set; }
    
    public bool Activo { get; set; } = true;
    
    // Relaciones
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    
    public virtual ICollection<ReferenciaInquilino> Referencias { get; set; } = new List<ReferenciaInquilino>();
    
    public virtual ICollection<Visita> Visitas { get; set; } = new List<Visita>();
} 
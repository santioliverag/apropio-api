using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apropio.API.Models;

public class Empleado
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
    [Required]
    [MaxLength(100)]
    public string Cargo { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Tipo { get; set; } = string.Empty; // Administrador, Corredor, Vendedor, Asistente, etc.
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? SalarioBase { get; set; }
    
    [Column(TypeName = "decimal(5,2)")]
    public decimal? PorcentajeComision { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? ComisionFija { get; set; }
    
    public DateTime FechaIngreso { get; set; } = DateTime.Now;
    
    public DateTime? FechaEgreso { get; set; }
    
    [MaxLength(50)]
    public string? TipoContrato { get; set; }
    
    [MaxLength(50)]
    public string? Estado { get; set; } = "Activo"; // Activo, Inactivo, Suspendido
    
    // Información bancaria
    [MaxLength(100)]
    public string? Banco { get; set; }
    
    [MaxLength(50)]
    public string? TipoCuenta { get; set; }
    
    [MaxLength(50)]
    public string? NumeroCuenta { get; set; }
    
    [MaxLength(50)]
    public string? Cbu { get; set; }
    
    [MaxLength(50)]
    public string? Cuil { get; set; }
    
    [MaxLength(500)]
    public string? Observaciones { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    public DateTime? FechaActualizacion { get; set; }
    
    public bool Activo { get; set; } = true;
    
    // Relaciones
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    
    public virtual ICollection<Visita> Visitas { get; set; } = new List<Visita>();
} 
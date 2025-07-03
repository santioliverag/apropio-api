using System.ComponentModel.DataAnnotations;

namespace Apropio.API.Models;

public class Visita
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int InmuebleId { get; set; }
    
    public int? InquilinoId { get; set; }
    
    public int? EmpleadoId { get; set; }
    
    [Required]
    public DateTime FechaHora { get; set; }
    
    [MaxLength(100)]
    public string? NombreVisitante { get; set; }
    
    [MaxLength(20)]
    public string? TelefonoVisitante { get; set; }
    
    [MaxLength(200)]
    public string? EmailVisitante { get; set; }
    
    [MaxLength(50)]
    public string? Estado { get; set; } = "Programada"; // Programada, Realizada, Cancelada, NoAsistio
    
    [MaxLength(1000)]
    public string? Observaciones { get; set; }
    
    [MaxLength(1000)]
    public string? Comentarios { get; set; }
    
    public int? Calificacion { get; set; } // 1-5 estrellas
    
    public bool? Interesado { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    public DateTime? FechaActualizacion { get; set; }
    
    public DateTime? FechaEliminacion { get; set; }
    
    public bool Activo { get; set; } = true;
    
    // Relaciones
    public virtual Inmueble Inmueble { get; set; } = null!;
    public virtual Inquilino? Inquilino { get; set; }
    public virtual Empleado? Empleado { get; set; }
} 
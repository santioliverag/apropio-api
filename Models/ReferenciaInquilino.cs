using System.ComponentModel.DataAnnotations;

namespace Apropio.API.Models;

public class ReferenciaInquilino
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int InquilinoId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Tipo { get; set; } = string.Empty; // Personal, Laboral, Comercial, Inmobiliaria
    
    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? Apellido { get; set; }
    
    [MaxLength(200)]
    public string? Empresa { get; set; }
    
    [MaxLength(100)]
    public string? Cargo { get; set; }
    
    [MaxLength(20)]
    public string? Telefono { get; set; }
    
    [MaxLength(200)]
    public string? Email { get; set; }
    
    [MaxLength(300)]
    public string? Direccion { get; set; }
    
    [MaxLength(50)]
    public string? Relacion { get; set; }
    
    public DateTime? FechaVerificacion { get; set; }
    
    [MaxLength(50)]
    public string? EstadoVerificacion { get; set; } = "Pendiente"; // Pendiente, Verificada, NoVerificada
    
    [MaxLength(1000)]
    public string? Comentarios { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    public DateTime? FechaActualizacion { get; set; }
    
    public bool Activo { get; set; } = true;
    
    // Relación
    public virtual Inquilino Inquilino { get; set; } = null!;
} 
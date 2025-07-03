using System.ComponentModel.DataAnnotations;

namespace Apropio.API.Models;

public class Propietario
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
    public string? Alias { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    public DateTime? FechaActualizacion { get; set; }
    
    public bool Activo { get; set; } = true;
    
    // Relaciones
    public virtual ICollection<Inmueble> Inmuebles { get; set; } = new List<Inmueble>();
} 
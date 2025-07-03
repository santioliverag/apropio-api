using System.ComponentModel.DataAnnotations;

namespace Apropio.API.Models;

public class ImagenInmueble
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int InmuebleId { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string? Descripcion { get; set; }
    
    public int Orden { get; set; } = 0;
    
    public bool EsPrincipal { get; set; } = false;
    
    public DateTime FechaSubida { get; set; } = DateTime.Now;
    
    // Relación
    public virtual Inmueble Inmueble { get; set; } = null!;
} 
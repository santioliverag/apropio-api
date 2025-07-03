using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apropio.API.Models;

public class PagoAlquiler
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int ContratoId { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Monto { get; set; }
    
    [Required]
    public DateTime FechaPago { get; set; }
    
    [Required]
    public DateTime FechaVencimiento { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string MetodoPago { get; set; } = string.Empty; // Efectivo, Transferencia, Cheque, Tarjeta
    
    [MaxLength(100)]
    public string? NumeroComprobante { get; set; }
    
    [MaxLength(50)]
    public string? Estado { get; set; } = "Pendiente"; // Pendiente, Pagado, Vencido, Parcial
    
    [MaxLength(500)]
    public string? Observaciones { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    public DateTime? FechaActualizacion { get; set; }
    
    public bool Activo { get; set; } = true;
    
    // Relación
    public virtual Contrato Contrato { get; set; } = null!;
} 
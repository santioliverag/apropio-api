using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apropio.API.Models;

public class Inmueble
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Titulo { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Descripcion { get; set; }
    
    [Required]
    [MaxLength(300)]
    public string Direccion { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Ciudad { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string Provincia { get; set; } = string.Empty;
    
    [MaxLength(10)]
    public string? CodigoPostal { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Tipo { get; set; } = string.Empty; // Casa, Departamento, Local, Oficina, etc.
    
    [Required]
    [MaxLength(50)]
    public string Operacion { get; set; } = string.Empty; // Venta, Alquiler, AlquilerTemporal
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Superficie { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? SuperficieCubierta { get; set; }
    
    public int? Habitaciones { get; set; }
    
    public int? Baños { get; set; }
    
    public int? Cocheras { get; set; }
    
    public bool TieneAscensor { get; set; }
    
    public bool TieneBalcon { get; set; }
    
    public bool TieneTerraza { get; set; }
    
    public bool TienePiscina { get; set; }
    
    public bool TieneJardin { get; set; }
    
    public bool TieneParrilla { get; set; }
    
    public bool TienePortero { get; set; }
    
    public bool TieneSeguridad { get; set; }
    
    public bool TieneGimnasio { get; set; }
    
    public bool TieneSalon { get; set; }
    
    [MaxLength(50)]
    public string? Estado { get; set; } = "Disponible"; // Disponible, Reservado, Vendido, Alquilado
    
    [MaxLength(500)]
    public string? Observaciones { get; set; }
    
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    
    public DateTime? FechaActualizacion { get; set; }
    
    public bool Activo { get; set; } = true;
    
    // Relaciones
    public int? PropietarioId { get; set; }
    public virtual Propietario? Propietario { get; set; }
    
    public virtual ICollection<ImagenInmueble> Imagenes { get; set; } = new List<ImagenInmueble>();
    
    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    
    public virtual ICollection<Visita> Visitas { get; set; } = new List<Visita>();
} 
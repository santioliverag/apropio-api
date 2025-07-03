using System.ComponentModel.DataAnnotations;

namespace Apropio.API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string NombreUsuario { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string? Telefono { get; set; }
        
        public TipoUsuario TipoUsuario { get; set; }
        
        public bool Activo { get; set; } = true;
        
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        
        public DateTime? UltimoAcceso { get; set; }
        
        public DateTime? FechaEliminacion { get; set; }
        
        // Propiedades de navegación
        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
    
    public enum TipoUsuario
    {
        Administrador = 1,
        Empleado = 2,
        Cliente = 3,
        Solo_Lectura = 4
    }
} 
using System.ComponentModel.DataAnnotations;
using Apropio.API.Models;

namespace Apropio.API.DTOs
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
    }
    
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public string NombreUsuario { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Nombres { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = string.Empty;
        
        [StringLength(20)]
        public string? Telefono { get; set; }
        
        public TipoUsuario TipoUsuario { get; set; } = TipoUsuario.Cliente;
    }
    
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
    }
    
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string TipoUsuario { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? UltimoAcceso { get; set; }
    }
    
    public class ChangePasswordDto
    {
        [Required]
        public string PasswordActual { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordNuevo { get; set; } = string.Empty;
    }
} 
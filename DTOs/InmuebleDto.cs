namespace Apropio.API.DTOs;

public class InmuebleDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public string? CodigoPostal { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Operacion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public decimal? Superficie { get; set; }
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
    public string? Estado { get; set; }
    public string? Observaciones { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaActualizacion { get; set; }
    public bool Activo { get; set; }
    public int? PropietarioId { get; set; }
    public string? PropietarioNombre { get; set; }
    public List<ImagenInmuebleDto> Imagenes { get; set; } = new List<ImagenInmuebleDto>();
}

public class ImagenInmuebleDto
{
    public int Id { get; set; }
    public int InmuebleId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int Orden { get; set; }
    public bool EsPrincipal { get; set; }
    public DateTime FechaSubida { get; set; }
}

public class CreateInmuebleDto
{
    public string Titulo { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;
    public string Provincia { get; set; } = string.Empty;
    public string? CodigoPostal { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Operacion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public decimal? Superficie { get; set; }
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
    public string? Estado { get; set; }
    public string? Observaciones { get; set; }
    public int? PropietarioId { get; set; }
} 
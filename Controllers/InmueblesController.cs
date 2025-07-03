using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apropio.API.Data;
using Apropio.API.Models;
using Apropio.API.DTOs;

namespace Apropio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InmueblesController : ControllerBase
{
    private readonly ApropiDbContext _context;

    public InmueblesController(ApropiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InmuebleDto>>> GetInmuebles(
        [FromQuery] string? ciudad = null,
        [FromQuery] string? tipo = null,
        [FromQuery] string? operacion = null,
        [FromQuery] decimal? precioMin = null,
        [FromQuery] decimal? precioMax = null,
        [FromQuery] int? habitaciones = null,
        [FromQuery] int? baños = null,
        [FromQuery] bool? tieneAscensor = null,
        [FromQuery] bool? tieneBalcon = null,
        [FromQuery] bool? tienePiscina = null,
        [FromQuery] string? estado = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Inmuebles
            .Include(i => i.Propietario)
            .Include(i => i.Imagenes)
            .Where(i => i.Activo);

        // Aplicar filtros
        if (!string.IsNullOrEmpty(ciudad))
            query = query.Where(i => i.Ciudad.Contains(ciudad));

        if (!string.IsNullOrEmpty(tipo))
            query = query.Where(i => i.TipoInmueble == tipo);

        if (!string.IsNullOrEmpty(operacion))
            query = query.Where(i => i.Operacion == operacion);

        if (precioMin.HasValue)
            query = query.Where(i => i.Precio >= precioMin);

        if (precioMax.HasValue)
            query = query.Where(i => i.Precio <= precioMax);

        if (habitaciones.HasValue)
            query = query.Where(i => i.Habitaciones == habitaciones);

        if (baños.HasValue)
            query = query.Where(i => i.Baños == baños);

        if (tieneAscensor.HasValue)
            query = query.Where(i => i.TieneAscensor == tieneAscensor);

        if (tieneBalcon.HasValue)
            query = query.Where(i => i.TieneBalcon == tieneBalcon);

        if (tienePiscina.HasValue)
            query = query.Where(i => i.TienePiscina == tienePiscina);

        if (!string.IsNullOrEmpty(estado))
            query = query.Where(i => i.Estado == estado);

        // Paginación
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var inmuebles = await query
            .OrderByDescending(i => i.FechaCreacion)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var inmuebleDtos = inmuebles.Select(i => new InmuebleDto
        {
            Id = i.Id,
            Titulo = i.Titulo,
            Descripcion = i.Descripcion,
            Direccion = i.Direccion,
            Ciudad = i.Ciudad,
            Provincia = i.Provincia,
            CodigoPostal = i.CodigoPostal,
            Tipo = i.TipoInmueble,
            Operacion = i.Operacion,
            Precio = i.Precio,
            Superficie = i.Superficie,
            SuperficieCubierta = i.SuperficieCubierta,
            Habitaciones = i.Habitaciones,
            Baños = i.Baños,
            Cocheras = i.Cocheras,
            TieneAscensor = i.TieneAscensor,
            TieneBalcon = i.TieneBalcon,
            TieneTerraza = i.TieneTerraza,
            TienePiscina = i.TienePiscina,
            TieneJardin = i.TieneJardin,
            TieneParrilla = i.TieneParrilla,
            TienePortero = i.TienePortero,
            TieneSeguridad = i.TieneSeguridad,
            TieneGimnasio = i.TieneGimnasio,
            TieneSalon = i.TieneSalon,
            Estado = i.Estado,
            Observaciones = i.Observaciones,
            FechaCreacion = i.FechaCreacion,
            FechaActualizacion = i.FechaActualizacion,
            Activo = i.Activo,
            PropietarioId = i.PropietarioId,
            PropietarioNombre = i.Propietario != null ? $"{i.Propietario.Nombre} {i.Propietario.Apellido}" : null,
            Imagenes = i.Imagenes.Select(img => new ImagenInmuebleDto
            {
                Id = img.Id,
                InmuebleId = img.InmuebleId,
                Url = img.Url,
                Descripcion = img.Descripcion,
                Orden = img.Orden,
                EsPrincipal = img.EsPrincipal,
                FechaSubida = img.FechaSubida
            }).OrderBy(img => img.Orden).ToList()
        }).ToList();

        Response.Headers.Add("X-Total-Count", totalItems.ToString());
        Response.Headers.Add("X-Total-Pages", totalPages.ToString());
        Response.Headers.Add("X-Current-Page", page.ToString());
        Response.Headers.Add("X-Page-Size", pageSize.ToString());

        return Ok(inmuebleDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InmuebleDto>> GetInmueble(int id)
    {
        var inmueble = await _context.Inmuebles
            .Include(i => i.Propietario)
            .Include(i => i.Imagenes)
            .FirstOrDefaultAsync(i => i.Id == id && i.Activo);

        if (inmueble == null)
        {
            return NotFound();
        }

        var inmuebleDto = new InmuebleDto
        {
            Id = inmueble.Id,
            Titulo = inmueble.Titulo,
            Descripcion = inmueble.Descripcion,
            Direccion = inmueble.Direccion,
            Ciudad = inmueble.Ciudad,
            Provincia = inmueble.Provincia,
            CodigoPostal = inmueble.CodigoPostal,
            Tipo = inmueble.TipoInmueble,
            Operacion = inmueble.Operacion,
            Precio = inmueble.Precio,
            Superficie = inmueble.Superficie,
            SuperficieCubierta = inmueble.SuperficieCubierta,
            Habitaciones = inmueble.Habitaciones,
            Baños = inmueble.Baños,
            Cocheras = inmueble.Cocheras,
            TieneAscensor = inmueble.TieneAscensor,
            TieneBalcon = inmueble.TieneBalcon,
            TieneTerraza = inmueble.TieneTerraza,
            TienePiscina = inmueble.TienePiscina,
            TieneJardin = inmueble.TieneJardin,
            TieneParrilla = inmueble.TieneParrilla,
            TienePortero = inmueble.TienePortero,
            TieneSeguridad = inmueble.TieneSeguridad,
            TieneGimnasio = inmueble.TieneGimnasio,
            TieneSalon = inmueble.TieneSalon,
            Estado = inmueble.Estado,
            Observaciones = inmueble.Observaciones,
            FechaCreacion = inmueble.FechaCreacion,
            FechaActualizacion = inmueble.FechaActualizacion,
            Activo = inmueble.Activo,
            PropietarioId = inmueble.PropietarioId,
            PropietarioNombre = inmueble.Propietario != null ? $"{inmueble.Propietario.Nombre} {inmueble.Propietario.Apellido}" : null,
            Imagenes = inmueble.Imagenes.Select(img => new ImagenInmuebleDto
            {
                Id = img.Id,
                InmuebleId = img.InmuebleId,
                Url = img.Url,
                Descripcion = img.Descripcion,
                Orden = img.Orden,
                EsPrincipal = img.EsPrincipal,
                FechaSubida = img.FechaSubida
            }).OrderBy(img => img.Orden).ToList()
        };

        return Ok(inmuebleDto);
    }

    [HttpPost]
    public async Task<ActionResult<InmuebleDto>> CreateInmueble(CreateInmuebleDto createInmuebleDto)
    {
        var inmueble = new Inmueble
        {
            Titulo = createInmuebleDto.Titulo,
            Descripcion = createInmuebleDto.Descripcion,
            Direccion = createInmuebleDto.Direccion,
            Ciudad = createInmuebleDto.Ciudad,
            Provincia = createInmuebleDto.Provincia,
            CodigoPostal = createInmuebleDto.CodigoPostal,
            TipoInmueble = createInmuebleDto.Tipo,
            Operacion = createInmuebleDto.Operacion,
            Precio = createInmuebleDto.Precio,
            Superficie = createInmuebleDto.Superficie,
            SuperficieCubierta = createInmuebleDto.SuperficieCubierta,
            Habitaciones = createInmuebleDto.Habitaciones,
            Baños = createInmuebleDto.Baños,
            Cocheras = createInmuebleDto.Cocheras,
            TieneAscensor = createInmuebleDto.TieneAscensor,
            TieneBalcon = createInmuebleDto.TieneBalcon,
            TieneTerraza = createInmuebleDto.TieneTerraza,
            TienePiscina = createInmuebleDto.TienePiscina,
            TieneJardin = createInmuebleDto.TieneJardin,
            TieneParrilla = createInmuebleDto.TieneParrilla,
            TienePortero = createInmuebleDto.TienePortero,
            TieneSeguridad = createInmuebleDto.TieneSeguridad,
            TieneGimnasio = createInmuebleDto.TieneGimnasio,
            TieneSalon = createInmuebleDto.TieneSalon,
            Estado = createInmuebleDto.Estado ?? "Disponible",
            Observaciones = createInmuebleDto.Observaciones,
            PropietarioId = createInmuebleDto.PropietarioId,
            FechaCreacion = DateTime.Now,
            Activo = true
        };

        _context.Inmuebles.Add(inmueble);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInmueble), new { id = inmueble.Id }, inmueble);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInmueble(int id, CreateInmuebleDto updateInmuebleDto)
    {
        var inmueble = await _context.Inmuebles.FindAsync(id);

        if (inmueble == null || !inmueble.Activo)
        {
            return NotFound();
        }

        inmueble.Titulo = updateInmuebleDto.Titulo;
        inmueble.Descripcion = updateInmuebleDto.Descripcion;
        inmueble.Direccion = updateInmuebleDto.Direccion;
        inmueble.Ciudad = updateInmuebleDto.Ciudad;
        inmueble.Provincia = updateInmuebleDto.Provincia;
        inmueble.CodigoPostal = updateInmuebleDto.CodigoPostal;
        inmueble.TipoInmueble = updateInmuebleDto.Tipo;
        inmueble.Operacion = updateInmuebleDto.Operacion;
        inmueble.Precio = updateInmuebleDto.Precio;
        inmueble.Superficie = updateInmuebleDto.Superficie;
        inmueble.SuperficieCubierta = updateInmuebleDto.SuperficieCubierta;
        inmueble.Habitaciones = updateInmuebleDto.Habitaciones;
        inmueble.Baños = updateInmuebleDto.Baños;
        inmueble.Cocheras = updateInmuebleDto.Cocheras;
        inmueble.TieneAscensor = updateInmuebleDto.TieneAscensor;
        inmueble.TieneBalcon = updateInmuebleDto.TieneBalcon;
        inmueble.TieneTerraza = updateInmuebleDto.TieneTerraza;
        inmueble.TienePiscina = updateInmuebleDto.TienePiscina;
        inmueble.TieneJardin = updateInmuebleDto.TieneJardin;
        inmueble.TieneParrilla = updateInmuebleDto.TieneParrilla;
        inmueble.TienePortero = updateInmuebleDto.TienePortero;
        inmueble.TieneSeguridad = updateInmuebleDto.TieneSeguridad;
        inmueble.TieneGimnasio = updateInmuebleDto.TieneGimnasio;
        inmueble.TieneSalon = updateInmuebleDto.TieneSalon;
        inmueble.Estado = updateInmuebleDto.Estado ?? inmueble.Estado;
        inmueble.Observaciones = updateInmuebleDto.Observaciones;
        inmueble.PropietarioId = updateInmuebleDto.PropietarioId;
        inmueble.FechaActualizacion = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InmuebleExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInmueble(int id)
    {
        var inmueble = await _context.Inmuebles.FindAsync(id);
        if (inmueble == null)
        {
            return NotFound();
        }

        // Soft delete
        inmueble.Activo = false;
        inmueble.FechaActualizacion = DateTime.Now;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool InmuebleExists(int id)
    {
        return _context.Inmuebles.Any(e => e.Id == id);
    }
} 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apropio.API.Data;
using Apropio.API.Models;

namespace Apropio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InquilinosController : ControllerBase
{
    private readonly ApropiDbContext _context;

    public InquilinosController(ApropiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Inquilino>>> GetInquilinos(
        [FromQuery] string? busqueda = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Inquilinos.Where(i => i.Activo);

        if (!string.IsNullOrEmpty(busqueda))
        {
            query = query.Where(i => i.Nombre.Contains(busqueda) || 
                                   i.Apellido.Contains(busqueda) || 
                                   i.Dni.Contains(busqueda) ||
                                   i.Email!.Contains(busqueda));
        }

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var inquilinos = await query
            .OrderBy(i => i.Apellido)
            .ThenBy(i => i.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        Response.Headers.Add("X-Total-Count", totalItems.ToString());
        Response.Headers.Add("X-Total-Pages", totalPages.ToString());
        Response.Headers.Add("X-Current-Page", page.ToString());
        Response.Headers.Add("X-Page-Size", pageSize.ToString());

        return Ok(inquilinos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Inquilino>> GetInquilino(int id)
    {
        var inquilino = await _context.Inquilinos
            .Include(i => i.Referencias)
            .FirstOrDefaultAsync(i => i.Id == id && i.Activo);

        if (inquilino == null)
        {
            return NotFound();
        }

        return inquilino;
    }

    [HttpPost]
    public async Task<ActionResult<Inquilino>> CreateInquilino(Inquilino inquilino)
    {
        inquilino.FechaCreacion = DateTime.Now;
        inquilino.Activo = true;

        _context.Inquilinos.Add(inquilino);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInquilino), new { id = inquilino.Id }, inquilino);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInquilino(int id, Inquilino inquilino)
    {
        if (id != inquilino.Id)
        {
            return BadRequest();
        }

        var existingInquilino = await _context.Inquilinos.FindAsync(id);
        if (existingInquilino == null || !existingInquilino.Activo)
        {
            return NotFound();
        }

        // Actualizar propiedades
        existingInquilino.Nombre = inquilino.Nombre;
        existingInquilino.Apellido = inquilino.Apellido;
        existingInquilino.Dni = inquilino.Dni;
        existingInquilino.Email = inquilino.Email;
        existingInquilino.Telefono = inquilino.Telefono;
        existingInquilino.Celular = inquilino.Celular;
        existingInquilino.Direccion = inquilino.Direccion;
        existingInquilino.Ciudad = inquilino.Ciudad;
        existingInquilino.Provincia = inquilino.Provincia;
        existingInquilino.CodigoPostal = inquilino.CodigoPostal;
        existingInquilino.FechaNacimiento = inquilino.FechaNacimiento;
        existingInquilino.EstadoCivil = inquilino.EstadoCivil;
        existingInquilino.Nacionalidad = inquilino.Nacionalidad;
        existingInquilino.Empresa = inquilino.Empresa;
        existingInquilino.Cargo = inquilino.Cargo;
        existingInquilino.Salario = inquilino.Salario;
        existingInquilino.TipoContrato = inquilino.TipoContrato;
        existingInquilino.FechaInicioTrabajo = inquilino.FechaInicioTrabajo;
        existingInquilino.TelefonoTrabajo = inquilino.TelefonoTrabajo;
        existingInquilino.DireccionTrabajo = inquilino.DireccionTrabajo;
        existingInquilino.Banco = inquilino.Banco;
        existingInquilino.TipoCuenta = inquilino.TipoCuenta;
        existingInquilino.NumeroCuenta = inquilino.NumeroCuenta;
        existingInquilino.Cbu = inquilino.Cbu;
        existingInquilino.IngresosMensuales = inquilino.IngresosMensuales;
        existingInquilino.OtrosIngresos = inquilino.OtrosIngresos;
        existingInquilino.Observaciones = inquilino.Observaciones;
        existingInquilino.FechaActualizacion = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!InquilinoExists(id))
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
    public async Task<IActionResult> DeleteInquilino(int id)
    {
        var inquilino = await _context.Inquilinos.FindAsync(id);
        if (inquilino == null)
        {
            return NotFound();
        }

        // Soft delete
        inquilino.Activo = false;
        inquilino.FechaActualizacion = DateTime.Now;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool InquilinoExists(int id)
    {
        return _context.Inquilinos.Any(e => e.Id == id);
    }
} 
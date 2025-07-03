using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Apropio.API.Data;
using Apropio.API.Models;

namespace Apropio.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpleadosController : ControllerBase
{
    private readonly ApropiDbContext _context;

    public EmpleadosController(ApropiDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Empleado>>> GetEmpleados(
        [FromQuery] string? tipo = null,
        [FromQuery] string? estado = null,
        [FromQuery] string? busqueda = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = _context.Empleados.Where(e => e.Activo);

        if (!string.IsNullOrEmpty(tipo))
            query = query.Where(e => e.TipoEmpleado == tipo);

        if (!string.IsNullOrEmpty(estado))
            query = query.Where(e => e.Estado == estado);

        if (!string.IsNullOrEmpty(busqueda))
        {
            query = query.Where(e => e.Nombres.Contains(busqueda) || 
                                   e.Apellidos.Contains(busqueda) || 
                                   e.Dni.Contains(busqueda) ||
                                   e.Email!.Contains(busqueda));
        }

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var empleados = await query
            .OrderBy(e => e.Apellidos)
            .ThenBy(e => e.Nombres)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        Response.Headers.Add("X-Total-Count", totalItems.ToString());
        Response.Headers.Add("X-Total-Pages", totalPages.ToString());
        Response.Headers.Add("X-Current-Page", page.ToString());
        Response.Headers.Add("X-Page-Size", pageSize.ToString());

        return Ok(empleados);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Empleado>> GetEmpleado(int id)
    {
        var empleado = await _context.Empleados
            .FirstOrDefaultAsync(e => e.Id == id && e.Activo);

        if (empleado == null)
        {
            return NotFound();
        }

        return empleado;
    }

    [HttpPost]
    public async Task<ActionResult<Empleado>> CreateEmpleado(Empleado empleado)
    {
        empleado.FechaCreacion = DateTime.Now;
        empleado.FechaIngreso = empleado.FechaIngreso == DateTime.MinValue ? DateTime.Now : empleado.FechaIngreso;
        empleado.Estado = empleado.Estado ?? "Activo";
        empleado.Activo = true;

        _context.Empleados.Add(empleado);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEmpleado), new { id = empleado.Id }, empleado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmpleado(int id, Empleado empleado)
    {
        if (id != empleado.Id)
        {
            return BadRequest();
        }

        var existingEmpleado = await _context.Empleados.FindAsync(id);
        if (existingEmpleado == null || !existingEmpleado.Activo)
        {
            return NotFound();
        }

        // Actualizar propiedades
        existingEmpleado.Nombres = empleado.Nombres;
        existingEmpleado.Apellidos = empleado.Apellidos;
        existingEmpleado.Dni = empleado.Dni;
        existingEmpleado.Email = empleado.Email;
        existingEmpleado.Telefono = empleado.Telefono;
        existingEmpleado.Celular = empleado.Celular;
        existingEmpleado.Direccion = empleado.Direccion;
        existingEmpleado.Ciudad = empleado.Ciudad;
        existingEmpleado.Provincia = empleado.Provincia;
        existingEmpleado.CodigoPostal = empleado.CodigoPostal;
        existingEmpleado.FechaNacimiento = empleado.FechaNacimiento;
        existingEmpleado.EstadoCivil = empleado.EstadoCivil;
        existingEmpleado.Nacionalidad = empleado.Nacionalidad;
        existingEmpleado.Cargo = empleado.Cargo;
        existingEmpleado.TipoEmpleado = empleado.TipoEmpleado;
        existingEmpleado.SalarioBase = empleado.SalarioBase;
        existingEmpleado.PorcentajeComision = empleado.PorcentajeComision;
        existingEmpleado.ComisionFija = empleado.ComisionFija;
        existingEmpleado.FechaIngreso = empleado.FechaIngreso;
        existingEmpleado.FechaEgreso = empleado.FechaEgreso;
        existingEmpleado.TipoContrato = empleado.TipoContrato;
        existingEmpleado.Estado = empleado.Estado;
        existingEmpleado.Banco = empleado.Banco;
        existingEmpleado.TipoCuenta = empleado.TipoCuenta;
        existingEmpleado.NumeroCuenta = empleado.NumeroCuenta;
        existingEmpleado.Cbu = empleado.Cbu;
        existingEmpleado.Cuil = empleado.Cuil;
        existingEmpleado.Observaciones = empleado.Observaciones;
        existingEmpleado.FechaActualizacion = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EmpleadoExists(id))
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
    public async Task<IActionResult> DeleteEmpleado(int id)
    {
        var empleado = await _context.Empleados.FindAsync(id);
        if (empleado == null)
        {
            return NotFound();
        }

        // Soft delete
        empleado.Activo = false;
        empleado.FechaActualizacion = DateTime.Now;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EmpleadoExists(int id)
    {
        return _context.Empleados.Any(e => e.Id == id);
    }
} 
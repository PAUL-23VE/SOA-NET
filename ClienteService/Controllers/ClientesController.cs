using Microsoft.AspNetCore.Mvc;
using ClienteService.Data;
using ClienteService.Models;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CrearCliente([FromBody] Cliente cliente)
    {
        cliente.Id = 1; // Simular creación
        return CreatedAtAction(nameof(ObtenerCliente), new { id = cliente.Id }, cliente);
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerCliente(int id)
    {
        var cliente = _context.Cliente.Find(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }
}
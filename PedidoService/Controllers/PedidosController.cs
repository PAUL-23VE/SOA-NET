using Microsoft.AspNetCore.Mvc;
using PedidoService.Data;
using PedidoService.Models;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly AppDbContext _context;

    public PedidosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult CrearPedido([FromBody] Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();
        return CreatedAtAction(nameof(ObtenerPedido), new { id = pedido.Id }, pedido);
    }
    [HttpGet("{id}")]
    public IActionResult ObtenerPedido(int id)
    {
        var pedido = _context.Pedidos.Find(id);
        if (pedido == null) return NotFound();
        return Ok(pedido);
    }
}

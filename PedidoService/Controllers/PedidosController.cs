using Microsoft.AspNetCore.Mvc;
using PedidoService.Data;
using PedidoService.Models;
using System.Net.Http;
using System.Threading.Tasks;

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
    public async Task<IActionResult> CrearPedido([FromBody] Pedido pedido)
    {
        // Validar cliente llamando a ClienteService
        using var httpClient = new HttpClient();
        var clienteResponse = await httpClient.GetAsync($"https://localhost:5002/api/clientes/{pedido.ClienteId}");
        if (!clienteResponse.IsSuccessStatusCode)
        {
            return BadRequest("Cliente no válido.");
        }

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

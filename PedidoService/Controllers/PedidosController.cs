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
}

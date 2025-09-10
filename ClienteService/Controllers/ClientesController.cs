using System.Net.Http.Json;
using ClienteService.Data;
using ClienteService.models;
using Microsoft.AspNetCore.Mvc;

namespace ClienteService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteDbContext _context;
        private readonly HttpClient _httpClient;

        public ClientesController(ClienteDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient("PedidoService");
        }

        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObtenerCliente), new { id = cliente.Id }, cliente);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerCliente(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpGet("{clienteId}/pedido/{pedidoId}")]
        public async Task<IActionResult> ObtenerClienteConPedido(int clienteId, int pedidoId)
        {
            var cliente = _context.Clientes.Find(clienteId);
            if (cliente == null) return NotFound("Cliente no encontrado");

            var response = await _httpClient.GetAsync($"api/pedidos/{pedidoId}");
            if (!response.IsSuccessStatusCode) return NotFound("Pedido no encontrado");

            var pedido = await response.Content.ReadFromJsonAsync<Pedido>();
            return Ok(new { Cliente = cliente, Pedido = pedido });
        }
    }

    public class Pedido
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
    }
}

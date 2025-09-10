using ClienteService.models;
using Microsoft.AspNetCore.Mvc;

namespace ClienteService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            cliente.Id = 1; // Simular creación
            return CreatedAtAction(nameof(ObtenerCliente), new { id = cliente.Id }, cliente);
        }

        /* public IActionResult Index()
         {
             return View();
         }
       */
    }
}

namespace PedidoService.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }

        // Relación con Cliente
        public int ClienteId { get; set; }
    }

}

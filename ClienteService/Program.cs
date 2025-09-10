using ClienteService.Data;
using Microsoft.EntityFrameworkCore;

namespace ClienteService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔹 Configuración de EF Core con SQL Server
            builder.Services.AddDbContext<ClienteDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // 🔹 Configuración de HttpClient para llamar a PedidoService
            builder.Services.AddHttpClient("PedidoService", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7144/"); // Dirección de PedidoService
            });

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

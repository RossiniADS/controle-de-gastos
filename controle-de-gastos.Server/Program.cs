using controle_de_gastos.Server.Interfaces;
using controle_de_gastos.Server.Services;

namespace controle_de_gastos.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controllers
            builder.Services.AddControllers();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("dev",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            builder.Services.AddScoped<IPessoaService, PessoaService>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<ITransacaoService, TransacaoService>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseCors("dev");

            app.UseAuthorization();

            app.MapControllers();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}

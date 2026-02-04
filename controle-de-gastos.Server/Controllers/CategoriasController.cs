using Microsoft.AspNetCore.Mvc;
using controle_de_gastos.Server.Entities;
using controle_de_gastos.Server.Interfaces;

namespace controle_de_gastos.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // Endpoint GET: api/categorias
        // Retorna a lista completa de categorias cadastradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            return Ok(await _categoriaService.ListarTodasAsync());
        }

        // Endpoint POST: api/categorias
        // Recebe uma nova categoria no corpo da requisição e salva no sistema
        [HttpPost]
        public async Task<ActionResult<Categoria>> Post(Categoria categoria)
        {
            var novaCategoria = await _categoriaService.CriarAsync(categoria);

            return CreatedAtAction(nameof(Get), new { id = novaCategoria.Id }, novaCategoria);
        }

        // Endpoint GET: api/categorias/totais
        // Retorna o total de gastos agrupados por categoria
        [HttpGet("totais")]
        public async Task<IActionResult> GetTotais()
        {
            var resultado = await _categoriaService.ObterTotaisPorCategoriaAsync();

            return Ok(resultado);
        }
    }
}

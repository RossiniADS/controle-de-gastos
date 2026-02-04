using Microsoft.AspNetCore.Mvc;
using controle_de_gastos.Server.Models;
using controle_de_gastos.Server.Interfaces;

namespace controle_de_gastos.Server.Controllers
{
    // A rota base fica no padrão: api/pessoas
    // Controller responsavel por expor os endpoints relacionados as pessoas
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        // Endpoint GET: api/pessoas
        // Retorna todas as pessoas cadastradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaResponse>>> Get()
        {
            return Ok(await _pessoaService.ListarTodasAsync());
        }

        // Endpoint POST: api/pessoas
        // Cria um novo registro de pessoa
        [HttpPost]
        public async Task<ActionResult<PessoaResponse>> Post(PessoaRequest request)
        {
            var novaPessoa = await _pessoaService.CriarAsync(request);
            return CreatedAtAction(nameof(Get), new { id = novaPessoa.Id }, novaPessoa);
        }

        // Endpoint PUT: api/pessoas/{id}
        // Atualiza os dados de uma pessoa existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PessoaRequest request)
        {
            var atualizada = await _pessoaService.AtualizarAsync(id, request);
            if (atualizada == null) return NotFound();
            return NoContent();
        }

        // Endpoint DELETE: api/pessoas/{id}
        // Remove uma pessoa pelo id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletado = await _pessoaService.DeletarAsync(id);
            if (!deletado) return NotFound();
            return NoContent();
        }

        // Endpoint GET: api/pessoas/totais
        // Retorna totais de gastos por pessoa + um resumo geral
        [HttpGet("totais")]
        public async Task<IActionResult> GetTotais()
        {
            var totais = await _pessoaService.ObterTotaisPorPessoaAsync();
            var resumo = await _pessoaService.ObterResumoGeralAsync();

            return Ok(new { Lista = totais, ResumoGeral = resumo });
        }
    }
}

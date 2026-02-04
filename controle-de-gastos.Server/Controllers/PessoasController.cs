using Microsoft.AspNetCore.Mvc;
using controle_de_gastos.Server.Entities;
using controle_de_gastos.Server.Interfaces;

namespace controle_de_gastos.Server.Controllers
{
    // Controller responsável por expor os endpoints relacionados às pessoas
    // A rota base fica no padrão: api/pessoas
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
        public async Task<ActionResult<IEnumerable<Pessoa>>> Get()
        {
            return Ok(await _pessoaService.ListarTodasAsync());
        }

        // Endpoint POST: api/pessoas
        // Cria um novo registro de pessoa
        [HttpPost]
        public async Task<ActionResult<Pessoa>> Post(Pessoa pessoa)
        {
            var novaPessoa = await _pessoaService.CriarAsync(pessoa);

            return CreatedAtAction(nameof(Get), new { id = novaPessoa.Id }, novaPessoa);
        }

        // Endpoint PUT: api/pessoas/{id}
        // Atualiza os dados de uma pessoa existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Pessoa pessoa)
        {
            if (id != pessoa.Id) return BadRequest();

            await _pessoaService.AtualizarAsync(pessoa);

            return NoContent();
        }

        // Endpoint DELETE: api/pessoas/{id}
        // Remove uma pessoa pelo id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pessoaService.DeletarAsync(id);

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

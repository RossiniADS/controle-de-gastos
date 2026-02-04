using Microsoft.AspNetCore.Mvc;
using controle_de_gastos.Server.Models;
using controle_de_gastos.Server.Interfaces;

namespace controle_de_gastos.Server.Controllers
{
    // A rota base sera: api/transacoes
    // Controller responsavel pelos endpoints de transacoes financeiras
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacoesController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        // Endpoint GET: api/transacoes
        // Retorna todas as transacoes registradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransacaoResponse>>> Get()
        {
            return Ok(await _transacaoService.ListarTodasAsync());
        }

        // Endpoint GET: api/transacoes/{id}
        // Retorna uma transacao especifica
        [HttpGet("{id}")]
        public async Task<ActionResult<TransacaoResponse>> GetById(Guid id)
        {
            var transacao = await _transacaoService.ObterPorIdAsync(id);
            if (transacao == null)
            {
                return NotFound();
            }

            return Ok(transacao);
        }

        // Endpoint POST: api/transacoes
        // Cria uma nova transacao
        [HttpPost]
        public async Task<ActionResult<TransacaoResponse>> Post(TransacaoRequest request)
        {
            try
            {
                var novaTransacao = await _transacaoService.CriarAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = novaTransacao.Id }, novaTransacao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using controle_de_gastos.Server.Entities;
using controle_de_gastos.Server.Interfaces;

namespace controle_de_gastos.Server.Controllers
{
    // Controller responsável pelos endpoints de transações financeiras
    // A rota base será: api/transacoes
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
        // Retorna todas as transações registradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transacao>>> Get()
        {
            return Ok(await _transacaoService.ListarTodasAsync());
        }

        // Endpoint POST: api/transacoes
        // Cria uma nova transação
        [HttpPost]
        public async Task<ActionResult<Transacao>> Post(Transacao transacao)
        {
            try
            {
                var novaTransacao = await _transacaoService.CriarAsync(transacao);

                return CreatedAtAction(nameof(Get), new { id = novaTransacao.Id }, novaTransacao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

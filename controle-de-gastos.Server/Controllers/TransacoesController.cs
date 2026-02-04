using Microsoft.AspNetCore.Mvc;
using controle_de_gastos.Server.Models;
using controle_de_gastos.Server.Interfaces;

namespace controle_de_gastos.Server.Controllers
{
    // A rota base sera: api/transacoes
    // Controller responsavel pelos endpoints de transações financeiras
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacoesController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        // Endpoint GET: api/transações
        // Retorna todas as transações registradas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransacaoResponse>>> Get()
        {
            return Ok(await _transacaoService.ListarTodasAsync());
        }

        // Endpoint POST: api/transações
        // Cria uma nova transação
        [HttpPost]
        public async Task<ActionResult<TransacaoResponse>> Post(TransacaoRequest request)
        {
            try
            {
                var novaTransacao = await _transacaoService.CriarAsync(request);
                return CreatedAtAction(nameof(Get), new { id = novaTransacao.Id }, novaTransacao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

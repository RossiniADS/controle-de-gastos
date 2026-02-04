using controle_de_gastos.Server.Models;

namespace controle_de_gastos.Server.Interfaces
{
    // Interface que define o contrato do serviço de transações.
    // Aqui são declaradas apenas as operações disponíveis,
    // sem a implementação das regras de negócio.
    public interface ITransacaoService
    {
        // Retorna todas as transações registradas no sistema.
        Task<IEnumerable<TransacaoResponse>> ListarTodasAsync();

        // Busca uma transação específica pelo Id.
        Task<TransacaoResponse> ObterPorIdAsync(Guid id);

        // Cria uma nova transação e retorna o registro criado.
        Task<TransacaoResponse> CriarAsync(TransacaoRequest request);
    }
}

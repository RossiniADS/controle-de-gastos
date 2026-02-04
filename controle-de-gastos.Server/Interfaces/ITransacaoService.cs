using controle_de_gastos.Server.Entities;

namespace controle_de_gastos.Server.Interfaces
{
    // Interface que define o contrato do serviço de transações.
    // Aqui são declaradas apenas as operações disponíveis,
    // sem a implementação das regras de negócio.
    public interface ITransacaoService
    {
        // Retorna todas as transações registradas no sistema.
        Task<IEnumerable<Transacao>> ListarTodasAsync();

        // Cria uma nova transação e retorna o registro criado.
        Task<Transacao> CriarAsync(Transacao transacao);
    }
}

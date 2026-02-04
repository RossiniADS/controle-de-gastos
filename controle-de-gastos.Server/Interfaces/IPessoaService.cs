using controle_de_gastos.Server.Entities;
using controle_de_gastos.Server.Models;

namespace controle_de_gastos.Server.Interfaces
{
    // Interface que define as operações disponíveis
    // para o serviço de pessoas.
    // Serve como um contrato que a implementação deve seguir.
    public interface IPessoaService
    {
        // Retorna todas as pessoas cadastradas no sistema.
        Task<IEnumerable<Pessoa>> ListarTodasAsync();

        // Busca uma pessoa específica pelo Id.
        Task<Pessoa> ObterPorIdAsync(Guid id);

        // Cria um novo registro de pessoa
        // e retorna o objeto já salvo.
        Task<Pessoa> CriarAsync(Pessoa pessoa);

        // Atualiza os dados de uma pessoa existente
        // e retorna o objeto atualizado.
        Task<Pessoa> AtualizarAsync(Pessoa pessoa);

        // Remove uma pessoa pelo Id.
        // Retorna true/false indicando se a exclusão foi bem-sucedida.
        Task<bool> DeletarAsync(Guid id);

        // Retorna os totais financeiros agrupados por pessoa.
        Task<IEnumerable<TotaisPessoaModel>> ObterTotaisPorPessoaAsync();

        // Retorna um resumo geral consolidado do sistema,
        // como totais de receitas, despesas e saldo.
        Task<ResumoGeralModel> ObterResumoGeralAsync();
    }
}

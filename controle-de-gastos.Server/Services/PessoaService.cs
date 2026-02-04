using controle_de_gastos.Server.Entities;
using controle_de_gastos.Server.Interfaces;
using controle_de_gastos.Server.Models;
using controle_de_gastos.Server.Data;

namespace controle_de_gastos.Server.Services
{
    // Implementação do serviço de pessoas.
    // Centraliza as regras de negócio relacionadas às pessoas
    // e manipula os dados armazenados em memória no MockData.
    public class PessoaService : IPessoaService
    {
        // Retorna todas as pessoas cadastradas.
        public async Task<IEnumerable<Pessoa>> ListarTodasAsync()
        {
            return await Task.FromResult(MockData.Pessoas);
        }

        // Busca uma pessoa específica pelo Id.
        public async Task<Pessoa> ObterPorIdAsync(Guid id)
        {
            var pessoa = MockData.Pessoas.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(pessoa);
        }

        // Cria uma nova pessoa.
        // Um novo Id é gerado antes de adicionar na lista.
        public async Task<Pessoa> CriarAsync(Pessoa pessoa)
        {
            pessoa.Id = Guid.NewGuid();
            MockData.Pessoas.Add(pessoa);

            return await Task.FromResult(pessoa);
        }

        // Atualiza os dados de uma pessoa existente.
        // A busca é feita pelo Id e o registro é substituído.
        public async Task<Pessoa> AtualizarAsync(Pessoa pessoa)
        {
            var index = MockData.Pessoas.FindIndex(p => p.Id == pessoa.Id);

            if (index != -1)
            {
                MockData.Pessoas[index] = pessoa;
            }

            return await Task.FromResult(pessoa);
        }

        // Remove uma pessoa pelo Id.
        // Também remove todas as transações vinculadas a ela.
        public async Task<bool> DeletarAsync(Guid id)
        {
            var pessoa = MockData.Pessoas.FirstOrDefault(p => p.Id == id);

            if (pessoa != null)
            {
                // Regra de negócio: ao excluir a pessoa,
                // as transações dela também devem ser apagadas
                MockData.Transacoes.RemoveAll(t => t.PessoaId == id);

                MockData.Pessoas.Remove(pessoa);

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        // Calcula os totais financeiros de cada pessoa.
        public async Task<IEnumerable<TotaisPessoaModel>> ObterTotaisPorPessoaAsync()
        {
            var lista = MockData.Pessoas.Select(p => new TotaisPessoaModel
            {
                Nome = p.Nome,

                // Soma das receitas da pessoa
                TotalReceitas = MockData.Transacoes
                    .Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),

                // Soma das despesas da pessoa
                TotalDespesas = MockData.Transacoes
                    .Where(t => t.PessoaId == p.Id && t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            }).ToList();

            return await Task.FromResult(lista);
        }

        // Retorna um resumo geral do sistema,
        // consolidando receitas e despesas totais.
        public async Task<ResumoGeralModel> ObterResumoGeralAsync()
        {
            var resumo = new ResumoGeralModel
            {
                TotalGeralReceitas = MockData.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),

                TotalGeralDespesas = MockData.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
            };

            return await Task.FromResult(resumo);
        }
    }
}

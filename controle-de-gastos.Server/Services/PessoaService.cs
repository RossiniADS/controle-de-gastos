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
        public async Task<IEnumerable<PessoaResponse>> ListarTodasAsync()
        {
            var pessoas = MockData.Pessoas.Select(p => PessoaResponse.FromEntity(p));
            return await Task.FromResult(pessoas);
        }

        // Busca uma pessoa específica pelo Id.
        public async Task<PessoaResponse> ObterPorIdAsync(Guid id)
        {
            var pessoa = MockData.Pessoas.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(pessoa != null ? PessoaResponse.FromEntity(pessoa) : null);
        }

        // Cria uma nova pessoa.
        // Um novo Id é gerado antes de adicionar na lista.
        public async Task<PessoaResponse> CriarAsync(PessoaRequest request)
        {
            var pessoa = new Pessoa
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Idade = request.Idade
            };
            MockData.Pessoas.Add(pessoa);
            return await Task.FromResult(PessoaResponse.FromEntity(pessoa));
        }

        // Atualiza os dados de uma pessoa existente.
        // A busca é feita pelo Id e o registro é substituído.
        public async Task<PessoaResponse> AtualizarAsync(Guid id, PessoaRequest request)
        {
            var index = MockData.Pessoas.FindIndex(p => p.Id == id);
            if (index != -1)
            {
                var pessoa = MockData.Pessoas[index];
                pessoa.Nome = request.Nome;
                pessoa.Idade = request.Idade;
                return await Task.FromResult(PessoaResponse.FromEntity(pessoa));
            }
            return await Task.FromResult<PessoaResponse>(null);
        }

        // Remove uma pessoa pelo Id.
        // Também remove todas as transações vinculadas a ela.
        public async Task<bool> DeletarAsync(Guid id)
        {
            var pessoa = MockData.Pessoas.FirstOrDefault(p => p.Id == id);
            if (pessoa != null)
            {
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

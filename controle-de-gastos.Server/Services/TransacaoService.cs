using controle_de_gastos.Server.Entities;
using controle_de_gastos.Server.Interfaces;
using controle_de_gastos.Server.Data;

namespace controle_de_gastos.Server.Services
{
    // Implementação do serviço de transações.
    // Aqui ficam as validações e regras de negócio
    // antes de registrar qualquer movimentação financeira.
    public class TransacaoService : ITransacaoService
    {
        // Retorna todas as transações cadastradas.
        public async Task<IEnumerable<Transacao>> ListarTodasAsync()
        {
            return await Task.FromResult(MockData.Transacoes);
        }

        // Cria uma nova transação aplicando todas as regras do sistema.
        public async Task<Transacao> CriarAsync(Transacao transacao)
        {
            // 1. Valida se a pessoa informada existe
            var pessoa = MockData.Pessoas.FirstOrDefault(p => p.Id == transacao.PessoaId);
            if (pessoa == null)
                throw new Exception("Pessoa não encontrada.");

            // 2. Valida se a categoria informada existe
            var categoria = MockData.Categorias.FirstOrDefault(c => c.Id == transacao.CategoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            // 3. Regra de negócio:
            // menores de idade só podem registrar despesas
            if (pessoa.Idade < 18 && transacao.Tipo == TipoTransacao.Receita)
            {
                throw new Exception("Menores de idade só podem registrar despesas.");
            }

            // 4. Validação da finalidade da categoria
            // impede usar categoria de receita para despesa e vice-versa
            if (transacao.Tipo == TipoTransacao.Despesa && categoria.Finalidade == FinalidadeCategoria.Receita)
            {
                throw new Exception("Esta categoria é exclusiva para receitas.");
            }

            if (transacao.Tipo == TipoTransacao.Receita && categoria.Finalidade == FinalidadeCategoria.Despesa)
            {
                throw new Exception("Esta categoria é exclusiva para despesas.");
            }

            // Gera o Id da transação e adiciona na lista em memória
            transacao.Id = Guid.NewGuid();
            MockData.Transacoes.Add(transacao);

            return await Task.FromResult(transacao);
        }
    }
}

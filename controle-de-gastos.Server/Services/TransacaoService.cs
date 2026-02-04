using controle_de_gastos.Server.Entities;
using controle_de_gastos.Server.Interfaces;
using controle_de_gastos.Server.Models;
using controle_de_gastos.Server.Data;

namespace controle_de_gastos.Server.Services
{
    // Implementação do serviço de transações.
    // Aqui ficam as validações e regras de negócio
    // antes de registrar qualquer movimentação financeira.
    public class TransacaoService : ITransacaoService
    {

        // Retorna todas as transações cadastradas.
        public async Task<IEnumerable<TransacaoResponse>> ListarTodasAsync()
        {
            var transacoes = MockData.Transacoes.Select(t => {
                var resp = TransacaoResponse.FromEntity(t);
                resp.PessoaNome = MockData.Pessoas.FirstOrDefault(p => p.Id == t.PessoaId)?.Nome;
                resp.CategoriaDescricao = MockData.Categorias.FirstOrDefault(c => c.Id == t.CategoriaId)?.Descricao;
                return resp;
            });
            return await Task.FromResult(transacoes);
        }

        // Cria uma nova transação aplicando todas as regras do sistema.
        public async Task<TransacaoResponse> CriarAsync(TransacaoRequest request)
        {
            // 1. Valida se a pessoa informada existe
            var pessoa = MockData.Pessoas.FirstOrDefault(p => p.Id == request.PessoaId);
            if (pessoa == null)
                throw new Exception("Pessoa não encontrada.");

            // 2. Valida se a categoria informada existe
            var categoria = MockData.Categorias.FirstOrDefault(c => c.Id == request.CategoriaId);
            if (categoria == null)
                throw new Exception("Categoria não encontrada.");

            // 3. Regra de negócio:
            // menores de idade só podem registrar despesas
            if (pessoa.Idade < 18 && request.Tipo == TipoTransacao.Receita)
            {
                throw new Exception("Menores de idade só podem registrar despesas.");
            }

            // 4. Validação da finalidade da categoria
            // impede usar categoria de receita para despesa e vice-versa
            if (request.Tipo == TipoTransacao.Despesa && categoria.Finalidade == FinalidadeCategoria.Receita)
            {
                throw new Exception("Esta categoria é exclusiva para receitas.");
            }

            if (request.Tipo == TipoTransacao.Receita && categoria.Finalidade == FinalidadeCategoria.Despesa)
            {
                throw new Exception("Esta categoria é exclusiva para despesas.");
            }

            // Gera o Id da transação e adiciona na lista em memória
            var transacao = new Transacao
            {
                Id = Guid.NewGuid(),
                Descricao = request.Descricao,
                Valor = request.Valor,
                Tipo = request.Tipo,
                CategoriaId = request.CategoriaId,
                PessoaId = request.PessoaId
            };

            MockData.Transacoes.Add(transacao);
            
            var response = TransacaoResponse.FromEntity(transacao);
            response.PessoaNome = pessoa.Nome;
            response.CategoriaDescricao = categoria.Descricao;
            
            return await Task.FromResult(response);
        }
    }
}

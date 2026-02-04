using controle_de_gastos.Server.Entities;
using controle_de_gastos.Server.Interfaces;
using controle_de_gastos.Server.Models;
using controle_de_gastos.Server.Data;

namespace controle_de_gastos.Server.Services
{
    // Implementação do serviço de categorias.
    // Aqui ficam as regras de negócio relacionadas às categorias
    // e o acesso aos dados simulados em memória (MockData).
    public class CategoriaService : ICategoriaService
    {

        // Retorna todas as categorias cadastradas.
        // Como estamos usando dados em memória, apenas devolvemos a lista atual.
        public async Task<IEnumerable<CategoriaResponse>> ListarTodasAsync()
        {
            var categorias = MockData.Categorias.Select(c => CategoriaResponse.FromEntity(c));
            return await Task.FromResult(categorias);
        }

        // Cria uma nova categoria.
        // Um novo Id é gerado e o item é adicionado à lista em memória.
        public async Task<CategoriaResponse> CriarAsync(CategoriaRequest request)
        {
            var categoria = new Categoria
            {
                Id = Guid.NewGuid(),
                Descricao = request.Descricao,
                Finalidade = request.Finalidade
            };
            MockData.Categorias.Add(categoria);
            return await Task.FromResult(CategoriaResponse.FromEntity(categoria));
        }

        // Calcula os totais financeiros agrupados por categoria.
        // Também gera um resumo geral com os totais consolidados do sistema.
        public async Task<object> ObterTotaisPorCategoriaAsync()
        {
            var lista = MockData.Categorias.Select(c => new
            {
                Descricao = c.Descricao,

                // Soma das receitas da categoria

                TotalReceitas = MockData.Transacoes
                    .Where(t => t.CategoriaId == c.Id && t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),

                // Soma das despesas da categoria
                TotalDespesas = MockData.Transacoes
                    .Where(t => t.CategoriaId == c.Id && t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor),

                // Saldo final da categoria (receitas - despesas)
                Saldo =
                    MockData.Transacoes
                        .Where(t => t.CategoriaId == c.Id && t.Tipo == TipoTransacao.Receita)
                        .Sum(t => t.Valor)
                    -
                    MockData.Transacoes
                        .Where(t => t.CategoriaId == c.Id && t.Tipo == TipoTransacao.Despesa)
                        .Sum(t => t.Valor)
            }).ToList();

            // Resumo geral do sistema inteiro
            var resumoGeral = new
            {
                TotalGeralReceitas = MockData.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),
                
                TotalGeralDespesas = MockData.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor),

                // Saldo líquido geral
                SaldoLiquido =
                    MockData.Transacoes
                        .Where(t => t.Tipo == TipoTransacao.Receita)
                        .Sum(t => t.Valor)
                    -
                    MockData.Transacoes
                        .Where(t => t.Tipo == TipoTransacao.Despesa)
                        .Sum(t => t.Valor)
            };

            // Retorna lista detalhada + resumo consolidado
            return await Task.FromResult(new { Lista = lista, ResumoGeral = resumoGeral });
        }
    }
}

using controle_de_gastos.Server.Models;

namespace controle_de_gastos.Server.Interfaces
{
    // Interface que define o contrato do serviço de categorias.
    // Aqui ficam apenas as assinaturas dos métodos, ou seja,
    // o que o serviço deve fazer, sem a implementação.
    public interface ICategoriaService
    {
        // Retorna todas as categorias cadastradas.
        // Método assíncrono para não bloquear a execução enquanto busca os dados.
        Task<IEnumerable<CategoriaResponse>> ListarTodasAsync();

        // Cria uma nova categoria no sistema
        // e retorna a categoria já persistida.
        Task<CategoriaResponse> CriarAsync(CategoriaRequest request);

        // Calcula e retorna os totais agrupados por categoria.
        Task<object> ObterTotaisPorCategoriaAsync();
    }
}

using controle_de_gastos.Server.Entities;

namespace controle_de_gastos.Server.Models
{
    public class CategoriaResponse
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public FinalidadeCategoria Finalidade { get; set; }

        public static CategoriaResponse FromEntity(Categoria categoria)
        {
            return new CategoriaResponse
            {
                Id = categoria.Id,
                Descricao = categoria.Descricao,
                Finalidade = categoria.Finalidade
            };
        }
    }
}

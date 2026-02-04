using controle_de_gastos.Server.Entities;

namespace controle_de_gastos.Server.Models
{
    public class TransacaoResponse
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public Guid CategoriaId { get; set; }
        public string CategoriaDescricao { get; set; }
        public Guid PessoaId { get; set; }
        public string PessoaNome { get; set; }

        public static TransacaoResponse FromEntity(Transacao transacao)
        {
            return new TransacaoResponse
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Tipo = transacao.Tipo,
                CategoriaId = transacao.CategoriaId,
                CategoriaDescricao = transacao.Categoria?.Descricao, // Null-conditional operator
                PessoaId = transacao.PessoaId,
                PessoaNome = transacao.Pessoa?.Nome // Null-conditional operator
            };
        }
    }
}

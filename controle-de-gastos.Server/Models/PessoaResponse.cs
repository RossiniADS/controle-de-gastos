using controle_de_gastos.Server.Entities;

namespace controle_de_gastos.Server.Models
{
    public class PessoaResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public static PessoaResponse FromEntity(Pessoa pessoa)
        {
            return new PessoaResponse
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade
            };
        }
    }
}

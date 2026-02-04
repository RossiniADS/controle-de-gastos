using System.ComponentModel.DataAnnotations;

namespace controle_de_gastos.Server.Entities
{
    /// <summary>
    /// Entidade que representa uma pessoa no sistema.
    /// </summary>
    public class Pessoa
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required]
        public int Idade { get; set; }

        // Relacionamento: Uma pessoa pode ter várias transações
        public virtual ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}

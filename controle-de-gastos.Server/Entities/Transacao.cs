using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace controle_de_gastos.Server.Entities
{
    /// <summary>
    /// Define o tipo da transação.
    /// </summary>
    public enum TipoTransacao
    {
        Despesa,
        Receita
    }

    /// <summary>
    /// Entidade que representa uma transação financeira.
    /// </summary>
    public class Transacao
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public decimal Valor { get; set; }

        [Required]
        public TipoTransacao Tipo { get; set; }

        [Required]
        public Guid CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }

        [Required]
        public Guid PessoaId { get; set; }

        [ForeignKey("PessoaId")]
        public virtual Pessoa? Pessoa { get; set; }
    }
}

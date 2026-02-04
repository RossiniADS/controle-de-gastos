using System.ComponentModel.DataAnnotations;

namespace controle_de_gastos.Server.Entities
{
    /// <summary>
    /// Define a finalidade da categoria.
    /// </summary>
    public enum FinalidadeCategoria
    {
        Despesa,
        Receita,
        Ambas
    }

    /// <summary>
    /// Entidade que representa uma categoria de transação.
    /// </summary>
    public class Categoria
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(400)]
        public string Descricao { get; set; }

        [Required]
        public FinalidadeCategoria Finalidade { get; set; }

        public virtual ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
}

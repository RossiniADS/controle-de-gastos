using System.ComponentModel.DataAnnotations;
using controle_de_gastos.Server.Entities;

namespace controle_de_gastos.Server.Models
{
    public class TransacaoRequest
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O tipo de transação é obrigatório.")]
        public TipoTransacao Tipo { get; set; }

        [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
        public Guid PessoaId { get; set; }
    }
}

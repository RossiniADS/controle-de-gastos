using System.ComponentModel.DataAnnotations;
using controle_de_gastos.Server.Entities;

namespace controle_de_gastos.Server.Models
{
    public class CategoriaRequest
    {
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [MaxLength(400, ErrorMessage = "A descrição deve ter no máximo 400 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "A finalidade é obrigatória.")]
        public FinalidadeCategoria Finalidade { get; set; }
    }
}

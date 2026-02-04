using System.ComponentModel.DataAnnotations;

namespace controle_de_gastos.Server.Models
{
    public class PessoaRequest
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A idade é obrigatória.")]
        [Range(0, 150, ErrorMessage = "A idade deve ser entre 0 e 150.")]
        public int Idade { get; set; }
    }
}

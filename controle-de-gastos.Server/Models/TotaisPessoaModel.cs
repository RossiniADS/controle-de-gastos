namespace controle_de_gastos.Server.Models
{
    /// <summary>
    /// Modelo para exibição de totais por pessoa.
    /// </summary>
    public class TotaisPessoaModel
    {
        public string Nome { get; set; }
        public decimal TotalReceitas { get; set; }
        public decimal TotalDespesas { get; set; }
        public decimal Saldo => TotalReceitas - TotalDespesas;
    }

    /// <summary>
    /// Modelo para o resumo geral de todas as pessoas.
    /// </summary>
    public class ResumoGeralModel
    {
        public decimal TotalGeralReceitas { get; set; }
        public decimal TotalGeralDespesas { get; set; }
        public decimal SaldoLiquido => TotalGeralReceitas - TotalGeralDespesas;
    }
}

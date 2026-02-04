using controle_de_gastos.Server.Entities;

namespace controle_de_gastos.Server.Data
{
    // Classe estática usada para simular um banco de dados em memória.
    // A ideia aqui é facilitar testes e desenvolvimento sem precisar
    // de um banco real, já iniciando o sistema com alguns dados padrão.
    public static class MockData
    {
        // Lista de pessoas simulando uma tabela de pessoas
        public static List<Pessoa> Pessoas { get; set; } = [];

        // Lista de categorias simulando uma tabela de categorias
        public static List<Categoria> Categorias { get; set; } = [];

        // Lista de transações simulando uma tabela de movimentações financeiras
        public static List<Transacao> Transacoes { get; set; } = [];

        // Construtor estático executado automaticamente
        // na primeira vez que a classe é acessada
        static MockData()
        {
            // Sempre que o sistema inicia, os dados são recriados
            Reset();
        }

        // Método responsável por reinicializar os dados em memória.
        public static void Reset()
        {
            // Criação das pessoas iniciais
            Pessoas =
            [
                new() {
                    Id = Guid.Parse("d1b1b1b1-b1b1-b1b1-b1b1-b1b1b1b1b1b1"),
                    Nome = "João Silva",
                    Idade = 35
                },
                new Pessoa
                {
                    Id = Guid.Parse("d2b2b2b2-b2b2-b2b2-b2b2-b2b2b2b2b2b2"),
                    Nome = "Maria Oliveira",
                    Idade = 17
                }
            ];

            // Criação das categorias padrão do sistema
            Categorias =
            [
                new Categoria
                {
                    Id = Guid.Parse("c1c1c1c1-c1c1-c1c1-c1c1-c1c1c1c1c1c1"),
                    Descricao = "Alimentação",
                    Finalidade = FinalidadeCategoria.Despesa
                },
                new Categoria
                {
                    Id = Guid.Parse("c2c2c2c2-c2c2-c2c2-c2c2-c2c2c2c2c2c2"),
                    Descricao = "Salário",
                    Finalidade = FinalidadeCategoria.Receita
                },
                new Categoria
                {
                    Id = Guid.Parse("c3c3c3c3-c3c3-c3c3-c3c3-c3c3c3c3c3c3"),
                    Descricao = "Outros",
                    Finalidade = FinalidadeCategoria.Ambas
                }
            ];

            // Criação de algumas transações de exemplo
            // usando as pessoas e categorias já criadas
            Transacoes =
            [
                new Transacao
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Supermercado",
                    Valor = 250.50m,
                    Tipo = TipoTransacao.Despesa,
                    PessoaId = Pessoas[0].Id,
                    CategoriaId = Categorias[0].Id
                },
                new Transacao
                {
                    Id = Guid.NewGuid(),
                    Descricao = "Mesada",
                    Valor = 100.00m,
                    Tipo = TipoTransacao.Despesa,
                    PessoaId = Pessoas[1].Id,
                    CategoriaId = Categorias[2].Id
                }
            ];
        }
    }
}

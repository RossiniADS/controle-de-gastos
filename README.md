# Controle de Gastos

## Descrição do Projeto

O projeto "Controle de Gastos" é uma aplicação full-stack desenvolvida para auxiliar no gerenciamento de finanças pessoais. Ele permite o cadastro de pessoas, categorias de despesas/receitas e o registro de transações financeiras, aplicando regras de negócio específicas para garantir a integridade dos dados.

## Funcionalidades

-   **Gestão de Pessoas**: Cadastro, listagem, edição e exclusão de usuários.
-   **Gestão de Categorias**: Cadastro, listagem e edição de categorias financeiras (despesa ou receita).
-   **Gestão de Transações**: Registro de transações (despesas e receitas) associadas a pessoas e categorias.
-   **Regras de Negócio**: Validações específicas para transações, como restrições para menores de idade e compatibilidade de categorias.
-   **Consultas Consolidadas**: Obtenção de totais de receitas, despesas e saldo por pessoa e totais por categoria.

## Tecnologias Utilizadas

### Frontend

O frontend da aplicação é construído com as seguintes tecnologias:

-   **React**: Biblioteca JavaScript para construção de interfaces de usuário.
-   **TypeScript**: Superset do JavaScript que adiciona tipagem estática.
-   **Vite**: Ferramenta de build rápido para projetos web modernos.
-   **Axios**: Cliente HTTP baseado em Promises para fazer requisições a APIs.
-   **React Router DOM**: Biblioteca para roteamento declarativo no React.

### Backend

O backend da aplicação é desenvolvido em .NET e utiliza:

-   **.NET 8.0 (ASP.NET Core)**: Framework para construção de aplicações web e APIs.
-   **C#**: Linguagem de programação.
-   **Arquitetura de Serviços**: Organização do código em controladores, serviços e entidades para modularidade e separação de responsabilidades.
-   **Dados em Memória (MockData)**: Para fins de demonstração, os dados são persistidos em memória, simulando um banco de dados.

## Estrutura do Projeto

O projeto é dividido em duas partes principais: `controle-de-gastos.client` (frontend) e `controle-de-gastos.Server` (backend).

```
controle-de-gastos/
├── controle-de-gastos.client/          # Aplicação frontend (React)
│   ├── public/
│   ├── src/
│   │   ├── assets/
│   │   ├── components/                 # Componentes reutilizáveis (ex: Navbar)
│   │   ├── pages/                      # Páginas da aplicação (ex: Pessoas, Categorias)
│   │   ├── services/                   # Serviços de comunicação com a API (ex: api.js)
│   │   ├── App.tsx                     # Componente raiz e configuração de rotas
│   │   └── main.tsx                    # Ponto de entrada da aplicação
│   ├── package.json                    # Dependências e scripts do frontend
│   └── vite.config.ts                  # Configuração do Vite
└── controle-de-gastos.Server/          # Aplicação backend (ASP.NET Core)
    ├── Controllers/                    # Controladores da API (ex: PessoasController)
    ├── Data/                           # Dados mockados (ex: MockData.cs)
    ├── Entities/                       # Modelos de entidades (ex: Pessoa, Categoria, Transacao)
    ├── Interfaces/                     # Interfaces para os serviços
    ├── Services/                       # Implementação dos serviços (ex: PessoaService)
    ├── appsettings.json                # Configurações da aplicação
    ├── Program.cs                      # Configuração do servidor e injeção de dependências
    └── controle-de-gastos.Server.csproj # Arquivo de projeto .NET
```

## Como Rodar o Projeto

### Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas em sua máquina:

-   [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
-   [Node.js e npm](https://nodejs.org/)

### Backend

1.  Navegue até o diretório do servidor:
    ```bash
    cd controle-de-gastos/controle-de-gastos.Server
    ```
2.  Restaure as dependências e compile o projeto:
    ```bash
    dotnet restore
    dotnet build
    ```
3.  Execute a aplicação backend:
    ```bash
    dotnet run
    ```
    O servidor estará disponível em `https://localhost:7167` (ou porta similar).

### Frontend

1.  Navegue até o diretório do cliente:
    ```bash
    cd controle-de-gastos/controle-de-gastos.client
    ```
2.  Instale as dependências:
    ```bash
    npm install
    ```
3.  Inicie a aplicação frontend:
    ```bash
    npm run dev
    ```
    A aplicação estará disponível em `http://localhost:5173` (ou porta similar).

## API Endpoints

A API backend expõe os seguintes endpoints:

### Pessoas

-   `GET /api/pessoas`: Lista todas as pessoas.
-   `GET /api/pessoas/totais`: Retorna totais consolidados de receitas, despesas e saldo por pessoa.
-   `POST /api/pessoas`: Cria uma nova pessoa.
-   `PUT /api/pessoas/{id}`: Atualiza uma pessoa existente.
-   `DELETE /api/pessoas/{id}`: Remove uma pessoa e suas transações associadas.

### Categorias

-   `GET /api/categorias`: Lista todas as categorias.
-   `GET /api/categorias/totais`: Retorna totais consolidados agrupados por categoria.
-   `POST /api/categorias`: Cria uma nova categoria.

### Transações

-   `GET /api/transacoes`: Lista todas as transações.
-   `POST /api/transacoes`: Cria uma nova transação.

## Regras de Negócio

As seguintes regras de negócio são aplicadas no serviço de transações (`TransacaoService`):

-   **Validação de Pessoa e Categoria**: Uma transação só pode ser criada se a pessoa e a categoria associadas existirem.
-   **Restrição para Menores de Idade**: Pessoas com menos de 18 anos só podem registrar transações do tipo `Despesa`.
-   **Compatibilidade de Categoria**: Não é permitido usar uma categoria com `Finalidade.Receita` para uma transação do tipo `Despesa`, e vice-versa.

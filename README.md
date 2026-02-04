# Controle de Gastos

## Visão Geral

O **Controle de Gastos** é uma aplicação full-stack desenvolvida para o gerenciamento de finanças pessoais, permitindo o controle de pessoas, categorias financeiras e transações de receitas e despesas.  
O projeto foi estruturado desde o início com foco em boas práticas de arquitetura, separação de responsabilidades e validações de regras de negócio, garantindo um código organizado, seguro e fácil de evoluir.

---

## Funcionalidades

- **Gestão de Pessoas**
  - Cadastro, listagem, edição e exclusão
  - Cálculo de totais consolidados (receitas, despesas e saldo)

- **Gestão de Categorias**
  - Cadastro e listagem de categorias financeiras
  - Classificação por finalidade (Receita ou Despesa)
  - Totais consolidados por categoria

- **Gestão de Transações**
  - Registro de receitas e despesas
  - Associação com pessoa e categoria
  - Validações automáticas de regras de negócio

- **Regras de Negócio**
  - Controle de transações para menores de idade
  - Compatibilidade entre tipo da transação e finalidade da categoria
  - Garantia de integridade entre entidades relacionadas


## Tecnologias Utilizadas

### Frontend

- React
- TypeScript
- Vite
- Axios
- React Router DOM


### Backend

- .NET 8 (ASP.NET Core)
- C#
- Arquitetura em Camadas
  - Controllers
  - Services
  - Interfaces
  - Entities
  - Models (DTOs)
- Dados em Memória (MockData)

A API utiliza **Models (DTOs)** para entrada e saída de dados, evitando o acoplamento direto com as entidades de domínio e garantindo maior segurança e flexibilidade.


## Arquitetura e Padrões

- DTOs (Data Transfer Objects) para Requests e Responses
- Injeção de Dependência
- Separação clara entre domínio e camada de apresentação
- Validações centralizadas
- Código preparado para futura persistência em banco de dados

### Models Utilizados

- PessoaRequest / PessoaResponse
- CategoriaRequest / CategoriaResponse
- TransacaoRequest / TransacaoResponse


## Estrutura do Projeto

```

controle-de-gastos/
├── controle-de-gastos.client/          # Frontend (React)
│   ├── public/
│   ├── src/
│   │   ├── assets/
│   │   ├── components/
│   │   ├── pages/
│   │   ├── services/
│   │   ├── App.tsx
│   │   └── main.tsx
│   ├── package.json
│   └── vite.config.ts
│
└── controle-de-gastos.Server/          # Backend (ASP.NET Core)
├── Controllers/
├── Data/
├── Entities/
├── Interfaces/
├── Models/
├── Services/
├── Program.cs
└── controle-de-gastos.Server.csproj

````


## Como Executar o Projeto

### Pré-requisitos

- .NET SDK 8.0
- Node.js e npm


### Backend

```bash
cd controle-de-gastos/controle-de-gastos.Server
dotnet restore
dotnet build
dotnet run
````

### Frontend

```bash
cd controle-de-gastos/controle-de-gastos.client
npm install
npm run dev
```

## Endpoints da API

### Pessoas

* GET /api/pessoas
* GET /api/pessoas/totais
* POST /api/pessoas
* PUT /api/pessoas/{id}
* DELETE /api/pessoas/{id}


### Categorias

* GET /api/categorias
* GET /api/categorias/totais
* POST /api/categorias


### Transações

* GET /api/transacoes
* POST /api/transacoes


## Regras de Negócio

* Uma transação só pode ser criada se Pessoa e Categoria existirem
* Pessoas menores de 18 anos só podem registrar transações do tipo Despesa
* Categorias de Receita não podem ser usadas em Despesas e vice-versa

As regras são aplicadas na camada de Services, garantindo consistência independente da origem da requisição.

---

## Autor

Rossini Alves

# 🧩 Item Detail API

API backend construída para fornecer os dados necessários de uma página de detalhes de produto, inspirada no Mercado Livre.

Este projeto foi desenvolvido com foco em boas práticas de design backend, Clean Architecture, tratamento de erros, testes automatizados e documentação OpenAPI 3.0.

# 🎯 Objetivo

Criar uma API backend que entregue todas as informações necessárias para o front-end exibir os detalhes de um produto.
O endpoint principal deve retornar informações completas do item — título, preço, estoque, atributos e imagens — de forma eficiente e bem estruturada.

# 🧰 Stack e Tecnologias

| Camada        | Tecnologia                          |
| ------------- | ----------------------------------- |
| Linguagem     | C# 13 / .NET 9                      |
| Arquitetura   | Clean Architecture                  |
| Framework Web | ASP.NET Core Minimal API            |
| Persistência  | Arquivo local JSON                  |
| Testes        | xUnit + FluentAssertions + Moq      |
| Documentação  | Swagger / OpenAPI 3.0               |

# 📡 Endpoints Principais

| Método | Rota                       | Descrição                                      |
| ------ | -------------------------- | ---------------------------------------------- |
| `GET`  | `/api/items/{id}`          | Retorna os detalhes completos de um produto    |
| `GET`  | `/health`                  | Verifica o status da API                       |
| `GET`  | `/openapi/v1/openapi.yaml` | Retorna o arquivo OpenAPI manual (YAML)        |
| `GET`  | `/docs`                    | Interface Swagger UI com as duas documentações |


# 🧪 Testes

O projeto contém:

Testes Unitários → validam comportamentos isolados (handlers e exceptions).

Testes de Integração → validam integração real entre handler, repositório e dataset JSON.


# 🧬 Estratégia Técnica

Clean Architecture: separação total entre domínio, aplicação, infraestrutura e interface.

Injeção de Dependência: repositórios e casos de uso são resolvidos via DI Container.

Tratamento Centralizado de Erros: middleware captura exceções como NotFoundException e responde com JSON padronizado.

Testes: O xUnit foi escolhido por ser o framework de testes unitários padrão do ecossistema .NET moderno + FluentAssertions que complementa o xUnit tornar as validações mais legíveis e expressivas, deixando os testes claros e fáceis de manter.

Documentação OpenAPI: duas fontes — Swagger gerado automaticamente e um arquivo YAML manual servindo em /openapi/v1/openapi.yaml.

GenAI e ferramentas modernas: utilizados para acelerar boilerplate, gerar DTOs, testes e refinar arquitetura, conforme boas práticas do desafio.

# Estrutura principal do projeto:
└── 📁 ItemDetailApi
    ├── 📁 src
    │   ├── 📁 ItemDetail.Api
    │   │   ├── 📁 Endpoints          # Rotas HTTP (Health, Items)
    │   │   ├── 📁 Middleware         # Tratamento global de exceções
    │   │   ├── 📁 Data               # Base local de produtos (products.json)
    │   │   └── 📁 openapi            # Definição OpenAPI 3.0 (Swagger + YAML)
    │   │
    │   ├── 📁 ItemDetail.Application
    │   │   ├── 📁 UseCases           # Casos de uso (ex: GetProductById)
    │   │   ├── 📁 DTOs               # Objetos de transferência (ProductDto)
    │   │   └── 📁 Common             # Exceções e utilitários de aplicação
    │   │
    │   ├── 📁 ItemDetail.Domain
    │   │   ├── 📁 Entities           # Entidades de domínio (Product)
    │   │   └── 📁 Abstractions       # Contratos e interfaces (IProductRepository)
    │   │
    │   └── 📁 ItemDetail.Infrastructure
    │       └── 📁 Repositories       # Implementações concretas (JsonProductRepository)
    │
    ├── 📁 tests
    │   └── 📁 ItemDetail.Tests
    │       ├── 📄 GetProductByIdUnitTests.cs     # Testes unitários
    │       └── 📄 ItemDetailIntegrationTests.cs  # Testes de integração
    │
    └── 📝 README.md                 # Documentação principal do projeto
    ├── 🏃‍♂️ run.md                    # Guia de execução rápida (build, run, test)
    └── 💬 prompts.md                # Histórico técnico dos prompts usados no desenvolvimento

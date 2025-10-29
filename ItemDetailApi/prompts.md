1. Escolha da Arquitetura

Prompt:
“Estou desenvolvendo uma API para detalhar produtos, estruturada em múltiplas camadas (Domínio, Aplicação, Infraestrutura e API).
Poderia me explicar as diferenças práticas entre as arquiteturas Clean, Hexagonal e Onion, e indicar qual delas seria a mais adequada para esse projeto, considerando escalabilidade, testabilidade e separação de responsabilidades?”

2. Estrutura inicial do projeto

Prompt:

“Quero criar uma API com Clean Architecture em .NET 9, para exibir detalhes de produtos no estilo Mercado Livre.
Monte a estrutura de solution e projetos (Domain, Application, Infrastructure e Api).”

3. Implementação do Handler e Repositório

Prompt:

“Crie o caso de uso GetProductById seguindo os princípios da Clean Architecture.”
“Monte o repositório JsonProductRepository para buscar os produtos de um arquivo local products.json.”

4. Middleware de tratamento de erros

Prompt:

“Chat, o middleware não seria melhor tirar do Program.cs e individualizar em uma classe?”

5. Documentação Swagger / OpenAPI

Prompt:
“Quero implementar uma documentação completa da API utilizando o Swagger com OpenAPI 3.0, de forma organizada e visualmente agradável.
Preciso que a interface esteja acessível em /docs e que o arquivo YAML gerado manualmente também seja servido pela aplicação em uma rota dedicada (/openapi/v1/openapi.yaml).
Poderia me orientar sobre a melhor forma de estruturar isso dentro do projeto, seguindo boas práticas de versionamento e clareza de documentação?”

6. Testes Automatizados

Prompt:
“Desejo implementar testes automatizados para validar o caso de uso GetProductByIdHandler.
Poderia criar testes unitários utilizando xUnit, FluentAssertions e Moq para garantir o comportamento esperado do handler?
Em seguida, gostaria também de um teste de integração que execute o fluxo completo sem mocks, utilizando o dataset real (products.json), a fim de validar a integração entre o handler, o repositório e os dados reais da aplicação.”
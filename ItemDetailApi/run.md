💾 Execução Local - 

1️⃣ Na raiz do repositório (ItemDetailApi/) restaurar pacotes

dotnet restore

2️⃣ Executar a API

dotnet build

dotnet run --project src/ItemDetail.Api

3️⃣ Acessar a documentação

Swagger UI → http://localhost:5000/docs

OpenAPI (YAML) → http://localhost:5000/openapi/v1/openapi.yaml

🧪 Testes

O projeto possui testes unitários e de integração.

▶️ Executar
dotnet test
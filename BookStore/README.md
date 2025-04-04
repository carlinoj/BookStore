# 📚 BookStore API

API RESTful desenvolvida em .NET 8 para gerenciar uma livraria com cadastro de usuários, autores, livros, autenticação JWT, compra de livros, busca e filtros. Projeto estruturado com boas práticas, SOLID, design patterns e testes unitários.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 8 (Web API)
- Entity Framework Core
- SQL Server
- JWT (Json Web Token)
- Swagger (Swashbuckle)
- xUnit + Moq (testes unitários)

---

## 📦 Estrutura da Solução

```
BookStore.sln
│
├── BookStore.Api              # Projeto principal (controllers, middlewares)
├── BookStore.Application      # Camada de aplicação (DTOs, serviços)
├── BookStore.Domain           # Entidades e interfaces de domínio
├── BookStore.Infrastructure   # Implementação do EF Core (DbContext, Repositórios)
└── BookStore.Tests            # Projeto de testes unitários com xUnit/Moq
```

---

## 📌 Funcionalidades

### 👥 Usuários

- Registro de usuários
- Login com retorno de JWT
- Controle de acesso por papel (Admin, comum)

### 📚 Livros

- CRUD de livros (somente Admin)
- Busca por título
- Filtro por categoria
- Compra com controle de estoque

### 🧑 Autores

- CRUD de autores (somente Admin)

---

## 🔐 Autenticação

- Autenticação baseada em JWT
- Endpoints protegidos com `[Authorize]`
- Papel de Admin requerido para operações críticas

---

## 📑 Documentação Swagger

Após rodar a API, acesse:

```
https://localhost:{porta}/swagger
```

Clique no botão **Authorize**, insira o token:

```
Bearer {seu_token_jwt}
```

---

## 🧪 Rodando os Testes

Na raiz do projeto de testes:

```bash
dotnet test
```

### ✅ Geração de relatório de cobertura

```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
reportgenerator -reports:coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html
```

Abra `coverage-report/index.html` para visualizar.

---

## 🧱 Como Executar o Projeto Localmente

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/bookstore-api.git
cd bookstore-api
```

2. Ajuste a `DefaultConnection` no `appsettings.json` do projeto `BookStore.Api`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=BookStoreDb;Trusted_Connection=True;"
}
```

3. Execute as migrations:

```bash
dotnet ef database update --project BookStore.Infrastructure --startup-project BookStore.Api
```

4. Rode a API:

```bash
dotnet run --project BookStore.Api
```

---

## ✅ Implementado

- [x] CRUD completo de livros e autores
- [x] Cadastro e login de usuários
- [x] Autenticação e autorização com JWT
- [x] Compra de livros com validação de estoque
- [x] Middleware de tratamento de erros
- [x] Swagger com suporte a Bearer Token
- [x] Testes unitários com xUnit e Moq
- [x] Repositório genérico + injeção de dependência

---

## 🌟 Diferenciais Possíveis (não incluídos)

- CQRS com MediatR
- AutoMapper
- Deploy com Docker
- Logs estruturados com Serilog

---

## 🧑‍💻 Autor

Desenvolvido por [Seu Nome].

---

## 📄 Licença

Este projeto está sob a licença MIT.

---

## 🔎 Observações

- Eu sempre utilizo ferramentas para formatação do README.md por questões estéticas e práticas.
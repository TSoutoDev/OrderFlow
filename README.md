# 🚀 OrderFlow

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-13-239120?style=for-the-badge&logo=csharp)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Entity Framework](https://img.shields.io/badge/EF%20Core-512BD4?style=for-the-badge)
![MediatR](https://img.shields.io/badge/MediatR-CQRS-blue?style=for-the-badge)
![FluentValidation](https://img.shields.io/badge/FluentValidation-Validation-success?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

Sistema de processamento de pedidos desenvolvido em **.NET 10**, aplicando conceitos modernos de arquitetura como **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS**, **SOLID** e **Entity Framework Core**.

Sistema de processamento de pedidos desenvolvido com .NET 10, Domain-Driven Design (DDD) e Clean Architecture.
\#🚧 Status: Em desenvolvimento (Sprint 2 concluída)
=======
Este projeto faz parte do meu portfólio e está sendo desenvolvido de forma incremental, simulando um sistema utilizado em ambiente corporativo.

---

# 📌 Status

> 🚧 Em desenvolvimento

### Sprint concluídas

- ✅ Sprint 1 — Estrutura da solução
- ✅ Sprint 2 — Camada de Domínio
- ✅ Sprint 3 — Casos de Uso + Persistência + API REST

---

# 🎯 Objetivo

O objetivo do OrderFlow é demonstrar a construção de uma API moderna utilizando boas práticas de arquitetura e desenvolvimento de software.

Durante sua evolução serão aplicados conceitos como:

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- SOLID
- Entity Framework Core
- SQL Server
- Testes Unitários
- Mensageria
- Worker Service
- Docker
- Azure

---
	
O OrderFlow tem como objetivo demonstrar a construção de uma aplicação real utilizando conceitos como:



\- Domain-Driven Design (DDD)

\- Clean Architecture

\- Testes Unitários

\- Processamento Assíncrono

\- Mensageria

\- Arquitetura em Camadas



\---



\## 🛠️ Tecnologias



\- .NET 10

\- C#

\- xUnit

\- Entity Framework Core \*(em desenvolvimento)\*

\- PostgreSQL \*(em desenvolvimento)\*

\- RabbitMQ \*(em desenvolvimento)\*

\- MongoDB \*(em desenvolvimento)\*

\- SignalR \*(em desenvolvimento)\*

\- Docker \*(em desenvolvimento)\*



\---



\## 📂 Estrutura da Solução

```text
src/
├── OrderFlow.Api
├── OrderFlow.Application
├── OrderFlow.Contracts
├── OrderFlow.Domain
├── OrderFlow.Infrastructure
├── OrderFlow.Web
└── OrderFlow.Worker

tests/
├── OrderFlow.UnitTests
└── OrderFlow.IntegrationTests
=======
# 🏗 Arquitetura

```
                    ASP.NET Core API
                           │
                    Controllers
                           │
                      MediatR (CQRS)
                           │
          Commands                 Queries
                │                    │
                └──────────┬─────────┘
                           │
                    Application Layer
                           │
                       Domain Layer
                           │
                  Infrastructure Layer
                           │
                 Entity Framework Core
                           │
                      SQL Server
```

---

\## 🗺️ Roadmap
=======
# 📂 Estrutura do Projeto

```
src
│
├── OrderFlow.Api
├── OrderFlow.Application
├── OrderFlow.Domain
└── OrderFlow.Infrastructure

tests
│
└── OrderFlow.UnitTests
```

---

# 🔄 Fluxo de uma requisição

```
HTTP Request
      │
      ▼
Controller
      │
      ▼
ValidationBehavior
      │
      ▼
Command / Query
      │
      ▼
Handler
      │
      ▼
Repository
      │
      ▼
Entity Framework Core
      │
      ▼
SQL Server
```

---

# ✨ Funcionalidades Implementadas

## Pedidos

- ✅ Criar pedido
- ✅ Consultar pedido por Id
- ✅ Persistência em SQL Server

## Validação

- ✅ FluentValidation
- ✅ Pipeline Behavior (MediatR)

## API

- ✅ Swagger
- ✅ Controller REST
- ✅ Dependency Injection

## Infraestrutura

- ✅ Entity Framework Core
- ✅ SQL Server
- ✅ Repository Pattern

## Tratamento de Erros

- ✅ Middleware Global
- ✅ Validação padronizada
- ✅ Respostas HTTP padronizadas

---

# 📡 Endpoints

## Criar Pedido

```
POST /api/orders
```

### Exemplo

```json
{
  "orderNumber": "ORD-0001",
  "customerId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
  "items": [
    {
      "productId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
      "productName": "Notebook Dell",
      "quantity": 2,
      "unitPrice": 5500,
      "currency": "BRL"
    }
  ]
}
```

---

## Consultar Pedido

```
GET /api/orders/{id}
```

---

# 🛠 Tecnologias

- .NET 10
- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- Swagger
- xUnit

---

# 📚 Conceitos Aplicados

| Conceito | Implementação |
|----------|---------------|
| Clean Architecture | ✅ |
| Domain-Driven Design | ✅ |
| CQRS | ✅ |
| SOLID | ✅ |
| Repository Pattern | ✅ |
| Dependency Injection | ✅ |
| Aggregate Root | ✅ |
| Entities | ✅ |
| Value Objects | ✅ |
| Middleware | ✅ |
| FluentValidation | ✅ |
| Entity Framework Core | ✅ |

---

# 🚀 Como executar

## Clonar o projeto

```bash
git clone https://github.com/TSoutoDev/OrderFlow.git
```

## Entrar na pasta

```bash
cd OrderFlow
```

## Restaurar os pacotes

```bash
dotnet restore
```

## Executar as migrations

\- 💼 LinkedIn: https://linkedin.com/in/t-souto

\- 💻 GitHub: https://github.com/TSoutoDev
=======
```bash
dotnet ef database update
```

## Executar a aplicação

```bash
dotnet run --project src/OrderFlow.Api
```

## Abrir o Swagger

```
https://localhost:xxxx/swagger
```

---

# 🗺 Roadmap

## Sprint 1

- ✅ Estrutura da solução

## Sprint 2

- ✅ Entidades
- ✅ Value Objects
- ✅ Aggregate Root
- ✅ Regras de negócio

## Sprint 3

- ✅ CQRS
- ✅ MediatR
- ✅ FluentValidation
- ✅ Entity Framework Core
- ✅ SQL Server
- ✅ Swagger
- ✅ Middleware Global
- ✅ API REST

## Sprint 4

- ⏳ Atualização de pedidos
- ⏳ Cancelamento de pedidos

## Sprint 5

- ⏳ RabbitMQ

## Sprint 6

- ⏳ Worker Service

## Sprint 7

- ⏳ MongoDB

## Sprint 8

- ⏳ SignalR

## Sprint 9

- ⏳ Docker

## Sprint 10

- ⏳ Deploy Azure

---

# 🎯 Próximos Passos

- Atualizar pedido
- Cancelar pedido
- Publicação de eventos
- Processamento assíncrono
- Cache
- Observabilidade
- Docker
- Deploy na Azure

---

# 👨‍💻 Autor

## Tiago Souto

Software Developer | .NET | C# | ASP.NET Core | SQL Server | Azure | Clean Architecture | DDD | CQRS

🔗 LinkedIn

https://linkedin.com/in/t-souto

💻 GitHub

https://github.com/TSoutoDev

---

⭐ Se este projeto foi útil ou interessante para você, deixe uma estrela no repositório.
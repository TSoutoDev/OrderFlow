# 🚀 OrderFlow

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-13-239120?style=for-the-badge&logo=csharp)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge)
![MediatR](https://img.shields.io/badge/MediatR-CQRS-blue?style=for-the-badge)
![FluentValidation](https://img.shields.io/badge/FluentValidation-Validation-success?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

Sistema de processamento de pedidos desenvolvido em **.NET 10**, utilizando **Clean Architecture**, **Domain-Driven Design (DDD)**, **CQRS**, **MediatR**, **Entity Framework Core** e **SQL Server**.

Este projeto faz parte do meu portfólio e está sendo desenvolvido de forma incremental, simulando a evolução de uma aplicação utilizada em ambiente corporativo.

---

# 📌 Status

> 🚧 Em desenvolvimento

### Sprint concluídas

- ✅ Sprint 1 — Fundação da solução
- ✅ Sprint 2 — Camada de Domínio
- ✅ Sprint 3 — Casos de Uso + Persistência + API REST
- ✅ Sprint 4 — Atualização e Cancelamento de Pedidos

---

# 🎯 Objetivo

O objetivo do OrderFlow é demonstrar a construção de uma API moderna utilizando boas práticas de arquitetura e desenvolvimento de software.

Durante sua evolução estão sendo aplicados conceitos como:

- Clean Architecture
- Domain-Driven Design (DDD)
- CQRS
- SOLID
- Repository Pattern
- MediatR
- FluentValidation
- Entity Framework Core
- SQL Server
- Testes Unitários
- Mensageria
- Worker Service
- Docker

---

# 🏗 Arquitetura

```text
                    ASP.NET Core API
                           │
                     Controllers
                           │
                     MediatR (CQRS)
                           │
        Commands                      Queries
             │                           │
             └──────────────┬────────────┘
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

# 📂 Estrutura da Solução

```text
src
│
├── OrderFlow.Api
├── OrderFlow.Application
├── OrderFlow.Contracts
├── OrderFlow.Domain
├── OrderFlow.Infrastructure
├── OrderFlow.Web
└── OrderFlow.Worker

tests
│
├── OrderFlow.UnitTests
└── OrderFlow.IntegrationTests
```

---

# 🔄 Fluxo de uma Requisição

```text
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
- ✅ Atualizar pedido
- ✅ Cancelar pedido

## Domínio

- ✅ Aggregate Root
- ✅ Entities
- ✅ Value Objects
- ✅ Regras de negócio
- ✅ Máquina de estados

## Application

- ✅ CQRS
- ✅ MediatR
- ✅ Commands
- ✅ Queries
- ✅ Handlers
- ✅ FluentValidation
- ✅ Pipeline Behavior

## Infraestrutura

- ✅ Entity Framework Core
- ✅ SQL Server
- ✅ Repository Pattern

## API

- ✅ ASP.NET Core Web API
- ✅ Swagger
- ✅ Controllers REST

## Tratamento de Exceções

- ✅ ValidationException
- ✅ DomainException
- ✅ KeyNotFoundException
- ✅ Middleware Global

---

# 📡 Endpoints

| Método | Endpoint | Descrição |
|---------|----------|-----------|
| POST | `/api/orders` | Criar pedido |
| GET | `/api/orders/{id}` | Consultar pedido |
| PUT | `/api/orders/{id}` | Atualizar pedido |
| PATCH | `/api/orders/{id}/cancel` | Cancelar pedido |

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

| Conceito | Status |
|----------|:------:|
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

# 🚀 Como Executar

## Clonar o repositório

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

- ✅ Aggregate Root
- ✅ Entities
- ✅ Value Objects
- ✅ Regras de negócio

## Sprint 3

- ✅ CQRS
- ✅ MediatR
- ✅ FluentValidation
- ✅ Entity Framework Core
- ✅ SQL Server
- ✅ Middleware Global
- ✅ API REST

## Sprint 4

- ✅ Atualização de pedidos
- ✅ Cancelamento de pedidos
- ✅ Tratamento de DomainException
- ✅ Tratamento de KeyNotFoundException

## Sprint 5

- ⏳ RabbitMQ

## Sprint 6

- ⏳ Worker Service

## Sprint 7

- ⏳ SignalR

## Sprint 8

- ⏳ Docker

## Sprint 9

- ⏳ Deploy

---

# 🎯 Próximos Passos

- Publicação de eventos com RabbitMQ
- Processamento assíncrono com Worker Service
- Comunicação em tempo real com SignalR
- Containerização com Docker
- Deploy

---

# 👨‍💻 Autor

## Tiago Souto

Software Developer | .NET | C# | ASP.NET Core | SQL Server | Clean Architecture | DDD | CQRS

🔗 **LinkedIn**

https://linkedin.com/in/t-souto

💻 **GitHub**

https://github.com/TSoutoDev

---

⭐ Se este projeto foi útil ou interessante para você, deixe uma estrela no repositório.
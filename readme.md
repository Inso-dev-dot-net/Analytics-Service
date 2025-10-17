# 📊 Analytics Service

## 1. Project and Purpose

**Analytics Service** — learning/demo project showcasing .NET, Docker, Kafka (Redpanda), PostgreSQL, and Grafana.

The goal is to demonstrate skills in building event-driven analytics pipelines:
- HTTP API for collecting events,
- message broker (Kafka/Redpanda) for streaming,
- PostgreSQL for raw storage and aggregation,
- simple dashboards with Razor Views and Grafana.

---

## 2. О проекте

Учебный проект, демонстрирующий навыки работы с современным стеком технологий для построения систем сбора и аналитики событий.  

Используются: **.NET (Web API, Worker Services, MVC Razor)**, **Docker**, **Kafka (Redpanda)**, **PostgreSQL**, **Grafana**.  

Проект показывает умение проектировать архитектуру, работать с брокерами сообщений и реляционными СУБД, а также настраивать базовую инфраструктуру для наблюдаемости и визуализации данных.

---

## 3. Stack and Versions

- **.NET 8**  
  - `ASP.NET Core Web API` (controller-based)  
  - `Worker Services` (background processing)  
  - `ASP.NET Core MVC + Razor Views`  

- **Language:** C# 12  

- **Infrastructure (Docker Compose):**  
  - **Redpanda** (Kafka-compatible broker, `latest`)  
  - **Redpanda Console** (web UI, `latest`)  
  - **PostgreSQL 16**  
  - **pgAdmin 4 v8**  
  - **Grafana OSS latest**

- **Libraries (NuGet):**  
  - Kafka: [`Confluent.Kafka`](https://www.nuget.org/packages/Confluent.Kafka/)  
  - PostgreSQL: [`Npgsql`](https://www.npgsql.org/) + [`Dapper`](https://www.nuget.org/packages/Dapper/)  
  - Validation: [`FluentValidation`](https://docs.fluentvalidation.net/)  
  - API docs: [`Swashbuckle.AspNetCore`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) (Swagger/OpenAPI)  
  - Health Checks: [`Microsoft.Extensions.Diagnostics.HealthChecks`](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks)
## 4. Project Status

🚧 In active development:  
- [x] Infrastructure setup (Docker, Redpanda, PostgreSQL, Grafana)  
- [x] .NET solution created (Api, Ingestor, Aggregator, Web, Contracts)  
- [ ] Kafka producer implementation (API)  
- [ ] Kafka consumer + PostgreSQL insert (Ingestor)  
- [ ] Aggregation logic + Razor dashboard  
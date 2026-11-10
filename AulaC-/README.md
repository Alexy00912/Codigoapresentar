# CyberGuard

Base ASP.NET Core MVC para centralizar informação de cibersegurança organizacional. A Fase 1 estabelece a análise do sistema, o modelo de domínio, o schema SQLite e a estrutura do projeto. Não inclui módulos CRUD funcionais, autenticação, dashboards ou monitorização em tempo real.

## Tecnologias

- .NET 10 e ASP.NET Core MVC
- Entity Framework Core 10
- SQLite

## Estrutura

A aplicação está na pasta `CyberGuard/`.

- `Models/`: oito entidades de domínio e respetivas enumerações
- `Data/`: contexto EF Core e configuração do schema
- `Migrations/`: versões do schema SQLite
- `Services/`: estrutura inicial de validação de regras de negócio
- `ViewModels/`: classe base para modelos de vista
- `Documentation/`: requisitos, diagrama ER, modelo relacional, dicionário de dados e regras de negócio

## Modelo de dados

A base SQLite contém `Empresas`, `Equipamentos`, `Tecnicos`, `Incidentes`, `Vulnerabilidades`, `Intervencoes`, `RelatoriosSeguranca` e `RegistosAtividade`.

A connection string está em `CyberGuard/appsettings.json`. Por omissão, a base de dados é `CyberGuard/cyberguard.db`.

## Executar

A partir da raiz do repositório:

```powershell
cd CyberGuard
dotnet restore
dotnet tool restore
dotnet ef database update
dotnet run
```

O manifesto da ferramenta EF Core está em `CyberGuard/dotnet-tools.json`.

## Documentação

Consultar `CyberGuard/Documentation/` para os requisitos do sistema, o diagrama entidade-relação, o schema relacional, o dicionário de dados e as regras de negócio.


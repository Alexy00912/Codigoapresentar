# MotoStock — Gestão de Inventário e Stock de Motociclos

Sistema web desenvolvido em **ASP.NET Core MVC** para gestão de inventário de motociclos, com registo de marcas, categorias, fornecedores, motocicletas e movimentos de stock (entradas, saídas, reservas e cancelamentos). Base de dados **SQLite** com **Entity Framework Core**.

> Projeto académico desenvolvido com Visual Studio Code e .NET 10.

---

## ✨ Funcionalidades

### Gestão de Motocicletas
- Cadastrar, editar, consultar e eliminar motocicletas
- Associação obrigatória a marca, categoria e fornecedor
- Registo de características: modelo, cilindrada, ano, cor, matrícula, número de quadro, quilometragem, tipo de motor, data de registo, preço de compra, preço de venda, estado, descrição e observações
- Alteração do estado da motocicleta (Disponível, Reservada, Vendida, Em preparação, Indisponível)
- Número de quadro único por motocicleta; matrícula única quando preenchida

### Gestão de Catálogos (Marcas, Categorias, Fornecedores)
- CRUD completo de marcas, categorias e fornecedores
- Ativa/desativação de registos (apenas itens ativos podem ser associados a novas motocicletas)
- Fornecedores com dados de contacto (NIF, email, telefone, morada, código postal, localidade)

### Movimentos de Stock
- Registo de movimentos associados a uma motocicleta existente
- Tipos: Entrada, Saída, Reserva, Cancelamento de reserva
- Consulta do histórico de movimentos por motocicleta e geral

### Interface
- Arquitetura MVC com Views Razor e ViewModels com validação via Data Annotations
- Dropdowns dinâmicos para marcas, categorias, fornecedores e motocicletas
- Mensagens de sucesso via TempData

---

## 🛠 Tecnologias

| Camada | Tecnologia / Versão |
|--------|---------------------|
| Plataforma | .NET 10 (SDK 10.0.x) |
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core 10 |
| Base de dados | SQLite (`motostock.db`) |
| Linguagem | C# |
| IDE | Visual Studio Code |
| Pacotes NuGet | `Microsoft.EntityFrameworkCore.Sqlite` 10.0.14, `Microsoft.EntityFrameworkCore.Tools` 10.0.14 |

---

## 📁 Estrutura do Projeto

```
MotoStock/
├── Controllers/              # Controladores MVC
│   ├── HomeController.cs
│   ├── MarcasController.cs
│   ├── CategoriasController.cs
│   ├── FornecedoresController.cs
│   ├── MotocicletasController.cs
│   └── MovimentosStockController.cs
├── Data/                     # Contexto EF Core e Migrations
│   ├── ApplicationDbContext.cs
│   └── Migrations/
├── Models/                   # Entidades de domínio e Enums
│   ├── Marca.cs
│   ├── Categoria.cs
│   ├── Fornecedor.cs
│   ├── Motocicleta.cs
│   ├── MovimentoStock.cs
│   └── Enums/
│       ├── EstadoMotocicleta.cs
│       └── TipoMovimento.cs
├── ViewModels/               # Modelos de vista com validação
│   ├── MarcaViewModel.cs
│   ├── CategoriaViewModel.cs
│   ├── FornecedorViewModel.cs
│   ├── MotocicletaViewModel.cs
│   └── MovimentoStockViewModel.cs
├── Views/                    # Views Razor
│   ├── Home/
│   ├── Marcas/
│   ├── Categorias/
│   ├── Fornecedores/
│   ├── Motocicletas/
│   ├── MovimentosStock/
│   └── Shared/
├── wwwroot/                  # Ficheiros estáticos (CSS, JS, lib, imagens)
├── Documentation/            # Documentação técnica do projeto
│   ├── 01-Requisitos.md
│   ├── 02-Diagrama-ER.md
│   ├── 03-Modelo-Relacional.md
│   └── 04-Dicionario-Dados.md
├── appsettings.json          # Configuração (connection string, logging)
├── appsettings.Development.json
├── Program.cs                # Configuração da aplicação e pipeline HTTP
├── MotoStock.csproj          # Ficheiro de projeto .NET
└── motostock.db              # Base de dados SQLite (gerada)
```

---

## 🗄 Modelo de Dados

A aplicação utiliza 5 entidades principais com integridade referencial (`ON DELETE RESTRICT` — sem eliminações em cascata):

| Entidade | Chave Primária | Chaves Estrangeiras | Índices Únicos |
|----------|---------------|---------------------|----------------|
| **Marcas** | Id | — | — |
| **Categorias** | Id | — | — |
| **Fornecedores** | Id | — | — |
| **Motocicletas** | Id | MarcaId → Marcas.Id, CategoriaId → Categorias.Id, FornecedorId → Fornecedores.Id | NumeroQuadro, Matricula |
| **MovimentosStock** | Id | MotocicletaId → Motocicletas.Id | — |

**Relações:**
- Marca 1:N Motocicleta
- Categoria 1:N Motocicleta
- Fornecedor 1:N Motocicleta
- Motocicleta 1:N MovimentoStock

**Estados de motocicleta:** Disponível, Reservada, Vendida, Em preparação, Indisponível  
**Tipos de movimento:** Entrada, Saída, Reserva, Cancelamento de reserva

> Detalhes completos em `Documentation/03-Modelo-Relacional.md` e `Documentation/04-Dicionario-Dados.md`.

---

## 🚀 Como Executar

### Pré-requisitos
- [.NET SDK 10](https://dotnet.microsoft.com/download) instalado

### Passos

A partir da raiz do repositório:

```powershell
# Navegar para a pasta do projeto
cd AulaC-\MotoStock

# Restaurar dependências NuGet
dotnet restore

# Aplicar migrations e criar/atualizar a base de dados SQLite
dotnet ef database update

# Executar a aplicação
dotnet run
```

A aplicação estará disponível em `http://localhost:5000` (ou a porta indicada no terminal).

> Se não tiver a CLI do EF Core instalada, adicione-a com:
> ```powershell
> dotnet tool install --global dotnet-ef
> ```

### Connection String
Definida em `MotoStock/appsettings.json`:
```json
"ConnectionStrings": {
  "MotoStockConnection": "Data Source=motostock.db"
}
```

A base de dados é criada automaticamente na pasta do projeto ao executar `dotnet ef database update`.

---

## 📚 Documentação

Toda a documentação técnica encontra-se na pasta `MotoStock/Documentation/`:

| Ficheiro | Conteúdo |
|----------|----------|
| `01-Requisitos.md` | Requisitos funcionais, técnicos e regras de negócio |
| `02-Diagrama-ER.md` | Diagrama entidade-relacionamento (Mermaid) e cardinalidades |
| `03-Modelo-Relacional.md` | Tabelas, campos, tipos, chaves e integridade referencial |
| `04-Dicionario-Dados.md` | Dicionário de dados detalhado por entidade |

---

## 📋 Regras de Negócio

1. Cada motocicleta pertence a exatamente uma marca, uma categoria e um fornecedor.
2. O número de quadro (`NumeroQuadro`) é obrigatório e único.
3. A matrícula, quando preenchida, deve ser única (várias motocicletas podem não ter matrícula).
4. Preços de compra e venda são obrigatórios e armazenados com precisão decimal (tipo TEXT no SQLite).
5. Um movimento de stock só pode ser registado para uma motocicleta existente.
6. Eliminações usam comportamento `RESTRICT`: não são permitidas eliminações em cascata que destruam históricos.
7. Apenas marcas, categorias e fornecedores **ativos** podem ser selecionados no cadastro de motocicletas.

---

## 👥 Autoria

Projeto desenvolvido para apresentação académica.  
Repositório: [`Codigoapresentar`](https://github.com/Alexy00912/Codigoapresentar)

---

## 📄 Licença

Este projeto é de carácter académico. Consulte o repositório para informações de utilização.
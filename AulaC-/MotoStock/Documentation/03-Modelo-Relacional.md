# 03 - Modelo Relacional

## Tabelas

O modelo relacional é composto por cinco tabelas principais (mais a tabela de histórico de migrations do EF Core):

1. **Marcas**
2. **Categorias**
3. **Fornecedores**
4. **Motocicletas**
5. **MovimentosStock**

---

## 1. Tabela Marcas

| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador da marca (auto-incremento) |
| Nome | TEXT NOT NULL | — | Nome da marca |
| PaisOrigem | TEXT NULL | — | País de origem da marca |
| Descricao | TEXT NULL | — | Descrição da marca |
| Ativo | INTEGER NOT NULL | — | Indica se a marca está ativa (1) ou inativa (0) |

**Chave primária:** `Id`  
**Índices únicos:** nenhum além da PK

---

## 2. Tabela Categorias

| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador da categoria (auto-incremento) |
| Nome | TEXT NOT NULL | — | Nome da categoria |
| Descricao | TEXT NULL | — | Descrição da categoria |
| Ativo | INTEGER NOT NULL | — | Indica se a categoria está ativa (1) ou inativa (0) |

**Chave primária:** `Id`  
**Índices únicos:** nenhum além da PK

---

## 3. Tabela Fornecedores

| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador do fornecedor (auto-incremento) |
| Nome | TEXT NOT NULL | — | Nome do fornecedor |
| NIF | TEXT NOT NULL | — | Número de identificação fiscal |
| Email | TEXT NULL | — | Endereço de email |
| Telefone | TEXT NULL | — | Número de telefone |
| Morada | TEXT NULL | — | Morada |
| CodigoPostal | TEXT NULL | — | Código postal |
| Localidade | TEXT NULL | — | Localidade |
| DataRegisto | TEXT NOT NULL | — | Data de registo do fornecedor |
| Ativo | INTEGER NOT NULL | — | Indica se o fornecedor está ativo (1) ou inativo (0) |

**Chave primária:** `Id`  
**Índices únicos:** nenhum além da PK

---

## 4. Tabela Motocicletas

| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador da motocicleta (auto-incremento) |
| MarcaId | INTEGER NOT NULL | FK | Chave estrangeira para Marcas |
| CategoriaId | INTEGER NOT NULL | FK | Chave estrangeira para Categorias |
| FornecedorId | INTEGER NOT NULL | FK | Chave estrangeira para Fornecedores |
| Modelo | TEXT NOT NULL | — | Modelo da motocicleta |
| Cilindrada | INTEGER NOT NULL | — | Cilindrada (cc) |
| Ano | INTEGER NOT NULL | — | Ano de fabrico |
| Cor | TEXT NOT NULL | — | Cor |
| Matricula | TEXT NULL | UK | Matrícula (única quando preenchida) |
| NumeroQuadro | TEXT NOT NULL | UK | Número de quadro (único) |
| Quilometragem | INTEGER NOT NULL | — | Quilometragem |
| TipoMotor | TEXT NOT NULL | — | Tipo de motor |
| DataRegisto | TEXT NOT NULL | — | Data de registo da motocicleta |
| PrecoCompra | TEXT NOT NULL | — | Preço de compra (valor monetário) |
| PrecoVenda | TEXT NOT NULL | — | Preço de venda (valor monetário) |
| Estado | INTEGER NOT NULL | — | Estado (1=Disponível, 2=Reservada, 3=Vendida, 4=Em preparação, 5=Indisponível) |
| Descricao | TEXT NULL | — | Descrição |
| Observacoes | TEXT NULL | — | Observações |

**Chave primária:** `Id`  
**Índices únicos:** `NumeroQuadro`, `Matricula`  
**Chaves estrangeiras:**
- `MarcaId` → `Marcas(Id)` — ON DELETE RESTRICT
- `CategoriaId` → `Categorias(Id)` — ON DELETE RESTRICT
- `FornecedorId` → `Fornecedores(Id)` — ON DELETE RESTRICT

---

## 5. Tabela MovimentosStock

| Campo | Tipo | Chave | Descrição |
|-------|------|-------|-----------|
| Id | INTEGER | PK | Identificador do movimento (auto-incremento) |
| MotocicletaId | INTEGER NOT NULL | FK | Chave estrangeira para Motocicletas |
| TipoMovimento | INTEGER NOT NULL | — | Tipo (1=Entrada, 2=Saída, 3=Reserva, 4=Cancelamento de reserva) |
| DataMovimento | TEXT NOT NULL | — | Data do movimento |
| Quantidade | INTEGER NOT NULL | — | Quantidade movimentada |
| Preco | TEXT NULL | — | Preço unitário no momento do movimento (valor monetário) |
| Motivo | TEXT NULL | — | Motivo do movimento |
| Observacoes | TEXT NULL | — | Observações |

**Chave primária:** `Id`  
**Índices únicos:** nenhum além da PK  
**Chave estrangeira:**
- `MotocicletaId` → `Motocicletas(Id)` — ON DELETE RESTRICT

---

## Relações e Integridade Referencial

| # | Origem (FK) | Destino (PK) | Comportamento no DELETE | Justificação |
|---|-------------|--------------|-------------------------|--------------|
| R1 | Motocicletas.MarcaId | Marcas.Id | RESTRICT | Evitar apagar marca com motocicletas associadas |
| R2 | Motocicletas.CategoriaId | Categorias.Id | RESTRICT | Evitar apagar categoria com motocicletas associadas |
| R3 | Motocicletas.FornecedorId | Fornecedores.Id | RESTRICT | Evitar apagar fornecedor com motocicletas associadas |
| R4 | MovimentosStock.MotocicletaId | Motocicletas.Id | RESTRICT | Preservar histórico de movimentos de stock |

Todas as chaves estrangeiras são NOT NULL, garantindo que uma motocicleta não pode ter movimentos de stock associados a um registo inexistente e que cada motocicleta referencia marcas, categorias e fornecedores existentes.

**Nota sobre valores monetários:** em SQLite não existe tipo nativo DECIMAL. Os campos `PrecoCompra`, `PrecoVenda` e `Preco` são mapeados como `TEXT`, preservando a precisão decimal dos valores monetários (evitando erros de ponto flutuante do tipo REAL).

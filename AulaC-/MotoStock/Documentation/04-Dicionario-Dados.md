# 04 - Dicionário de Dados

Convenções: **PK** = Chave Primária, **FK** = Chave Estrangeira, **UK** = Chave Única (único), **NN** = Obrigatório (Not Null), **NUL** = Opcional (aceita NULL).

---

## Entidade: Marca

| Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|-------|---------------|-------------|-------|-----------|
| Id | INTEGER | NN | PK | Identificador único da marca |
| Nome | TEXT (máx. 100) | NN | — | Nome da marca |
| PaisOrigem | TEXT (máx. 100) | NUL | — | País de origem |
| Descricao | TEXT (máx. 500) | NUL | — | Descrição da marca |
| Ativo | INTEGER (bit) | NN | — | Estado ativo/inativo da marca |

---

## Entidade: Categoria

| Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|-------|---------------|-------------|-------|-----------|
| Id | INTEGER | NN | PK | Identificador único da categoria |
| Nome | TEXT (máx. 100) | NN | — | Nome da categoria |
| Descricao | TEXT (máx. 500) | NUL | — | Descrição da categoria |
| Ativo | INTEGER (bit) | NN | — | Estado ativo/inativo da categoria |

---

## Entidade: Fornecedor

| Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|-------|---------------|-------------|-------|-----------|
| Id | INTEGER | NN | PK | Identificador único do fornecedor |
| Nome | TEXT (máx. 150) | NN | — | Nome do fornecedor |
| NIF | TEXT (máx. 20) | NN | — | Número de identificação fiscal |
| Email | TEXT (máx. 150) | NUL | — | Endereço de email |
| Telefone | TEXT (máx. 50) | NUL | — | Número de telefone |
| Morada | TEXT (máx. 200) | NUL | — | Morada |
| CodigoPostal | TEXT (máx. 20) | NUL | — | Código postal |
| Localidade | TEXT (máx. 100) | NUL | — | Localidade |
| DataRegisto | DATETIME | NN | — | Data de registo do fornecedor |
| Ativo | INTEGER (bit) | NN | — | Estado ativo/inativo do fornecedor |

---

## Entidade: Motocicleta

| Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|-------|---------------|-------------|-------|-----------|
| Id | INTEGER | NN | PK | Identificador único da motocicleta |
| MarcaId | INTEGER | NN | FK | Referência à marca (Marca.Id) |
| CategoriaId | INTEGER | NN | FK | Referência à categoria (Categoria.Id) |
| FornecedorId | INTEGER | NN | FK | Referência ao fornecedor (Fornecedor.Id) |
| Modelo | TEXT (máx. 150) | NN | — | Modelo da motocicleta |
| Cilindrada | INTEGER | NN | — | Cilindrada em cc |
| Ano | INTEGER | NN | — | Ano de fabrico |
| Cor | TEXT (máx. 50) | NN | — | Cor da motocicleta |
| Matricula | TEXT (máx. 20) | NUL | UK | Matrícula (única quando preenchida) |
| NumeroQuadro | TEXT (máx. 100) | NN | UK | Número de quadro (único) |
| Quilometragem | INTEGER | NN | — | Quilometragem |
| TipoMotor | TEXT (máx. 50) | NN | — | Tipo de motor |
| DataRegisto | DATETIME | NN | — | Data de registo da motocicleta |
| PrecoCompra | DECIMAL (TEXT) | NN | — | Preço de compra (valor monetário) |
| PrecoVenda | DECIMAL (TEXT) | NN | — | Preço de venda (valor monetário) |
| Estado | INTEGER (enum) | NN | — | Estado da motocicleta |
| Descricao | TEXT (máx. 1000) | NUL | — | Descrição da motocicleta |
| Observacoes | TEXT (máx. 1000) | NUL | — | Observações |

**Valores do enum Estado:** 1=Disponível, 2=Reservada, 3=Vendida, 4=Em preparação, 5=Indisponível

---

## Entidade: MovimentoStock

| Campo | Tipo de Dados | Obrigatório | Chave | Descrição |
|-------|---------------|-------------|-------|-----------|
| Id | INTEGER | NN | PK | Identificador único do movimento |
| MotocicletaId | INTEGER | NN | FK | Referência à motocicleta (Motocicleta.Id) |
| TipoMovimento | INTEGER (enum) | NN | — | Tipo de movimento |
| DataMovimento | DATETIME | NN | — | Data do movimento |
| Quantidade | INTEGER | NN | — | Quantidade movimentada |
| Preco | DECIMAL (TEXT) | NUL | — | Preço no momento do movimento (valor monetário) |
| Motivo | TEXT (máx. 500) | NUL | — | Motivo do movimento |
| Observacoes | TEXT (máx. 1000) | NUL | — | Observações |

**Valores do enum TipoMovimento:** 1=Entrada, 2=Saída, 3=Reserva, 4=Cancelamento de reserva

---

## Resumo de Chaves

| Tabela | Chave Primária | Chaves Estrangeiras | Índices Únicos |
|--------|---------------|---------------------|----------------|
| Marcas | Id | — | — |
| Categorias | Id | — | — |
| Fornecedores | Id | — | — |
| Motocicletas | Id | MarcaId → Marcas.Id<br>CategoriaId → Categorias.Id<br>FornecedorId → Fornecedores.Id | NumeroQuadro, Matricula |
| MovimentosStock | Id | MotocicletaId → Motocicletas.Id | — |

**Comportamento de eliminação:** todas as FKs estão configuradas com `ON DELETE RESTRICT` (sem eliminação em cascata), preservando o histórico de movimentos de stock.

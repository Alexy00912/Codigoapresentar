# 02 - Diagrama Entidade-Relação (ER)

## Diagrama em Mermaid

```mermaid
erDiagram
    Marca ||--o{ Motocicleta : "possui"
    Categoria ||--o{ Motocicleta : "classifica"
    Fornecedor ||--o{ Motocicleta : "fornece"
    Motocicleta ||--|{ MovimentoStock : "gera"

    Marca {
        int Id PK
        string Nome UK1
        string PaisOrigem
        string Descricao
        bool Ativo
    }

    Categoria {
        int Id PK
        string Nome UK1
        string Descricao
        bool Ativo
    }

    Fornecedor {
        int Id PK
        string Nome
        string NIF UK1
        string Email
        string Telefone
        string Morada
        string CodigoPostal
        string Localidade
        date DataRegisto
        bool Ativo
    }

    Motocicleta {
        int Id PK
        int MarcaId FK
        int CategoriaId FK
        int FornecedorId FK
        string Modelo
        int Cilindrada
        int Ano
        string Cor
        string Matricula UK2
        string NumeroQuadro UK1
        int Quilometragem
        string TipoMotor
        date DataRegisto
        decimal PrecoCompra
        decimal PrecoVenda
        EstadoMotocicleta Estado
        string Descricao
        string Observacoes
    }

    MovimentoStock {
        int Id PK
        int MotocicletaId FK
        TipoMovimento TipoMovimento
        date DataMovimento
        int Quantidade
        decimal Preco
        string Motivo
        string Observacoes
    }
```

> **Legenda dos estados de motocicleta (`EstadoMotocicleta`):** Disponível, Reservada, Vendida, Em preparação, Indisponível.
>
> **Legenda dos tipos de movimento (`TipoMovimento`):** Entrada, Saída, Reserva, Cancelamento de reserva.

---

## Entidades e Cardinalidades

| Relação | Entidade 1 | Cardinalidade | Entidade 2 | Cardinalidade | Descrição |
|---------|------------|---------------|------------|---------------|-----------|
| R1 | Marca | 1 (um) | Motocicleta | N (muitos) | Uma marca pode ter várias motocicletas; cada motocicleta pertence a uma única marca. |
| R2 | Categoria | 1 (um) | Motocicleta | N (muitos) | Uma categoria pode classificar várias motocicletas; cada motocicleta tem uma única categoria. |
| R3 | Fornecedor | 1 (um) | Motocicleta | N (muitos) | Um fornecedor pode fornecer várias motocicletas; cada motocicleta tem um único fornecedor. |
| R4 | Motocicleta | 1 (um) | MovimentoStock | N (muitos) | Uma motocicleta pode ter vários movimentos de stock; cada movimento pertence a uma única motocicleta. |

**Tipo de participação:** todas as relações de "muitos" para "um" são de participação total na entidade "um" (a chave estrangeira é obrigatória na entidade "muitos"), exceto os campos opcionais indicados no dicionário de dados.

**Integridade referencial:** todas as chaves estrangeiras estão configuradas com comportamento `ON DELETE RESTRICT`, impedindo eliminações em cascata que destruam históricos.

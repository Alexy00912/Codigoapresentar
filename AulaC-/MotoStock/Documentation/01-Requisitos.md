# 01 - Requisitos

## MotoStock — Gestão de Inventário e Stock de Motociclos

---

## 1. Requisitos Funcionais

### 1.1 Gestão de Marcas
- Cadastrar uma nova marca de motociclos (nome, país de origem, descrição, estado ativo/inativo).
- Editar os dados de uma marca existente.
- Consultar a lista de marcas cadastradas.
- Consultar os detalhes de uma marca específica.
- Desativar/ativar uma marca (apenas marcas ativas podem ser associadas a novas motocicletas).

### 1.2 Gestão de Categorias
- Cadastrar uma nova categoria de classificação (ex: Desportiva, Naked, Enduro, Trail, Touring, Scooter, Custom, Cross, Clássica, Supermotard).
- Editar os dados de uma categoria existente.
- Consultar a lista de categorias cadastradas.
- Desativar/ativar uma categoria.

### 1.3 Gestão de Fornecedores
- Cadastrar um novo fornecedor (nome, NIF, email, telefone, morada, código postal, localidade, data de registo, estado ativo/inativo).
- Editar os dados de um fornecedor existente.
- Consultar a lista de fornecedores cadastrados.
- Desativar/ativar um fornecedor.

### 1.4 Gestão de Motocicletas
- Cadastrar uma nova motocicleta associando-a a uma marca, categoria e fornecedor existentes.
- Registar todas as características: modelo, cilindrada, ano, cor, matrícula, número de quadro, quilometragem, tipo de motor, data de registo, preço de compra, preço de venda, estado, descrição e observações.
- Editar os dados de uma motocicleta existente.
- Consultar a lista de motocicletas com filtros por marca, categoria, fornecedor e estado.
- Consultar os detalhes de uma motocicleta específica.
- Alterar o estado de uma motocicleta (Disponível, Reservada, Vendida, Em preparação, Indisponível).

### 1.5 Movimentos de Stock
- Registar um movimento de stock associado a uma motocicleta existente.
- Tipos de movimento permitidos: Entrada, Saída, Reserva, Cancelamento de reserva.
- Consultar o histórico de movimentos de stock de uma motocicleta.
- Consultar o histórico geral de entradas e saídas de stock.

---

## 2. Requisitos Técnicos

- Plataforma: ASP.NET Core MVC (Model-View-Controller).
- Linguagem: C#.
- Base de dados: SQLite (obrigatório).
- ORM: Entity Framework Core.
- Interface: Tabler UI com Bootstrap 5.
- Ambiente de desenvolvimento: Visual Studio Code.
- Versão do SDK .NET: estável compatível (verificado .NET 10.0.401).
- Estrutura de pastas: Controllers, Models, ViewModels, Data, Services, Views, wwwroot, Documentation.
- Não utilizar SQL Server nem outras bases de dados.
- Não utilizar dados fictícios para substituir a persistência real.

---

## 3. Regras de Negócio

1. Cada motocicleta deve pertencer a exatamente uma marca, uma categoria e um fornecedor.
2. O número de quadro (NúmeroQuadro) é obrigatório e deve ser único para cada motocicleta.
3. A matrícula, quando preenchida, deve ser única (várias motocicletas podem não ter matrícula ainda).
4. Os preços monetários (Preço de Compra e Preço de Venda) são obrigatórios e armazenados com precisão decimal.
5. Um movimento de stock só pode ser registado para uma motocicleta que já exista na base de dados.
6. Não são permitidas eliminações em cascata que destruam o histórico de movimentos de stock: ao eliminar uma marca, categoria, fornecedor ou motocicleta, os registos dependentes não são apagados automaticamente (comportamento Restrict).
7. Uma motocicleta não pode ter movimentos de stock associados a um registo de motocicleta inexistente (integridade referencial).
8. Apenas marcas, categorias e fornecedores activos podem ser selecionados no cadastro de motocicletas.

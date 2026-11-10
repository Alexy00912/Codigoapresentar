using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotoStock.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    NIF = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Telefone = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Morada = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CodigoPostal = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Localidade = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    DataRegisto = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PaisOrigem = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motocicletas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MarcaId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FornecedorId = table.Column<int>(type: "INTEGER", nullable: false),
                    Modelo = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Cilindrada = table.Column<int>(type: "INTEGER", nullable: false),
                    Ano = table.Column<int>(type: "INTEGER", nullable: false),
                    Cor = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Matricula = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    NumeroQuadro = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Quilometragem = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoMotor = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DataRegisto = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PrecoCompra = table.Column<decimal>(type: "TEXT", nullable: false),
                    PrecoVenda = table.Column<decimal>(type: "TEXT", nullable: false),
                    Estado = table.Column<int>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motocicletas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motocicletas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motocicletas_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motocicletas_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimentosStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MotocicletaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoMovimento = table.Column<int>(type: "INTEGER", nullable: false),
                    DataMovimento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: true),
                    Motivo = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Observacoes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentosStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentosStock_Motocicletas_MotocicletaId",
                        column: x => x.MotocicletaId,
                        principalTable: "Motocicletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Motocicletas_CategoriaId",
                table: "Motocicletas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Motocicletas_FornecedorId",
                table: "Motocicletas",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Motocicletas_MarcaId",
                table: "Motocicletas",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Motocicletas_Matricula",
                table: "Motocicletas",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motocicletas_NumeroQuadro",
                table: "Motocicletas",
                column: "NumeroQuadro",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosStock_MotocicletaId",
                table: "MovimentosStock",
                column: "MotocicletaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentosStock");

            migrationBuilder.DropTable(
                name: "Motocicletas");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Marcas");
        }
    }
}

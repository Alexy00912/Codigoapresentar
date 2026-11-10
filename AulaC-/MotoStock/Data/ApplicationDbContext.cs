using Microsoft.EntityFrameworkCore;
using MotoStock.Models;

namespace MotoStock.Data
{
    /// <summary>
    /// Contexto de base de dados da aplicação MotoStock.
    /// Gerencia as entidades de inventário de motociclos.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Motocicleta> Motocicletas { get; set; }
        public DbSet<MovimentoStock> MovimentosStock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ================================================================
            // Marca
            // ================================================================

            // ================================================================
            // Marca
            // ================================================================

            // ================================================================
            // Marca
            // ================================================================
            modelBuilder.Entity<Marca>(entity =>
            {

            // ================================================================
            // Motocicleta
            // ================================================================
            modelBuilder.Entity<Motocicleta>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Modelo).IsRequired().HasMaxLength(150);
                entity.Property(m => m.Cilindrada).IsRequired();
                entity.Property(m => m.Ano).IsRequired();
                entity.Property(m => m.Cor).IsRequired().HasMaxLength(50);
                entity.Property(m => m.Matricula).HasMaxLength(20);
                entity.Property(m => m.NumeroQuadro).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Quilometragem).IsRequired();
                entity.Property(m => m.TipoMotor).IsRequired().HasMaxLength(50);
                entity.Property(m => m.DataRegisto).IsRequired();
                entity.Property(m => m.PrecoCompra).IsRequired().HasColumnType("TEXT");
                entity.Property(m => m.PrecoVenda).IsRequired().HasColumnType("TEXT");
                entity.Property(m => m.Estado).IsRequired();
                entity.Property(m => m.Descricao).HasMaxLength(1000);
                entity.Property(m => m.Observacoes).HasMaxLength(1000);

                entity.HasIndex(m => m.NumeroQuadro).IsUnique();
                entity.HasIndex(m => m.Matricula).IsUnique();

                entity.HasOne(m => m.Marca)
                    .WithMany(marca => marca.Motocicletas)
                    .HasForeignKey(m => m.MarcaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Categoria)
                    .WithMany(categoria => categoria.Motocicletas)
                    .HasForeignKey(m => m.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Fornecedor)
                    .WithMany(fornecedor => fornecedor.Motocicletas)
                    .HasForeignKey(m => m.FornecedorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(m => m.MovimentosStock)
                    .WithOne(ms => ms.Motocicleta)
                    .HasForeignKey(ms => ms.MotocicletaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        /// <summary>
        /// Gera dados iniciais (seed) na base de dados ao criar as migrações.
        /// </summary>
        protected override void Seed(Microsoft.EntityFrameworkCore.MigrationBuilder migrationBuilder)
        {
            // 1. Marcas
            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nome", "PaisOrigem", "Descricao", "Ativo" },
                values: new object[,]
                {
                    { 1, "Yamaha", "Japão", "Marcas japonesas de engenharia equilibrada e desempenho.", true },
                    { 2, "Honda", "Japão", "Confiabilidade e tecnologia em motos de tracção.", true },
                    { 3, "KTM", "Austrália", "Desportividade, potência e precura.", true },
                    { 4, "Suzuki", "Japão", "Dominância em enduro e motocross.", true },
                    { 5, "Bajaj", "Índia", "Motos de bom custo-benefício.", true }
                }
            );

            // 2. Categorias
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome", "Descricao", "Ativo" },
                values: new object[,]
                {
                    { 1, "Supermotard", "Motos de alta potência para rali e paisagem.", true },
                    { 2, "Enduro", "Motos preparadas para terrenos difíceis.", true },
                    { 3, "Naked", "Estética agressiva com carroçaria resistente.", true },
                    { 4, "Trials", "Precisão mínima e ótima potência para desafios.", true },
                    { 5, "Scooter", "Ciclismo elétrico urbano de fácil manuseamento.", true }
                }
            );

            // 3. Fornecedores
            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Nome", "NIF", "Email", "Telefone", "Morada", "CodigoPostal", "Localidade", "DataRegisto", "Ativo" },
                values: new object[,]
                {
                    { 1, "MotoParts Lda", "507123456", "contact@motoparts.pt", "912345678", "Rua das Motas 123", "1000-001", "Lisboa", new DateTime(2024, 1, 15), true },
                    { 2, "Two Wheels Import", "508234567", "sales@twowheels.pt", "912345679", "Avenida do Comércio 456", "1000-002", "Lisboa", new DateTime(2024, 2, 20), true },
                    { 3, "Bikes Distribuidora", "509345678", "info@bikes.pt", "912345680", "Praça da Motocicleta 789", "1200-003", "Porto", true }
                }

            // 4. Motocicletas
            migrationBuilder.InsertData(
                table: "Motocicletas",
                columns: new[] {
                    "Id", "MarcaId", "CategoriaId", "FornecedorId", "Modelo", "Cilindrada",
                    "Ano", "Cor", "Matricula", "NumeroQuadro", "Quilometragem",
                    "TipoMotor", "DataRegisto", "PrecoCompra", "PrecoVenda", "Estado",
                    "Descricao", "Observacoes"
                },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "Yamaha MT-07", 689, 2020, "Vermelho", "AA-123-BB", "YAMA001", 12000, "2 cilindros", new DateTime(2024, 3, 1), 4500.00m, 5990.00m, 1, "Supermotard de alta potência, perfeita para rali.", "Primeira entrada de stock." },
                    { 2, 2, 1, 2, "Honda CBR600RR", 600, 2021, "Azul", "AB-456-CD", "HON002", 8500, "4 cilindros", new DateTime(2024, 3, 2), 5200.00m, 6990.00m, 1, "Supermotard esportiva, excelente aceleração.", "Reservada para um cliente especial." },
                    { 3, 3, 2, 3, "KTM 390 Enduro", 390, 2022, "Verde", "AC-789-DE", "KTM003", 5000, "2 cilindros", new DateTime(2024, 3, 3), 3200.00m, 4490.00m, 2, "Moto robusta para terrenos difíceis.", "Produto pouco usado, uma das melhores." },
                    { 4, 4, 2, 2, "Suzuki DR-Z400", 390, 2021, "Preto", "AD-101-FG", "SZU004", 15000, "2 cilindros", new DateTime(2024, 3, 4), 2800.00m, 3990.00m, 3, "Enduro clássica, ideal para trilhas.", "Boa para iniciantes." },
                    { 5, 5, 5, 1, "Bajaj Boxer Xt", 200, 2023, "Cinza", "AE-202-HI", "BAJ005", 2000, "2 cilindros", new DateTime(2024, 3, 5), 1800.00m, 2490.00m, 4, "Scooter urbano, facilidade de manuseamento.", "Precisa de revisão rápida." },
                    { 6, 1, 1, 1, "Yamaha Ténéré 700", 698, 2020, "Bege", "AF-303-JK", "YAM006", 22000, "2 cilindros", new DateTime(2024, 3, 6), 6800.00m, 8990.00m, 3, "Ténéré de aventura, para terras cruas.", "" },
                    { 7, 2, 3, 2, "Honda CBR300R", 300, 2022, "Prata", "AG-504-LM", "HON007", 6000, "4 cilindros", new DateTime(2024, 3, 7), 4100.00m, 5490.00m, 1, "Naked esportivo, ótimo para inicias.", "" }
                }
            );

            // 5. Movimentos de stock
            migrationBuilder.InsertData(
                table: "MovimentosStock",
                columns: new[] { "Id", "MotocicletaId", "TipoMovimento", "DataMovimento", "Quantidade", "Preco", "Motivo", "Observacoes" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 3, 15), 1, 4500.00m, "Entrada de stock", "" },
                    { 2, 2, 1, new DateTime(2024, 3, 16), 1, 5200.00m, "Entrada de stock", "" },
                    { 3, 3, 1, new DateTime(2024, 3, 17), 1, 3200.00m, "Entrada de stock inicial", "" },
                    { 4, 3, 2, new DateTime(2024, 3, 18), 1, 3500.00m, "Saída reservada ao cliente", "Reservada para rápido arredamento." },
                    { 5, 4, 1, new DateTime(2024, 3, 19), 1, 2800.00m, "Entrada de stock", "" },
                    { 6, 5, 1, new DateTime(2024, 3, 20), 1, 1800.00m, "Entrada de stock", "Aprovada pela administração." },
                    { 7, 6, 1, new DateTime(2024, 3, 21), 1, 6800.00m, "Entrada de stock", "" },
                    { 8, 7, 1, new DateTime(2024, 3, 22), 1, 4100.00m, "Entrada de stock", "" },
                    { 9, 1, 3, new DateTime(2024, 3, 23), 1, 5990.00m, "Reserva da mota MT-07", "Reservada para cliente VIP." },
                    { 10, 3, 3, new DateTime(2024, 3, 24), 1, 4490.00m, "Reserva da mota KTM Enduro", "Se não for retirada, passará para outro cliente." },
                    { 11, 1, 4, new DateTime(2024, 3, 25), 1, 5990.00m, "Cancelamento de reserva", "Cliente desistiu, voltou para disponível." },
                }
            );
        }
    }
}
            );
        }
    }
}
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Nome).IsRequired().HasMaxLength(100);
                entity.Property(m => m.PaisOrigem).HasMaxLength(100);
                entity.Property(m => m.Descricao).HasMaxLength(500);
                entity.Property(m => m.Ativo).IsRequired();

                entity.HasMany(m => m.Motocicletas)
                    .WithOne(moto => moto.Marca)
                    .HasForeignKey(moto => moto.MarcaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ================================================================
            // Categoria
            // ================================================================
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Descricao).HasMaxLength(500);
                entity.Property(c => c.Ativo).IsRequired();

                entity.HasMany(c => c.Motocicletas)
                    .WithOne(moto => moto.Categoria)
                    .HasForeignKey(moto => moto.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ================================================================
            // Fornecedor
            // ================================================================
            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Nome).IsRequired().HasMaxLength(150);
                entity.Property(f => f.NIF).IsRequired().HasMaxLength(20);
                entity.Property(f => f.Email).HasMaxLength(150);
                entity.Property(f => f.Telefone).HasMaxLength(50);
                entity.Property(f => f.Morada).HasMaxLength(200);
                entity.Property(f => f.CodigoPostal).HasMaxLength(20);
                entity.Property(f => f.Localidade).HasMaxLength(100);
                entity.Property(f => f.DataRegisto).IsRequired();
                entity.Property(f => f.Ativo).IsRequired();

                entity.HasMany(f => f.Motocicletas)
                    .WithOne(moto => moto.Fornecedor)
                    .HasForeignKey(moto => moto.FornecedorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ================================================================
            // Motocicleta
            // ================================================================
            modelBuilder.Entity<Motocicleta>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Modelo).IsRequired().HasMaxLength(150);
                entity.Property(m => m.Cilindrada).IsRequired();
                entity.Property(m => m.Ano).IsRequired();
                entity.Property(m => m.Cor).IsRequired().HasMaxLength(50);
                entity.Property(m => m.Matricula).HasMaxLength(20);
                entity.Property(m => m.NumeroQuadro).IsRequired().HasMaxLength(100);
                entity.Property(m => m.Quilometragem).IsRequired();
                entity.Property(m => m.TipoMotor).IsRequired().HasMaxLength(50);
                entity.Property(m => m.DataRegisto).IsRequired();
                entity.Property(m => m.PrecoCompra).IsRequired().HasColumnType("TEXT");
                entity.Property(m => m.PrecoVenda).IsRequired().HasColumnType("TEXT");
                entity.Property(m => m.Estado).IsRequired();
                entity.Property(m => m.Descricao).HasMaxLength(1000);
                entity.Property(m => m.Observacoes).HasMaxLength(1000);

                // Número de quadro único para cada motocicleta
                entity.HasIndex(m => m.NumeroQuadro).IsUnique();

                // Matrícula única quando preenchida (vários NULL são permitidos)
                entity.HasIndex(m => m.Matricula).IsUnique();

                entity.HasOne(m => m.Marca)
                    .WithMany(marca => marca.Motocicletas)
                    .HasForeignKey(m => m.MarcaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Categoria)
                    .WithMany(categoria => categoria.Motocicletas)
                    .HasForeignKey(m => m.CategoriaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(m => m.Fornecedor)
                    .WithMany(fornecedor => fornecedor.Motocicletas)
                    .HasForeignKey(m => m.FornecedorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(m => m.MovimentosStock)
                    .WithOne(ms => ms.Motocicleta)
                    .HasForeignKey(ms => ms.MotocicletaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ================================================================
            // MovimentoStock
            // ================================================================
            modelBuilder.Entity<MovimentoStock>(entity =>
            {
                entity.HasKey(ms => ms.Id);
                entity.Property(ms => ms.TipoMovimento).IsRequired();
                entity.Property(ms => ms.DataMovimento).IsRequired();
                entity.Property(ms => ms.Quantidade).IsRequired();
                entity.Property(ms => ms.Preco).HasColumnType("TEXT");
                entity.Property(ms => ms.Motivo).HasMaxLength(500);
                entity.Property(ms => ms.Observacoes).HasMaxLength(1000);

                entity.HasOne(ms => ms.Motocicleta)
                    .WithMany(moto => moto.MovimentosStock)
                    .HasForeignKey(ms => ms.MotocicletaId)
                    .OnDelete(DeleteBehavior.Restrict);

        /// <summary>
        /// Insere dados iniciais (seed) na base de dados.
        /// </summary>
        protected override void Seed(Microsoft.EntityFrameworkCore.MigrationBuilder migrationBuilder)
        {
            // 1. Marcas
            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nome", "PaisOrigem", "Descricao", "Ativo" },
                values: new object[,]
                {
                    { 1, "Yamaha", "Japão", "Marcas japonesas de engenharia equilibrada e desempenho.", true },
                    { 2, "Honda", "Japão", "Confiabilidade e tecnologia em motos de tracção.", true },
                    { 3, "KTM", "Austrália", "Desportividade, potência e precura.", true },
                    { 4, "Suzuki", "Japão", "Dominância em enduro e motocross.", true },
                    { 5, "Bajaj", "Índia", "Motos de bom custo-benefício.", true }
                }
            );

            // 2. Categorias
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome", "Descricao", "Ativo" },
                values: new object[,]
                {
                    { 1, "Supermotard", "Motos de alta potência para rali e paisagem.", true },
                    { 2, "Enduro", "Motos preparadas para terrenos difíceis.", true },
                    { 3, "Naked", "Estética agressiva com carroçaria resistente.", true },
                    { 4, "Trials", "Precisão mínima e ótima potência para desafios.", true },
                    { 5, "Scooter", "Ciclismo elétrico urbano de fácil manuseamento.", true }
                }
            );
            });
        }
    }
            // 3. Fornecedores
            migrationBuilder.InsertData(
                table: "Fornecedores",
                columns: new[] { "Id", "Nome", "NIF", "Email", "Telefone", "Morada", "CodigoPostal", "Localidade", "DataRegisto", "Ativo" },
                values: new object[,]
                {
                    { 1, "MotoParts Lda", "507123456", "contact@motoparts.pt", "912345678", "Rua das Motas 123", "1000-001", "Lisboa", new DateTime(2024, 1, 15), true },
                    { 2, "Two Wheels Import", "508234567", "sales@twowheels.pt", "912345679", "Avenida do Comércio 456", "1000-002", "Lisboa", new DateTime(2024, 2, 20), true },
                    { 3, "Bikes Distribuidora", "509345678", "info@bikes.pt", "912345680", "Praça da Motocicleta 789", "1200-003", "Porto", true }
                }
            );

            // 4. Motocicletas
            migrationBuilder.InsertData(
                table: "Motocicletas",
                columns: new[] {
                    "Id", "MarcaId", "CategoriaId", "FornecedorId", "Modelo", "Cilindrada",
                    "Ano", "Cor", "Matricula", "NumeroQuadro", "Quilometragem",
                    "TipoMotor", "DataRegisto", "PrecoCompra", "PrecoVenda", "Estado",
                    "Descricao", "Observacoes"
                },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "Yamaha MT-07", 689, 2020, "Vermelho", "AA-123-BB", "YAMA001", 12000, "2 cilindros", new DateTime(2024, 3, 1), 4500.00m, 5990.00m, 1, "Supermotard de alta potência, perfeita para rali.", "Primeira entrada de stock." },
                    { 2, 2, 1, 2, "Honda CBR600RR", 600, 2021, "Azul", "AB-456-CD", "HON002", 8500, "4 cilindros", new DateTime(2024, 3, 2), 5200.00m, 6990.00m, 1, "Supermotard esportiva, excelente aceleração.", "Reservada para um cliente especial." },
                    { 3, 3, 2, 3, "KTM 390 Enduro", 390, 2022, "Verde", "AC-789-DE", "KTM003", 5000, "2 cilindros", new DateTime(2024, 3, 3), 3200.00m, 4490.00m, 2, "Moto robusta para terrenos difíceis.", "Produto pouco usado, uma das melhores." },
                    { 4, 4, 2, 2, "Suzuki DR-Z400", 390, 2021, "Preto", "AD-101-FG", "SZU004", 15000, "2 cilindros", new DateTime(2024, 3, 4), 2800.00m, 3990.00m, 3, "Enduro clássica, ideal para trilhas.", "Boa para iniciantes." },
                    { 5, 5, 5, 1, "Bajaj Boxer Xt", 200, 2023, "Cinza", "AE-202-HI", "BAJ005", 2000, "2 cilindros", new DateTime(2024, 3, 5), 1800.00m, 2490.00m, 4, "Scooter urbano, facilidade de manuseamento.", "Precisa de revisão rápida." },
                    { 6, 1, 1, 1, "Yamaha Ténéré 700", 698, 2020, "Bege", "AF-303-JK", "YAM006", 22000, "2 cilindros", new DateTime(2024, 3, 6), 6800.00m, 8990.00m, 3, "Ténéré de aventura, para terras cruas.", "" },
                    { 7, 2, 3, 2, "Honda CBR300R", 300, 2022, "Prata", "AG-504-LM", "HON007", 6000, "4 cilindros", new DateTime(2024, 3, 7), 4100.00m, 5490.00m, 1, "Naked esportivo, ótimo para inicias.", ""
);
                }
            );
}



        /// <summary>
        /// Gera dados iniciais (seed) na base de dados ao criar as migrações.
        /// </summary>
        protected override void Seed(Microsoft.EntityFrameworkCore.MigrationBuilder migrationBuilder)
        {
            // 1. Marcas
            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nome", "PaisOrigem", "Descricao", "Ativo" },
                values: new object[,]
                {
                    { 1, "Yamaha", "Japão", "Marcas japonesas de engenharia equilibrada e desempenho.", true },
                    { 2, "Honda", "Japão", "Confiabilidade e tecnologia em motos de tracção.", true },
                    { 3, "KTM", "Austrália", "Desportividade, potência e precura.", true },
                    { 4, "Suzuki", "Japão", "Dominância em enduro e motocross.", true },
                    { 5, "Bajaj", "Índia", "Motos de bom custo-benefício.", true }
                }
            );

            // 2. Categorias
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome", "Descricao", "Ativo" },
                values: new object[,]
                {
                    { 1, "Supermotard", "Motos de alta potência para rali e paisagem.", true },
                    { 2, "Enduro", "Motos preparadas para terrenos difíceis.", true },
                    { 3, "Naked", "Estética agressiva com carroçaria resistente.", true },
                    { 4, "Trials", "Precisão mínima e ótima potência para desafios.", true },
                    { 5, "Scooter", "Ciclismo elétrico urbano de fácil manuseamento.", true }
                }
            );

            // 4. Motocicletas
            migrationBuilder.InsertData(
                table: "Motocicletas",
                columns: new[] {
                    "Id", "MarcaId", "CategoriaId", "FornecedorId", "Modelo", "Cilindrada",
                    "Ano", "Cor", "Matricula", "NumeroQuadro", "Quilometragem",
                    "TipoMotor", "DataRegisto", "PrecoCompra", "PrecoVenda", "Estado",
                    "Descricao", "Observacoes"
                },
                values: new object[,]
                {
                    { 1, 1, 1, 1, "Yamaha MT-07", 689, 2020, "Vermelho", "AA-123-BB", "YAMA001", 12000, "2 cilindros", new DateTime(2024, 3, 1), 4500.00m, 5990.00m, 1, "Supermotard de alta potência, perfeita para rali.", "Primeira entrada de stock." },
                    { 2, 2, 1, 2, "Honda CBR600RR", 600, 2021, "Azul", "AB-456-CD", "HON002", 8500, "4 cilindros", new DateTime(2024, 3, 2), 5200.00m, 6990.00m, 1, "Supermotard esportiva, excelente aceleração.", "Reservada para um cliente especial." },
                    { 3, 3, 2, 3, "KTM 390 Enduro", 390, 2022, "Verde", "AC-789-DE", "KTM003", 5000, "2 cilindros", new DateTime(2024, 3, 3), 3200.00m, 4490.00m, 2, "Moto robusta para terrenos difíceis.", "Produto pouco usado, uma das melhores." },
                    { 4, 4, 2, 2, "Suzuki DR-Z400", 390, 2021, "Preto", "AD-101-FG", "SZU004", 15000, "2 cilindros", new DateTime(2024, 3, 4), 2800.00m, 3990.00m, 3, "Enduro clássica, ideal para trilhas.", "Boa para iniciantes." },
                    { 5, 5, 5, 1, "Bajaj Boxer Xt", 200, 2023, "Cinza", "AE-202-HI", "BAJ005", 2000, "2 cilindros", new DateTime(2024, 3, 5), 1800.00m, 2490.00m, 4, "Scooter urbano, facilidade de manuseamento.", "Precisa de revisão rápida." },
                    { 6, 1, 1, 1, "Yamaha Ténéré 700", 698, 2020, "Bege", "AF-303-JK", "YAM006", 22000, "2 cilindros", new DateTime(2024, 3, 6), 6800.00m, 8990.00m, 3, "Ténéré de aventura, para terras cruas.", "" },
                    { 7, 2, 3, 2, "Honda CBR300R", 300, 2022, "Prata", "AG-504-LM", "HON007", 6000, "4 cilindros", new DateTime(2024, 3, 7), 4100.00m, 5490.00m, 1, "Naked esportivo, ótimo para inicias.", ""
                }
            );

            // 5. Movimentos de stock
            migrationBuilder.InsertData(
                table: "MovimentosStock",
                columns: new[] { "Id", "MotocicletaId", "TipoMovimento", "DataMovimento", "Quantidade", "Preco", "Motivo", "Observacoes" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 3, 15), 1, 4500.00m, "Entrada de stock", "" },
                    { 2, 2, 1, new DateTime(2024, 3, 16), 1, 5200.00m, "Entrada de stock", "" },
                    { 3, 3, 1, new DateTime(2024, 3, 17), 1, 3200.00m, "Entrada de stock inicial", "" },
                    { 4, 3, 2, new DateTime(2024, 3, 18), 1, 3500.00m, "Saída reservada ao cliente", "Reservada para rápido arredamento." },
                    { 5, 4, 1, new DateTime(2024, 3, 19), 1, 2800.00m, "Entrada de stock", "" },
                    { 6, 5, 1, new DateTime(2024, 3, 20), 1, 1800.00m, "Entrada de stock", "Aprovada pela administração." },
                    { 7, 6, 1, new DateTime(2024, 3, 21), 1, 6800.00m, "Entrada de stock", "" },
                    { 8, 7, 1, new DateTime(2024, 3, 22), 1, 4100.00m, "Entrada de stock", "" },
                    { 9, 1, 3, new DateTime(2024, 3, 23), 1, 5990.00m, "Reserva da mota MT-07", "Reservada para cliente VIP." },
                    { 10, 3, 3, new DateTime(2024, 3, 24), 1, 4490.00m, "Reserva da mota KTM Enduro", "Se não for retirada, passará para outro cliente." },
                    { 11, 1, 4, new DateTime(2024, 3, 25), 1, 5990.00m, "Cancelamento de reserva", "Cliente desistiu, voltou para disponível." },
                }
            );
        }
    }
}
            migrationBuilder.InsertData(
                table: "MovimentosStock",
                columns: new[] { "Id", "MotocicletaId", "TipoMovimento", "DataMovimento", "Quantidade", "Preco", "Motivo", "Observacoes" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 3, 15), 1, 4500.00m, "Entrada de stock", "" },
                    { 2, 2, 1, new DateTime(2024, 3, 16), 1, 5200.00m, "Entrada de stock", "" },
                    { 3, 3, 1, new DateTime(2024, 3, 17), 1, 3200.00m, "Entrada de stock inicial", "" },
                    { 4, 3, 2, new DateTime(2024, 3, 18), 1, 3500.00m, "Saída reservada ao cliente", "Reservada para rápido arredamento." },
                    { 5, 4, 1, new DateTime(2024, 3, 19), 1, 2800.00m, "Entrada de stock", "" },
                    { 6, 5, 1, new DateTime(2024, 3, 20), 1, 1800.00m, "Entrada de stock", "Aprovada pela administração." },
                    { 7, 6, 1, new DateTime(2024, 3, 21), 1, 6800.00m, "Entrada de stock", "" },
                    { 8, 7, 1, new DateTime(2024, 3, 22), 1, 4100.00m, "Entrada de stock", "" },
                    { 9, 1, 3, new DateTime(2024, 3, 23), 1, 5990.00m, "Reserva da mota MT-07", "Reservada para cliente VIP." },
                    { 10, 3, 3, new DateTime(2024, 3, 24), 1, 4490.00m, "Reserva da mota KTM Enduro", "Se não for retirada, passará para outro cliente." },
                    { 11, 1, 4, new DateTime(2024, 3, 25), 1, 5990.00m, "Cancelamento de reserva", "Cliente desistiu, voltou para disponível." },
                }
            );
        }
    }
}
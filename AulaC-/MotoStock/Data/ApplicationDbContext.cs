using Microsoft.EntityFrameworkCore;
using MotoStock.Models;
using MotoStock.Models.Enums;

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
            modelBuilder.Entity<Marca>(entity =>
            {
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
            });
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using MotoStock.Models.Enums;

namespace MotoStock.Models
{
    /// <summary>
    /// Motociclo em gestão de inventário.
    /// </summary>
    public class Motocicleta
    {
        public int Id { get; set; }

        [Required]
        public int MarcaId { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        [Required]
        public int FornecedorId { get; set; }

        [Required]
        [StringLength(150)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public int Cilindrada { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        [StringLength(50)]
        public string Cor { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Matricula { get; set; }

        [Required]
        [StringLength(100)]
        public string NumeroQuadro { get; set; } = string.Empty;

        [Required]
        public int Quilometragem { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoMotor { get; set; } = string.Empty;

        [Required]
        public DateTime DataRegisto { get; set; }

        [Required]
        public decimal PrecoCompra { get; set; }

        [Required]
        public decimal PrecoVenda { get; set; }

        [Required]
        public EstadoMotocicleta Estado { get; set; }

        [StringLength(1000)]
        public string? Descricao { get; set; }

        [StringLength(1000)]
        public string? Observacoes { get; set; }

        public virtual Marca Marca { get; set; } = null!;
        public virtual Categoria Categoria { get; set; } = null!;
        public virtual Fornecedor Fornecedor { get; set; } = null!;
        public virtual ICollection<MovimentoStock> MovimentosStock { get; set; } = new List<MovimentoStock>();
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using MotoStock.Models.Enums;

namespace MotoStock.Models
{
    /// <summary>
    /// Registo de um movimento de stock (entrada, saída, reserva, cancelamento de reserva).
    /// </summary>
    public class MovimentoStock
    {
        public int Id { get; set; }

        [Required]
        public int MotocicletaId { get; set; }

        [Required]
        public TipoMovimento TipoMovimento { get; set; }

        [Required]
        public DateTime DataMovimento { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public decimal? Preco { get; set; }

        [StringLength(500)]
        public string? Motivo { get; set; }

        [StringLength(1000)]
        public string? Observacoes { get; set; }

        public virtual Motocicleta Motocicleta { get; set; } = null!;
    }
}

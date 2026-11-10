using System.ComponentModel.DataAnnotations;

namespace MotoStock.Models
{
    /// <summary>
    /// Marca de motociclos (ex: Yamaha, Honda, KTM).
    /// </summary>
    public class Marca
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(100)]
        public string? PaisOrigem { get; set; }

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Required]
        public bool Ativo { get; set; } = true;

        public virtual ICollection<Motocicleta> Motocicletas { get; set; } = new List<Motocicleta>();
    }
}

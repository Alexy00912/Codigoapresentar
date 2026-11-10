using System.ComponentModel.DataAnnotations;

namespace MotoStock.Models
{
    /// <summary>
    /// Categoria de classificação de motociclos
    /// (ex: Desportiva, Naked, Enduro, Scooter, etc.).
    /// </summary>
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Required]
        public bool Ativo { get; set; } = true;

        public virtual ICollection<Motocicleta> Motocicletas { get; set; } = new List<Motocicleta>();
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace MotoStock.Models
{
    /// <summary>
    /// Fornecedor de motociclos.
    /// </summary>
    public class Fornecedor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string NIF { get; set; } = string.Empty;

        [StringLength(150)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Telefone { get; set; }

        [StringLength(200)]
        public string? Morada { get; set; }

        [StringLength(20)]
        public string? CodigoPostal { get; set; }

        [StringLength(100)]
        public string? Localidade { get; set; }

        [Required]
        public DateTime DataRegisto { get; set; }

        [Required]
        public bool Ativo { get; set; } = true;

        public virtual ICollection<Motocicleta> Motocicletas { get; set; } = new List<Motocicleta>();
    }
}

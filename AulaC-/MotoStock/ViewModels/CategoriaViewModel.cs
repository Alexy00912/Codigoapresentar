using System.ComponentModel.DataAnnotations;

namespace MotoStock.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não pode exceder 100 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;
    }
}

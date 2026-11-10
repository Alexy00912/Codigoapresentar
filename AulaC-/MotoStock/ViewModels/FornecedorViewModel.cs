using System;
using System.ComponentModel.DataAnnotations;

namespace MotoStock.ViewModels
{
    public class FornecedorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O NIF é obrigatório.")]
        [StringLength(20, ErrorMessage = "O NIF não pode exceder 20 caracteres.")]
        [Display(Name = "NIF")]
        public string NIF { get; set; } = string.Empty;

        [StringLength(150, ErrorMessage = "O email não pode exceder 150 caracteres.")]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [StringLength(50, ErrorMessage = "O telefone não pode exceder 50 caracteres.")]
        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        [StringLength(200, ErrorMessage = "A morada não pode exceder 200 caracteres.")]
        [Display(Name = "Morada")]
        public string? Morada { get; set; }

        [StringLength(20, ErrorMessage = "O código postal não pode exceder 20 caracteres.")]
        [Display(Name = "Código Postal")]
        public string? CodigoPostal { get; set; }

        [StringLength(100, ErrorMessage = "A localidade não pode exceder 100 caracteres.")]
        [Display(Name = "Localidade")]
        public string? Localidade { get; set; }

        [Required(ErrorMessage = "A data de registo é obrigatória.")]
        [Display(Name = "Data de Registo")]
        [DataType(DataType.Date)]
        public DateTime DataRegisto { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;
    }
}

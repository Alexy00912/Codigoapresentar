using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MotoStock.Models.Enums;

namespace MotoStock.ViewModels
{
    public class MotocicletaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A marca é obrigatória.")]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O fornecedor é obrigatório.")]
        [Display(Name = "Fornecedor")]
        public int FornecedorId { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [StringLength(150, ErrorMessage = "O modelo não pode exceder 150 caracteres.")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cilindrada é obrigatória.")]
        [Display(Name = "Cilindrada (cc)")]
        [Range(1, 99999, ErrorMessage = "A cilindrada deve ser um valor positivo.")]
        public int Cilindrada { get; set; }

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [Display(Name = "Ano")]
        [Range(1900, 2100, ErrorMessage = "O ano deve estar entre 1900 e 2100.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "A cor é obrigatória.")]
        [StringLength(50, ErrorMessage = "A cor não pode exceder 50 caracteres.")]
        [Display(Name = "Cor")]
        public string Cor { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "A matrícula não pode exceder 20 caracteres.")]
        [Display(Name = "Matrícula")]
        public string? Matricula { get; set; }

        [Required(ErrorMessage = "O número de quadro é obrigatório.")]
        [StringLength(100, ErrorMessage = "O número de quadro não pode exceder 100 caracteres.")]
        [Display(Name = "Número de Quadro")]
        public string NumeroQuadro { get; set; } = string.Empty;

        [Required(ErrorMessage = "A quilometragem é obrigatória.")]
        [Display(Name = "Quilometragem")]
        [Range(0, 999999, ErrorMessage = "A quilometragem deve ser um valor positivo.")]
        public int Quilometragem { get; set; }

        [Required(ErrorMessage = "O tipo de motor é obrigatório.")]
        [StringLength(50, ErrorMessage = "O tipo de motor não pode exceder 50 caracteres.")]
        [Display(Name = "Tipo de Motor")]
        public string TipoMotor { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data de registo é obrigatória.")]
        [Display(Name = "Data de Registo")]
        [DataType(DataType.Date)]
        public DateTime DataRegisto { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "O preço de compra é obrigatório.")]
        [Display(Name = "Preço de Compra")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço de compra deve ser superior a zero.")]
        public decimal PrecoCompra { get; set; }

        [Required(ErrorMessage = "O preço de venda é obrigatório.")]
        [Display(Name = "Preço de Venda")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço de venda deve ser superior a zero.")]
        public decimal PrecoVenda { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [Display(Name = "Estado")]
        public EstadoMotocicleta Estado { get; set; }

        [StringLength(1000, ErrorMessage = "A descrição não pode exceder 1000 caracteres.")]
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        [StringLength(1000, ErrorMessage = "As observações não podem exceder 1000 caracteres.")]
        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }

        // Listas para dropdowns
        public IEnumerable<SelectListItem> Marcas { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Categorias { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Fornecedores { get; set; } = new List<SelectListItem>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using MotoStock.Models.Enums;

namespace MotoStock.ViewModels
{
    public class MovimentoStockViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A motocicleta é obrigatória.")]
        [Display(Name = "Motocicleta")]
        public int MotocicletaId { get; set; }

        [Required(ErrorMessage = "O tipo de movimento é obrigatório.")]
        [Display(Name = "Tipo de Movimento")]
        public TipoMovimento TipoMovimento { get; set; }

        [Required(ErrorMessage = "A data do movimento é obrigatória.")]
        [Display(Name = "Data do Movimento")]
        [DataType(DataType.DateTime)]
        public DateTime DataMovimento { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Display(Name = "Quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser superior a zero.")]
        public int Quantidade { get; set; }

        [Display(Name = "Preço")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser superior a zero.")]
        public decimal? Preco { get; set; }

        [StringLength(500, ErrorMessage = "O motivo não pode exceder 500 caracteres.")]
        [Display(Name = "Motivo")]
        public string? Motivo { get; set; }

        [StringLength(1000, ErrorMessage = "As observações não podem exceder 1000 caracteres.")]
        [Display(Name = "Observações")]
        public string? Observacoes { get; set; }

        // Lista para dropdown
        public IEnumerable<SelectListItem> Motocicletas { get; set; } = new List<SelectListItem>();
    }
}

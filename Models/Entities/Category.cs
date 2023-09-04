using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGF.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace SGF.Models.Entities 
{
  public class Category 
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [Display(Name = "Nome da Categoria")]
    [MaxLength(25, ErrorMessage = "A nome da Categoria deve ter no máximo 25 caracteres.")]
    [MinLength(3, ErrorMessage = "A nome da Categoria deve ter no mínimo 3 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "A descrição da categoria é obrigatória.")]
    [Display(Name = "Descrição")]
    [MaxLength(80, ErrorMessage = "A descrição deve ter no máximo 80 caracteres.")]
    [MinLength(3, ErrorMessage = "A descrição deve ter no mínimo 3 caracteres.")]
    public string Description { get; set; }

    public string UserId { get; set; }
  }
}
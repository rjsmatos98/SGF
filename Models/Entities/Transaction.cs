using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SGF.Models.Enums;

namespace SGF.Models.Entities 
{
  public class Transaction 
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "A Descrição da transação é obrigatório")]
    [Display(Name = "Descrição")]
    [MaxLength(80, ErrorMessage = "A descrição deve ter no máximo 80 caracteres.")]
    [MinLength(3, ErrorMessage = "A descrição deve ter no mínimo 3 caracteres.")]
    public string Description { get; set; }

    public int CategoryId { get; set; }
    
    [Display(Name = "Categoria")]
    public Category Category { get; set; }

    [Required(ErrorMessage = "A data da transação é obrigatória.")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Date { get; set; }
    
    [Required(ErrorMessage = "O tipo de transação é obrigatória.")]
    [Display(Name = "Tipo")]
    public TransactionType TransactionType { get; set; }

    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = true)]
    [Display(Name = "Valor")]
    [Column(TypeName = "decimal(15, 2)")]
    public decimal Amount  { get; set; }

    public string UserId { get; set; }
  }
}
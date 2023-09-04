using System.ComponentModel.DataAnnotations;

namespace SGF.Models.Enums
{
  public enum TransactionType 
  {
      [Display(Name = "Entrada")]
      Entrada,
      [Display(Name = "Saída")]
      Saida,
  }
}
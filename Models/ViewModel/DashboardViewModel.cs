using System.ComponentModel.DataAnnotations;

namespace SGF.Models.ViewModel
{
  public class DashboardViewModel
  {
    [Display(Name = "Entradas")]
    public decimal TotalIncome { get; set; }
    
    [Display(Name = "Saídas")]
    public decimal TotalOutcome { get; set; }

    [Display(Name = "Total")]
    public decimal TotalBalance { get; set; }
  }
}
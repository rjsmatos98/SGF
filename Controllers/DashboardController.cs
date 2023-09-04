using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGF.Models.Data;
using SGF.Models.ViewModel;
using SGF.Services;

namespace SGF.Controllers
{
  [Authorize]
  public class DashboardController : Controller
  {
    private readonly TransactionService _transactionService;
    public DashboardController(TransactionService transactionService)
    {
      _transactionService = transactionService;
    }

    public async Task<IActionResult> Index()
    {
      string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

      decimal totalIncome = await _transactionService.CalculateTotalByType(userId, Models.Enums.TransactionType.Entrada);
      decimal totalOutcome = await _transactionService.CalculateTotalByType(userId, Models.Enums.TransactionType.Saida);
      decimal totalBalance = await _transactionService.CalculateBalance(userId);

      var viewModel = new DashboardViewModel
      {
        TotalIncome = totalIncome,
        TotalOutcome = totalOutcome,
        TotalBalance = totalBalance
      };

      return View(viewModel);
    }
  }
}
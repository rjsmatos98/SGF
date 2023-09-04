using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SGF.Models.Data;
using SGF.Models.Enums;

namespace SGF.Services
{
  public class TransactionService
  {
    private readonly AppDbContext _context;

    public TransactionService(AppDbContext context)
    {
      _context = context;
    }

    public async Task<decimal> CalculateTotalByType(string userId, TransactionType transactionType)
    {
      decimal sumTransactions = await _context.Transaction
        .Where(t => t.UserId == userId && t.TransactionType == transactionType)
        .SumAsync(t => t.Amount); 

      return sumTransactions;
    }

    public async Task<decimal> CalculateBalance(string userId)
    {
      decimal totalIncome = await CalculateTotalByType(userId, TransactionType.Entrada);
      decimal totalOutcome = await CalculateTotalByType(userId, TransactionType.Saida);

      decimal sumTransactions = totalIncome - totalOutcome;

      return sumTransactions;
    }
  }
}
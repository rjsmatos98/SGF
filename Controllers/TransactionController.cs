using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SGF.Models.Data;
using SGF.Models.Entities;
using System.Security.Claims;
using System.Collections.Generic;

namespace SGF.Controllers
{
  [Authorize]
  public class TransactionController : Controller 
  {
    private readonly AppDbContext _context;

    public TransactionController(AppDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      return View(await _context.Transaction.Where(t => t.UserId == userId).OrderByDescending(t => t.Date).ToListAsync());
    }

    public async Task<IActionResult> GetCategories()
    {
      string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      List<Category> categories = await _context.Category.Where(t => t.UserId == userId).ToListAsync();
      return await Task.FromResult(Json(categories));
    }

    [HttpPost]
    public async Task<IActionResult> Create(Transaction transaction)
    {
      if(ModelState.IsValid)
      {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        transaction.UserId = userId;
        _context.Add(transaction);
        await _context.SaveChangesAsync();
        
        return await Task.FromResult(Json(new { isValid = true, redirectTo = Url.Action(nameof(Index)) }));
      }
      
      var errors = ModelState.Values
          .SelectMany(v => v.Errors)
          .Select(e => e.ErrorMessage)
          .ToList();

      return await Task.FromResult(Json(new { isValid = false, errors }));
    }

    public async Task<IActionResult> Edit(int? id)
    {
      string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      Transaction transaction = await _context.Transaction.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == id);
      
      if(transaction == null)
      {
          return NotFound();
      }

      return await Task.FromResult(Json(transaction));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Transaction transaction)
    {
      if(ModelState.IsValid)
      {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Transaction editTransaction = await _context.Transaction.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == transaction.Id);
        
        if(editTransaction != null)
        {
          editTransaction.Description = transaction.Description;
          editTransaction.CategoryId = transaction.CategoryId;
          editTransaction.Date = transaction.Date;
          editTransaction.TransactionType = transaction.TransactionType;
          editTransaction.Amount = transaction.Amount;

          await _context.SaveChangesAsync();
          return await Task.FromResult(Json(new { isValid = true, redirectTo = Url.Action(nameof(Index)) }));
        }
        
        return await Task.FromResult(Json(new { isValid = false, errors = "Transação não encontrada!" }));
      }
      
      var errors = ModelState.Values
          .SelectMany(v => v.Errors)
          .Select(e => e.ErrorMessage)
          .ToList();

      return await Task.FromResult(Json(new { isValid = false, errors }));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int? id)
    {
      string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      Transaction transaction = await _context.Transaction.FirstOrDefaultAsync(t => t.UserId == userId && t.Id == id);

      if(transaction != null)
      {
        _context.Transaction.Remove(transaction);
        _context.SaveChanges();
        return await Task.FromResult(Json(new { isValid = true, redirectTo = Url.Action(nameof(Index)) }));
      }
      else
      {
        return await Task.FromResult(Json(new { isValid = false, errors = "Não foi possível deletar o registro solicitado!" }));
      }
    }

    [HttpGet]
    public IActionResult Search(string searchTerm)
    {
      IQueryable<Transaction> transactions = _context.Transaction;

      if (!string.IsNullOrEmpty(searchTerm))
      {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        transactions = transactions.Where(t => t.Description.Contains(searchTerm) && t.UserId == userId);
      }

      return PartialView("_TransactionsPartial", transactions);
    }
  }
}
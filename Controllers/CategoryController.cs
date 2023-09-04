using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGF.Models.Data;
using SGF.Models.Entities;

namespace SGF.Controllers
{
  [Authorize]
  public class CategoryController : Controller
  {
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      return View(await _context.Category.Where(c => c.UserId == userId).ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
      if(ModelState.IsValid)
      {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        category.UserId = userId;
        _context.Add(category);
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
      Category category = await _context.Category.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);
      
      if(category == null)
      {
          return NotFound();
      }

      return await Task.FromResult(Json(category));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Category category)
    {
      if(ModelState.IsValid)
      {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Category editCategory = await _context.Category.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == category.Id);
        
        if(editCategory != null)
        {
          editCategory.Name = category.Name;
          editCategory.Description = category.Description;

          await _context.SaveChangesAsync();
          return await Task.FromResult(Json(new { isValid = true, redirectTo = Url.Action(nameof(Index)) }));
        }
        
        return await Task.FromResult(Json(new { isValid = false, errors = "Categoria não encontrada!" }));
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
      Category category = await _context.Category.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == id);

      if(category != null)
      {
        _context.Category.Remove(category);
        _context.SaveChanges();
        return await Task.FromResult(Json(new { isValid = true, redirectTo = Url.Action(nameof(Index)) }));
      }
      else
      {
        return await Task.FromResult(Json(new { isValid = false, errors = "Não foi possível deletar o registro solicitado!" }));
      }
    }
  }
}
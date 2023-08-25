using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGF.Models;

namespace SGF.Controllers
{
  public class AccountController : Controller
  {
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      this.userManager = userManager;
      this.signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      if(ModelState.IsValid)
      {
        var user = new IdentityUser
        {
          UserName = model.Email,
          Email = model.Email
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if(result.Succeeded)
        {
          await signInManager.SignInAsync(user, isPersistent: false);
          return RedirectToAction("Index", "home");
        }

        foreach(var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }
      return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
      if(ModelState.IsValid)
      {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

        if(result.Succeeded)
        {
          return RedirectToAction("index", "home");
        }

        ModelState.AddModelError(string.Empty, "Login Inválido");
      }

      return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
      await signInManager.SignOutAsync();
      return RedirectToAction("index", "home");
    }
  }
}
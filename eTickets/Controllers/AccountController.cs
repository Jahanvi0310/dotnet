using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using eTickets.Models;
using eTickets.Data.ViewModels;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // GET: /Account/Register
    public IActionResult Register()
    {
        return View();
    }

    // POST: /Account/Register
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
 if (model.Password == null)
    {
        ModelState.AddModelError("", "Invalid password.");
        return View(model);
    }
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Movies");
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    // GET: /Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST: /Account/Login
    // POST: /Account/Login
[HttpPost]
public async Task<IActionResult> Login(LoginViewModel model, string?returnUrl)
{
    if (!ModelState.IsValid)
{
    Console.WriteLine("Model state is invalid. Errors:");
    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
        Console.WriteLine(error.ErrorMessage);
    }
    return View(model);
}
Console.WriteLine(model.Email+"email",model.Password+"password");
    if (model.Email == null || model.Password == null)
    {
        ModelState.AddModelError("", "Invalid login attempt.");
        return View(model);
    }

    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

    if (result.Succeeded)
    {
        return RedirectToAction("Index", "Movies");
    }
    else
    {
        ModelState.AddModelError("", "Invalid login attempt.");
        return View(model);
    }
}


    // POST: /Account/Logout
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}

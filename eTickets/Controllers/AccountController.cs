using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using eTickets.Models;
using eTickets.Data.ViewModels;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
private readonly byte[] _key;
    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
     _key = new byte[32]; // 32 bytes = 256 bits
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(_key);
        }
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
// POST: /Account/Login
[HttpPost]
// POST: /Account/Login
[HttpPost]
public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
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

    Console.WriteLine(model.Email + "email", model.Password + "password");
    if (model.Email == null || model.Password == null)
    {
        ModelState.AddModelError("", "Invalid login attempt.");
        return View(model);
    }

    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

    if (result.Succeeded)
    {
        // Generate JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, model.Email) // Assuming the email is the username
                // Add additional claims as needed
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        Console.WriteLine("Generated token: " + tokenString);
        // Return the token as JSON
        return RedirectToAction("Index","Movies");
    }
    else
    {
        ModelState.AddModelError("", "Invalid login attempt.");
        return View(model);
    }
    
    // Return a default response in case no other return statement is reached
    // Or you can redirect to another action or return an appropriate response
}



private string GenerateJwtToken(ApplicationUser user)
{
    // Add any additional claims as needed
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email!),
        // Add more claims as needed
    };

    // Create the token
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(2), // Set the token expiration as needed
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return tokenHandler.WriteToken(token);
}


    // POST: /Account/Logout
   [HttpPost]
public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    TempData["LogoutMessage"] = "You have been logged out successfully.";

    return RedirectToAction("Login", "Authentication");
}

}

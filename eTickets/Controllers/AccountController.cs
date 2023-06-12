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
using eTickets.Data.Static;

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
    public IActionResult Register()=>View(new RegisterViewModel());
  

    // POST: /Account/Register
//     [HttpPost]
//     public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
// {
//     if (!ModelState.IsValid)
//         return View(registerViewModel);

//     var user = await _userManager.FindByEmailAsync(registerViewModel.Email!);
//     if (user != null)
//     {
//         TempData["Error"] = "This Email address is already in use";
//         return View(registerViewModel);
//     }

//     var newUser = new ApplicationUser()
//     {
//         FullName = registerViewModel.FullName,
//         Email = registerViewModel.Email,
//         UserName = registerViewModel.Email
//     };

//     var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password!);
//     if (newUserResponse.Succeeded)
//     {
//         await _userManager.AddToRoleAsync(newUser, UserRoles.User);
//         return View("RegisterCompleted");
//     }

//     // Add a return statement here if the registration is not successful
//     return View(registerViewModel);
// }
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
            return View("RegisterCompleted");
        }
        
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error.Description);
        }

        return View(model);
    }

    // GET: /Account/Login
    public IActionResult Login()=>View(new LoginViewModel());

    // POST: /Account/Login
    
[HttpPost]
public async Task<IActionResult> Login(LoginViewModel loginViewModel)
{
    if (!ModelState.IsValid)
    {
        return View(loginViewModel);
    }

    var user = await _userManager.FindByEmailAsync(loginViewModel.Email!);

    if (user != null)
    {
        var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password!);

        if (passwordCheck)
        {
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password!, false, false);

            if (result.Succeeded)
            {
                // Customize the behavior after successful login
                TempData["Message"] = "Login successful!";
                return View("Index","Movies");
            }
            else
            {
                // Log an error if the sign-in fails
                TempData["Error"] = "Failed to sign in the user.";
                Console.WriteLine("Failed to sign in the user.");
            }
        }
        else
        {
            // Log an error if the password is incorrect
            TempData["Error"] = "Wrong credentials. Please try again.";
            Console.WriteLine("Wrong credentials. Please try again.");
        }
    }
    else
    {
        // Log an error if the user is not found
        TempData["Error"] = "Wrong credentials. Please try again.";
        Console.WriteLine("User not found.");
    }

    return View(loginViewModel);
}

// public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
// {
//     if (!ModelState.IsValid)
//     {
//         Console.WriteLine("Model state is invalid. Errors:");
//         foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
//         {
//             Console.WriteLine(error.ErrorMessage);
//         }
//         return View(model);
//     }

//     Console.WriteLine(model.Email + "email", model.Password + "password");
//     if (model.Email == null || model.Password == null)
//     {
//         ModelState.AddModelError("", "Invalid login attempt.");
//         return View(model);
//     }

//     var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

//     if (result.Succeeded)
//     {
//         // Generate JWT token
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = new ClaimsIdentity(new[]
//             {
//                 new Claim(ClaimTypes.Name, model.Email) // Assuming the email is the username
//                 // Add additional claims as needed
//             }),
//             Expires = DateTime.UtcNow.AddDays(1),
//             SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
//         };
//         var token = tokenHandler.CreateToken(tokenDescriptor);
//         var tokenString = tokenHandler.WriteToken(token);
//         Console.WriteLine("Generated token: " + tokenString);
// //         Response.Cookies.Append("jwtToken", tokenString, new CookieOptions
// // {
// //     Expires = DateTime.UtcNow.AddDays(1), // Set the expiration as needed
// //     HttpOnly = true,
// //     Secure = true // Set to true if using HTTPS
// // });
//         // Return the token as JSON
//         return RedirectToAction("Index","Movies");
//     }
//     else
//     {
//         ModelState.AddModelError("", "Invalid login attempt.");
//         return View(model);
//     }
    
//     // Return a default response in case no other return statement is reached
//     // Or you can redirect to another action or return an appropriate response
// }



// private string GenerateJwtToken(ApplicationUser user)
// {
//     // Add any additional claims as needed
//     var claims = new List<Claim>
//     {
//         new Claim(JwtRegisteredClaimNames.Sub, user.Id),
//         new Claim(JwtRegisteredClaimNames.Email, user.Email!),
//         // Add more claims as needed
//     };

//     // Create the token
//     var tokenHandler = new JwtSecurityTokenHandler();
//     var tokenDescriptor = new SecurityTokenDescriptor
//     {
//         Subject = new ClaimsIdentity(claims),
//         Expires = DateTime.UtcNow.AddHours(2), // Set the token expiration as needed
//         SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
//     };
//     var token = tokenHandler.CreateToken(tokenDescriptor);

//     return tokenHandler.WriteToken(token);
// }


    // POST: /Account/Logout
   [HttpPost]
public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
   

    return RedirectToAction("Index", "Movies");
}

}

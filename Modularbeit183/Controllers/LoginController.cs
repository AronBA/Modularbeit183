using Microsoft.AspNetCore.Mvc;
using Modularbeit183.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Modularbeit183.Data;
using Modularbeit183.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
namespace Modularbeit183.Controllers;

public class LoginController : Controller
{
    private readonly DatabaseContext _context;

    public LoginController(DatabaseContext context)
    {
        _context = context;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(UserModel u)
    {
           
             
        List<UserModel> user = await _context.Users.Where(t => u.Username == u.Username && Encrypt(u.Passwort) == u.Passwort && u.Deleted == false).ToListAsync();


           
        if (!user.Any())
        {

            TempData["Message"] = "Passwort oder Benutzername sind falsch";
            return RedirectToAction("Login");
                
                
        }
      

        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, user[0].Headcoach.ToString()),
            new(ClaimTypes.Name, user[0].Benutzername),
            new(ClaimTypes.Surname, user[0].Vorname),
            new(ClaimTypes.GivenName, user[0].Nachname),
            new(ClaimTypes.NameIdentifier, user[0].Id.ToString()),

            


        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
           
           

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));





        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
           
        await HttpContext.SignOutAsync();
        TempData["Message"] = "Sie wurden erfolgreich abgemeldet";
        return RedirectToAction("Login");
    }


    public static string Encrypt(string passwort)
    {
        var crypt = new SHA256Managed();
        string hash = String.Empty;
        byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(passwort));
        foreach (byte theByte in crypto)
        {
            hash += theByte.ToString("x2");
        }
        return hash;

    }
    
}
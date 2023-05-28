using Microsoft.AspNetCore.Mvc;
using amazon.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace amazon.Controllers
{
    public class AuthController : Controller
    {
        //instanciamos conección a bd
        DbamazonContext context = new DbamazonContext();

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario us, string MantenerSession = "")
        {
            // consulta para sacar el nivel en base al usuario y contraseña


            Usuario user = context.Usuarios
                .Where(u => u.Usuario1== us.Usuario1 && u.Clave == us.Clave)
                .FirstOrDefault();


            if (user != null)
            {
                
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);


            }

            TempData["error"] = "Credenciales Incorrectas";
            return RedirectToAction("Login", "Auth");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["error"] = "";
            return RedirectToAction("Login", "Auth");
        }
    }
}
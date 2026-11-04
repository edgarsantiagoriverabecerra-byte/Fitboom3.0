using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Fitboom2._0.Models;

namespace FITBOOM.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        // Inyección de dependencias para el SignInManager
        public UsuarioController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // Vista principal del usuario protegida o pública
        public IActionResult Index()
        {
            return View();
        }

        // Método POST para el Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Usuario");
            }

            ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Nutricion()
        {
            return View("~/Views/Cliente/NutricionU.cshtml");
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace TuProyecto.Controllers
{
    public class ProgramasPagosController : Controller
    {
        public IActionResult Index()
        {
            int? idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            if (idUsuario == null) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult GuardarMembresia(int id, string nombre, string duracion, decimal precio, string beneficios)
        {
            return RedirectToAction("Index");
        }

        public IActionResult EliminarMembresia(int id)
        {
            return RedirectToAction("Index");
        }

        public IActionResult AnularPago(int id)
        {
            return RedirectToAction("Index");
        }
    }
}
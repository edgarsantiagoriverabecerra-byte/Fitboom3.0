using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FITBOOM.Models;
using FITBOOM.Data;

namespace Fitboom3._0.Controllers
{
    public class AdminController : Controller
    {
        private readonly FITBOOMContext _context;

        public AdminController(FITBOOMContext context)
        {
            _context = context;
        }

        // GET: /Admin
        public async Task<IActionResult> Index()
        {
            var adminUser = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Rol != null &&
                    (u.Rol.ToLower() == "admin" || u.Rol.ToLower() == "administrador" || u.Rol.ToLower().Contains("admin")));

            if (adminUser == null)
            {
                adminUser = await _context.Usuarios.FirstOrDefaultAsync();
            }

            ViewBag.NombreAdmin = adminUser != null ? adminUser.Nombre : "Admin Central";
            ViewBag.TotalUsuarios = await _context.Usuarios.CountAsync();
            ViewBag.MembresiasList = await _context.Membresia.ToListAsync();

            var staff = await _context.Usuarios
                .Where(u => u.Rol != null &&
                    (u.Rol.ToLower().Contains("entrenador") ||
                     u.Rol.ToLower().Contains("trainer") ||
                     u.Rol.ToLower().Contains("nutricionista") ||
                     u.Rol.ToLower().Contains("staff")))
                .ToListAsync();

            ViewBag.StaffList = staff;

            var todosLosUsuarios = await _context.Usuarios.ToListAsync();
            return View(todosLosUsuarios);
        }

        // GET: /Admin/Usuarios
        public async Task<IActionResult> Usuarios()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return View(usuarios);
        }

        // GET: /Admin/Membresias
        public async Task<IActionResult> Membresias()
        {
            var membresias = await _context.Membresia.ToListAsync();
            return View(membresias);
        }

        // GET: /Admin/Alimentos
        [HttpGet]
        public IActionResult Alimentos()
        {
            // Carga la vista del admin desde Views/Admin/Alimentos.cshtml
            return View();
        }

        // GET: /Admin/Rutinas
        public IActionResult Rutinas()
        {
            return View();
        }

        // GET: /Admin/Profesionales
        public async Task<IActionResult> Profesionales()
        {
            var profesionales = await _context.Usuarios
                .Where(u => u.Rol != null &&
                    (u.Rol.ToLower().Contains("entrenador") ||
                     u.Rol.ToLower().Contains("trainer") ||
                     u.Rol.ToLower().Contains("nutricionista") ||
                     u.Rol.ToLower().Contains("profesional")))
                .ToListAsync();

            return View(profesionales);
        }

        // GET: /Admin/Perfil
        [HttpGet]
        public async Task<IActionResult> Perfil()
        {
            var email = HttpContext.Session.GetString("UsuarioEmail") ?? "SantiagoR@gmail.com";

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == email);

            if (usuario == null)
            {
                usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Rol != null && u.Rol.ToLower().Contains("admin"));
            }

            if (usuario == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }

        // POST: /Admin/Perfil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Perfil(Usuario model, string FotoUrl)
        {
            var usuarioDB = await _context.Usuarios.FindAsync(model.IdUsuario);

            if (usuarioDB != null)
            {
                // Actualizar datos personales
                usuarioDB.Nombre = model.Nombre;
                usuarioDB.Apellido = model.Apellido;
                usuarioDB.Correo = model.Correo;

                // Si viene un rol especificado en el formulario, se actualiza; de lo contrario se conserva
                if (!string.IsNullOrWhiteSpace(model.Rol))
                {
                    usuarioDB.Rol = model.Rol;
                }

                _context.Update(usuarioDB);
                await _context.SaveChangesAsync();

                // Actualizar variables de sesión (incluyendo la foto de perfil manual)
                HttpContext.Session.SetString("UsuarioNombre", usuarioDB.Nombre);
                HttpContext.Session.SetString("UsuarioEmail", usuarioDB.Correo);

                if (!string.IsNullOrWhiteSpace(FotoUrl))
                {
                    HttpContext.Session.SetString("UsuarioFoto", FotoUrl);
                }

                // Registrar evento en la campanita de notificaciones
                var hora = DateTime.Now.ToString("HH:mm");
                HttpContext.Session.SetString("UltimaNotificacion", $"Cambio realizado: Perfil de {usuarioDB.Nombre} actualizado a las {hora}");
                HttpContext.Session.SetString("TieneNotificacion", "true");

                TempData["SuccessMessage"] = "Perfil, rol y foto actualizados correctamente.";
                return RedirectToAction(nameof(Perfil));
            }

            return View(model);
        }

        // GET: /Admin/Configuracion
        [HttpGet]
        public IActionResult Configuracion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Configuración()
        {
            // Aquí puedes pasar opcionalmente datos del usuario actual si los necesitas en la vista
            return View();
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Fitboom3._0.Controllers
{
    public class ClienteController : Controller
    {
        // Cadena de conexión directa a SQLEXPRESS
        private readonly string cadenaConexion = @"Server=.\SQLEXPRESS;Database=FITBOOM;Trusted_Connection=True;TrustServerCertificate=True;";

        // GET: ClienteController/Index (Vista principal / Nutrición del cliente)
        public ActionResult Index()
        {
            int? idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            if (idUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CargarDatosUsuarioSesion(idUsuario.Value);

            return View("~/Views/Cliente/Index.cshtml");
        }

        // ==========================================
        // VISTA: RUTINAS DE ENTRENAMIENTO
        // ==========================================
        public ActionResult Rutinas()
        {
            int? idUsuario = HttpContext.Session.GetInt32("UsuarioId");
            if (idUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }

            CargarDatosUsuarioSesion(idUsuario.Value);

            // Ruta explícita asegurando que cargue la vista de rutinas correctamente
            return View("~/Views/Cliente/Rutinas.cshtml");
        }

        // Método auxiliar privado para consultar los datos del usuario en la BD de forma segura
        private void CargarDatosUsuarioSesion(int idUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // CONSULTA SEGURA: Solo consultamos columnas existentes para evitar errores de columnas inválidas
                string query = "SELECT Nombre, Rol, FotoRuta FROM Usuarios WHERE IdUsuario = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Id", idUsuario);
                    conexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            HttpContext.Session.SetString("UsuarioNombre", reader["Nombre"].ToString());
                            string rol = reader["Rol"].ToString();
                            HttpContext.Session.SetString("UsuarioRol", rol);
                            HttpContext.Session.SetString("UsuarioFoto", reader["FotoRuta"] != DBNull.Value ? reader["FotoRuta"].ToString() : "/images/imagen predeterminada.jpg");

                            // Asignamos un plan por defecto de forma segura ya que la columna no está en la base de datos
                            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioPlan")))
                            {
                                HttpContext.Session.SetString("UsuarioPlan", "PRO");
                            }
                        }
                    }
                }
            }
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
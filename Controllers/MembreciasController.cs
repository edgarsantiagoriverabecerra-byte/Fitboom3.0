using Microsoft.AspNetCore.Mvc;

public class MembresiasController : Controller
{
    // Vista principal con la tabla
    public IActionResult Index()
    {
        return View();
    }

    // Redirige o muestra la interfaz para crear un plan nuevo
    [HttpGet]
    public IActionResult Crear()
    {
        return View(); // Renderiza la vista Crear.cshtml
    }

    [HttpPost]
    public IActionResult Crear(string nombre, string precio, string estado)
    {
        // Lógica para guardar en SQL Server...
        return RedirectToAction("Index");
    }

    // Redirige o muestra la interfaz para editar un plan existente según su ID
    [HttpGet]
    public IActionResult Editar(int id)
    {
        // Lógica para buscar el plan en SQL Server usando el 'id'...
        return View(); // Renderiza la vista Editar.cshtml
    }

    [HttpPost]
    public IActionResult Editar(int id, string nombre, string precio, string estado)
    {
        // Lógica para actualizar en SQL Server...
        return RedirectToAction("Index");
    }
}
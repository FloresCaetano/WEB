using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class MedicamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public int numeroEntero()
        {
            return 10;
        }

        public string saludar()
        {
            return "hola mi hermano";
        }

        public string saludosNombre(string nombre)
        {
            return "Bienvenido " + nombre;
        }

        public string saludosNombreCompleto(string nombre, string apellido)
        {
            return "Bienvenido " + nombre + " " + apellido;
        }
    }
}

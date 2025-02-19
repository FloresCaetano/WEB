using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class LaboratorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<LaboratorioCLS> filtrarLaboratorio(LaboratorioCLS objLaboratorio)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.FiltrarLaboratorios(objLaboratorio.nombre, objLaboratorio.direccion, objLaboratorio.personaContacto);
        }
    }
}

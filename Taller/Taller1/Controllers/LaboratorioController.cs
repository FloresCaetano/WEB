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

        public LaboratorioCLS recuperarLaboratorio(int id)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.recuperarLaboratorios(id);
        }

        public int guardarLaboratorio(LaboratorioCLS objLaboratorio)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.GuardarLaboratorios(objLaboratorio);
        }

        public int eliminarLaboratorio(int id)
        {
            LaboratorioDAL obj = new LaboratorioDAL();
            return obj.eliminarLaboratorios(id);
        }
    }
}

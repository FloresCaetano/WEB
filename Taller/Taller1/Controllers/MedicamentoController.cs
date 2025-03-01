using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class MedicamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<MedicamentoCLS> filtrarMedicamento(MedicamentoCLS medicamentoCLS)
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.FiltrarMedicamento(medicamentoCLS);
        }

        public MedicamentoCLS recuperarMedicamento(int id)
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.recuperarMedicamento(id);
        }

        public int guardarMedicamento(MedicamentoCLS medicamentoCLS)
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.guardarMedicamento(medicamentoCLS);
        }

        public int eliminarMedicamento(int id)
        {
            MedicamentoDAL obj = new MedicamentoDAL();
            return obj.eliminarMedicamento(id);
        }

    }
}

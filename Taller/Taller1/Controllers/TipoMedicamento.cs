using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class TipoMedicamento : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }

        public IActionResult SinMenu()
        {
            return View();
        }

        public List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            return TipoMedicamentoBL.listarTipoMedicamento();
        }

        public List<TipoMedicamentoCLS> filtrarTipoMedicamento(string nombre)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.FiltrarTipoMedicamentos(nombre);
        }

    }
}

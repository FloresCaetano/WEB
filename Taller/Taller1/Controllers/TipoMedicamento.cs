using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class TipoMedicamento : Controller
    {
        public IActionResult Index()
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

        public int guardarTipoMedicamento(TipoMedicamentoCLS tipoMedicamentoCLS)
        {
            TipoMedicamentoDAL tipoMedicamentoDAL = new TipoMedicamentoDAL();
            return tipoMedicamentoDAL.guardarTipoMedicamento(tipoMedicamentoCLS);
        }

        public TipoMedicamentoCLS recuperarTipoMedicamento(int idTipoMedicamento)
        {
            TipoMedicamentoDAL tipoMedicamentoDAL = new TipoMedicamentoDAL();
            return tipoMedicamentoDAL.recuperarTipoMedicamentos(idTipoMedicamento);
        }

        public int eliminarTipoMedicamento(int id)
        {
            TipoMedicamentoDAL tipoMedicamentoDAL = new TipoMedicamentoDAL();
            return tipoMedicamentoDAL.eliminarTipoMedicamento(id);
        }

    }
}

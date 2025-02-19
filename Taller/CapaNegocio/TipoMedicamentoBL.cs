using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class TipoMedicamentoBL
    {
        public static List<TipoMedicamentoCLS> listarTipoMedicamento()
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.ListarTipoMedicamento();
        }

        public List<TipoMedicamentoCLS> filtrarTipoMedicamento(string nombre)
        {
            TipoMedicamentoDAL obj = new TipoMedicamentoDAL();
            return obj.FiltrarTipoMedicamentos(nombre);
        }

    }
}

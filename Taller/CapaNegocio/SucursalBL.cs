using CapaEntidad;
using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class SucursalBL
    {
        public List<SucursalCLS> listarSucursal()
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.ListarSucursales();

        }

        public List<SucursalCLS> filtrarSucursal(SucursalCLS objSucursal)
        {
            SucursalDAL obj = new SucursalDAL();
            return obj.FiltrarSucursales(objSucursal);
        }
    }
}

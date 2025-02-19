﻿using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;

namespace Taller1.Controllers
{
    public class Sucursal : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

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

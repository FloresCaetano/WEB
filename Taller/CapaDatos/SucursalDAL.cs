using CapaEntidad;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class SucursalDAL : CadenaDAL
    {
        public List<SucursalCLS> ListarSucursales()
        {
            List<SucursalCLS> listaSucursales = null;

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspListarSucursal", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            SucursalCLS oSucursales;
                            listaSucursales = new List<SucursalCLS>();
                            while (dr.Read())
                            {
                                oSucursales = new SucursalCLS();
                                oSucursales.idSucursal = dr.GetInt32(0);
                                oSucursales.nombre = dr.GetString(1);
                                oSucursales.direccion = dr.GetString(2);

                                listaSucursales.Add(oSucursales);

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listaSucursales = null;
                    throw;
                }
            }

            return listaSucursales;

        }

        public List<SucursalCLS> FiltrarSucursales(SucursalCLS objSucur)
        {
            List<SucursalCLS> listaSucursales = null;

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspFiltrarSucursal", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (string.IsNullOrEmpty(objSucur.nombre))
                        {
                            objSucur.nombre = "";
                        }
                        cmd.Parameters.AddWithValue("@nombresucursal", objSucur.nombre == null ? "" : objSucur.nombre);
                        cmd.Parameters.AddWithValue("@direccion", objSucur.direccion == null ? "" : objSucur.direccion);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            SucursalCLS oSucursales;
                            listaSucursales = new List<SucursalCLS>();
                            while (dr.Read())
                            {
                                oSucursales = new SucursalCLS();
                                oSucursales.idSucursal = dr.GetInt32(0);
                                oSucursales.nombre = dr.GetString(1);
                                oSucursales.direccion = dr.GetString(2);

                                listaSucursales.Add(oSucursales);

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listaSucursales = null;
                    throw;
                }
            }

            return listaSucursales;

        }
    }
}

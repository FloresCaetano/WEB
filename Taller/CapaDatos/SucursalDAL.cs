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

        public int GuardarSucursal(SucursalCLS sucursalCLS)
        {
            List<LaboratorioCLS> listaLaboratorios = null;

            int id = sucursalCLS.idSucursal;
            string nombre = sucursalCLS.nombre;
            string direccion = sucursalCLS.direccion;

            if (nombre == null || direccion == null)
            {
                return 0;
            }

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspGuardarSucursal", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@iidsucursal", id);
                        cmd.Parameters.AddWithValue("@nombre", nombre == null ? "" : nombre);
                        cmd.Parameters.AddWithValue("@direccion", direccion == null ? "" : direccion);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    return 0;
                    throw;
                }
            }

        }


        public SucursalCLS recuperarSucursal(int id)
        {
            SucursalCLS sucursal = new SucursalCLS();
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("" +
                        "SELECT IIDSUCURSAL, NOMBRE, DIRECCION FROM Sucursal WHERE IIDSUCURSAL = @id;", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                sucursal.idSucursal = dr.GetInt32(0);
                                sucursal.nombre = dr.GetString(1);
                                sucursal.direccion = dr.GetString(2);

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    sucursal = null;
                    throw;
                }

                return sucursal;
            }

        }

        public int eliminarSucursal(int id)
        {
            LaboratorioCLS listaLaboratorios = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("" +
                        "DELETE FROM Sucursal WHERE IIDSUCURSAL = @id;", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;

                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    return 0;
                    throw;
                }
            }

        }

    }
}

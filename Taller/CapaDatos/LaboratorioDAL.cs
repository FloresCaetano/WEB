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
    public class LaboratorioDAL : CadenaDAL
    {
        public List<LaboratorioCLS> FiltrarLaboratorios(string nombre, string direccion, string contacto)
        {
            List<LaboratorioCLS> listaLaboratorios = null;

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspFiltrarLaboratorio", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (string.IsNullOrEmpty(nombre))
                        {
                            nombre = "";
                        }
                        cmd.Parameters.AddWithValue("@nombre", nombre == null ? "" : nombre);
                        cmd.Parameters.AddWithValue("@direccion", direccion == null ? "" : direccion);
                        cmd.Parameters.AddWithValue("@personacontacto", contacto == null ? "" : contacto);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            LaboratorioCLS oLaboratorios;
                            listaLaboratorios = new List<LaboratorioCLS>();
                            while (dr.Read())
                            {
                                oLaboratorios = new LaboratorioCLS();
                                oLaboratorios.idLaboratorio = dr.GetInt32(0);
                                oLaboratorios.nombre = dr.GetString(1);
                                oLaboratorios.direccion = dr.GetString(2);
                                oLaboratorios.personaContacto = dr.GetString(3);

                                listaLaboratorios.Add(oLaboratorios);

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listaLaboratorios = null;
                    throw;
                }
            }

            return listaLaboratorios;

        }
        public int GuardarLaboratorios(LaboratorioCLS laboratorioCLS)
        {
            List<LaboratorioCLS> listaLaboratorios = null;

            int id = laboratorioCLS.idLaboratorio;
            string nombre = laboratorioCLS.nombre;
            string direccion = laboratorioCLS.direccion;
            string contacto = laboratorioCLS.personaContacto;
            string ncontacto = laboratorioCLS.numeroContacto;

            if(nombre == null || direccion == null || contacto == null)
            {
                return 0;
            }

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspGuardarLaboratorio", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idlaboratorio", id);
                        cmd.Parameters.AddWithValue("@nombre", nombre == null ? "" : nombre);
                        cmd.Parameters.AddWithValue("@direccion", direccion == null ? "" : direccion);
                        cmd.Parameters.AddWithValue("@personacontacto", contacto == null ? "" : contacto);
                        cmd.Parameters.AddWithValue("@numerocontacto", ncontacto == null ? "" : ncontacto);

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


        public LaboratorioCLS recuperarLaboratorios(int id)
        {
            LaboratorioCLS listaLaboratorios = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("" +
                        "SELECT IIDLABORATORIO, NOMBRE, DIRECCION, PERSONACONTACTO, NUMEROCONTACTO FROM Laboratorio WHERE IIDLABORATORIO = @id;", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@id", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            LaboratorioCLS oLaboratorios;
                            while (dr.Read())
                            {
                                oLaboratorios = new LaboratorioCLS();
                                oLaboratorios.idLaboratorio = dr.GetInt32(0);
                                oLaboratorios.nombre = dr.GetString(1);
                                oLaboratorios.direccion = dr.GetString(2);
                                oLaboratorios.personaContacto = dr.GetString(3);

                                listaLaboratorios = oLaboratorios;

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listaLaboratorios = null;
                    throw;
                }

                return listaLaboratorios;
            }

        }

        public int eliminarLaboratorios(int id)
        {
            LaboratorioCLS listaLaboratorios = null;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("" +
                        "UPDATE Laboratorio set BHABILITADO = 0 Where IIDLABORATORIO = @id;", cn))
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

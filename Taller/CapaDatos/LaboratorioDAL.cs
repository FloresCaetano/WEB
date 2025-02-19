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
    }
}

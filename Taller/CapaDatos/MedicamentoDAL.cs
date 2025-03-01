using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class MedicamentoDAL : CadenaDAL
    {
        public List<MedicamentoCLS> FiltrarMedicamento(MedicamentoCLS medicamentoCLS)
        {
            List<MedicamentoCLS> listaMedicamentos = null;

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspFiltrarMedicamento", cn))
                    {
                        int id = medicamentoCLS.idMedicamento;
                        string nombre = medicamentoCLS.nombre;
                        int idTipo = medicamentoCLS.idTipoMed;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idmedicamento", id);
                        cmd.Parameters.AddWithValue("@nombre", nombre == null ? "" : nombre);
                        cmd.Parameters.AddWithValue("@idlaboratorio", 0);
                        cmd.Parameters.AddWithValue("@idtipomedicamento", idTipo);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            MedicamentoCLS oLaboratorios;
                            listaMedicamentos = new List<MedicamentoCLS>();
                            while (dr.Read())
                            {
                                oLaboratorios = new MedicamentoCLS();
                                oLaboratorios.idMedicamento = dr.GetInt32(0);
                                oLaboratorios.nombre = dr.GetString(1);
                                oLaboratorios.nombreLaboratorio = dr.GetString(2);
                                oLaboratorios.nombreTipoMedicamento = dr.GetString(3);

                                listaMedicamentos.Add(oLaboratorios);

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listaMedicamentos = null;
                    throw;
                }
            }

            return listaMedicamentos;

        }
        public int guardarMedicamento(MedicamentoCLS medicamentoCLS)
        {

            int id = medicamentoCLS.idMedicamento;
            string codigo = medicamentoCLS.codigo;
            string nombre = medicamentoCLS.nombre;
            int idLab = medicamentoCLS.idLab;
            int idTipo = medicamentoCLS.idTipoMed;
            string uso = medicamentoCLS.uso;
            string contenido = medicamentoCLS.contenido;

            if (nombre == null || codigo == null || uso == null || contenido == null)
            {
                return -1; // -1 indica un error de campos vacios
            }


            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspGuardarMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@iidmedicamento", id);
                        cmd.Parameters.AddWithValue("@codigo", codigo == null ? "" : codigo);
                        cmd.Parameters.AddWithValue("@nombremedicamento", nombre == null ? "" : nombre);
                        cmd.Parameters.AddWithValue("@iidlaboratorio", idLab);
                        cmd.Parameters.AddWithValue("@iidtipomedicamento", idTipo);
                        cmd.Parameters.AddWithValue("@usomedicamento", uso == null ? "" : uso);
                        cmd.Parameters.AddWithValue("@contenido", contenido == null ? "" : contenido);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected; // Si regresa 0 indica un error en las claves foraneas
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


        public MedicamentoCLS recuperarMedicamento(int id)
        {
            MedicamentoCLS medicamentoCLS = new MedicamentoCLS(); ;
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspRecuperarMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idmedicamento", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                medicamentoCLS.idMedicamento = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                medicamentoCLS.codigo = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                medicamentoCLS.nombre = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                medicamentoCLS.idLab = dr.IsDBNull(3) ? 0 : dr.GetInt32(3);
                                medicamentoCLS.idTipoMed = dr.IsDBNull(4) ? 0 : dr.GetInt32(4);
                                medicamentoCLS.uso = dr.IsDBNull(5) ? "" : dr.GetString(5);
                                medicamentoCLS.contenido = dr.IsDBNull(6) ? "" : dr.GetString(6);

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    medicamentoCLS = null;
                    throw;
                }

                return medicamentoCLS;
            }

        }

        public int eliminarMedicamento(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("" +
                        "DELETE FROM Medicamento WHERE IIDMEDICAMENTO = @id;", cn))
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

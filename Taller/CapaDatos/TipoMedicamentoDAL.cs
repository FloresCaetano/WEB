using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using CapaEntidad;
using Microsoft.Extensions.Configuration;

namespace CapaDatos
{
    public class TipoMedicamentoDAL : CadenaDAL
    {
        public List<TipoMedicamentoCLS> ListarTipoMedicamento()
        {
            List<TipoMedicamentoCLS> listaTipoMedicamento = null;

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            IConfigurationRoot root = builder.Build();
            String cadenaDato = root.GetConnectionString("cn");

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspListarTipoMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataReader dr = cmd.ExecuteReader();

                        if(dr != null)
                        {
                            TipoMedicamentoCLS otipoMedicamento;
                            listaTipoMedicamento = new List<TipoMedicamentoCLS>();
                            while(dr.Read())
                            {
                                otipoMedicamento = new TipoMedicamentoCLS();
                                otipoMedicamento.idTipoMedicamento = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                otipoMedicamento.nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                otipoMedicamento.descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2);
                                listaTipoMedicamento.Add(otipoMedicamento);
                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    listaTipoMedicamento = null;
                    throw;
                }
            }
           
            return listaTipoMedicamento;

        }

        public List<TipoMedicamentoCLS> FiltrarTipoMedicamentos(string nombre)
        {
            List<TipoMedicamentoCLS> listaSucursales = null;

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspFiltrarTipoMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombretipomedicamento", nombre == null ? "" : nombre);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            TipoMedicamentoCLS oTipoMedicamento;
                            listaSucursales = new List<TipoMedicamentoCLS>();
                            while (dr.Read())
                            {
                                oTipoMedicamento = new TipoMedicamentoCLS();
                                oTipoMedicamento.idTipoMedicamento = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                oTipoMedicamento.nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                oTipoMedicamento.descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2);

                                listaSucursales.Add(oTipoMedicamento);

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

        public int guardarTipoMedicamento(TipoMedicamentoCLS objTipoMedicamento)
        {

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("uspGuardarTipoMedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        String nombre = objTipoMedicamento.nombre;
                        String descripcion = objTipoMedicamento.descripcion;
                        int id = objTipoMedicamento.idTipoMedicamento;

                        if (nombre == null || descripcion == null)
                        {
                            return 0;
                        }

                        cmd.Parameters.AddWithValue("@nombre", nombre == null ? "" : nombre);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@descripcion", descripcion == null ? "" : descripcion);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected;

                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    throw;
                }
            }
        }

        public TipoMedicamentoCLS recuperarTipoMedicamentos(int id)
        {
            TipoMedicamentoCLS tipoMed = null;

            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("" +
                        "SELECT IIDTIPOMEDICAMENTO, " +
                        "NOMBRE, DESCRIPCION FROM TipoMedicamento WHERE BHABILITADO = 1 " +
                        "and IIDTIPOMEDICAMENTO = @iidtipomedicamento", cn))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.AddWithValue("@iidtipomedicamento", id);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr != null)
                        {
                            tipoMed = new TipoMedicamentoCLS();
                            while (dr.Read())
                            {
                                tipoMed = new TipoMedicamentoCLS();
                                tipoMed.idTipoMedicamento = dr.IsDBNull(0) ? 0 : dr.GetInt32(0);
                                tipoMed.nombre = dr.IsDBNull(1) ? "" : dr.GetString(1);
                                tipoMed.descripcion = dr.IsDBNull(2) ? "" : dr.GetString(2);

                            }

                        }
                    }
                }
                catch (Exception)
                {
                    cn.Close();
                    tipoMed = null;
                    throw;
                }
            }

            return tipoMed;

        }

        public int eliminarTipoMedicamento(int id)
        {
            using (SqlConnection cn = new SqlConnection(cadenaDato))
            {
                try
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("" +
						"DELETE FROM TipoMedicamento WHERE IIDTIPOMEDICAMENTO = @id;", cn))
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

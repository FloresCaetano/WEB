using System.Data.SqlClient;
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
                        if (string.IsNullOrEmpty(nombre))
                        {
                            nombre = "";
                        }
                        cmd.Parameters.AddWithValue("@nombretipomedicamento", nombre);

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

    }
}

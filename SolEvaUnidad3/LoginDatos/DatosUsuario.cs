using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LoginDatos
{

    // Clase que representa los datos de un usuario en la capa de datos
    public class DatosUsuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }


    public class AccesoDatos
    {
        private string connectionString = "Data Source=ALITA23\\SQLEXPRESS;Initial Catalog=SolEvaUnidad3;Integrated Security=True";

        public DatosUsuario ObtenerDatosUsuario(string username)
        {
            string query = "SELECT Id, Username, Password FROM Usuarios WHERE Username = @Username";
            DatosUsuario datosUsuario = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            datosUsuario = new DatosUsuario
                            {
                                Id = (int)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"]
                            };
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al obtener datos de usuario: " + ex.Message);
                    }
                }
            }

            return datosUsuario;
        }
    }

}

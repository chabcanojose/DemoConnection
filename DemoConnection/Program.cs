using System;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace DemoConnection
{
    class Program
    {
        static void Main(string[] args)
        {
           

            try
            {
                string connString = "Data Source=LAPTOP-TVTGUQSN\\SQLEXPRESS02;Initial Catalog=Book;" + "Integrated Security = True";
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    Console.WriteLine(GetConnectionInformation(connection));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Se encontró el sigiuente error al intentar establecer la conexion:\n{0}", ex.Message);
            }
            //InsertarAutor();
            //InsertarAutor("Roger", "Pressman");
            //GetAutoresRegistrados();
            //EliminarAutor(4);
            //GetAutoresRegistrados();
            //ModificarAutoresRegistrados(7, "Mike", "Bell");
            ConsultarAutores();
        }

        static string GetConnectionInformation(SqlConnection conn)
        {
            StringBuilder s = new StringBuilder(1024);
            s.AppendLine("Cadena de Conexion: " + conn.ConnectionString);
            s.AppendLine("Estado: " + conn.State.ToString());
            s.AppendLine("Tiempo de espera de la conexion: " + conn.ConnectionTimeout.ToString());
            s.AppendLine("Base de datos: " + conn.Database);
            s.AppendLine("Fuente de datos" + conn.DataSource);
            s.AppendLine("Version del servidor: " + conn.ServerVersion);
            s.AppendLine("Id de la estacion de trabajo" + conn.WorkstationId);
            return s.ToString();
        }

        static int InsertarAutor(string firstName, string lastName)
        {
            int renglonesAfectados = 0;
            string connString = "Data Source = LAPTOP - TVTGUQSN\\SQLEXPRESS02; Initial Catalog = Books; ";
            connString += "Integrated Security= True";
            string sqlString = "INSERT INTO Authors(FirstName, LastName)";
            sqlString += "VALUES('Harvey', 'Deitel')";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlString, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@FirstName", firstName));
                        command.Parameters.Add(new SqlParameter("@LasttName", lastName));
                        renglonesAfectados = command.ExecuteNonQuery();
                        Console.WriteLine("Comandos ejecutados {0}", command.CommandText);
                        Console.WriteLine("Renglones afectados {0}", renglonesAfectados);
                    }
                } 
            }
            catch(Exception ex)
            {
                Console.WriteLine("Se encontro el siguiente error al intentar establecer la conexion:\n {0}", ex.Message);
            }
            return renglonesAfectados;           
        }

        static int GetAutoresRegistrados()
        {
            int renglonesAfectados = 0;
            string connString = "Data Source = LAPTOP - TVTGUQSN\\SQLEXPRESS02; Initial Catalog = Books; ";
            connString += "Integrated Security= True";
            string sqlString = "SELECT COUNT(*) FROM Authors";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlString, connection))
                    {
                        renglonesAfectados = (int)command.ExecuteScalar();
                        Console.WriteLine("Autores registrados: {0}", renglonesAfectados);
                    }
                }                                    
            }
            catch(Exception ex)
            {
                Console.WriteLine("Se encontro el siguiente error al intentar establecer la conexion: \n{0}", ex.Message);
            }
            return renglonesAfectados;
        }

        static int EliminarAutor( int idAutor)
        {
            int renglonesAfectados = 0;
            string connString = "Data Source = LAPTOP - TVTGUQSN\\SQLEXPRESS02; Initial Catalog = Books; ";
            connString += "Integrated Security= True";
            string sqlString = "DELETE FROM Authors WHERE AuthorId = @AuthorId";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlString, connection))
                    {
                        command.CommanType = CommandType.Text;
                        command.Parameters.Add(new SqlParameters("@AuthorId", idAutor));
                        renglonesAfectados = (int)command.ExecuteScalar();
                        Console.WriteLine("Autores eliminados: {0}", renglonesAfectados);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se encontro el siguiente error al intentar establecer la conexion: \n{0}", ex.Message);
            }
            return renglonesAfectados;
        }

        static int ModificarAutoresRegistrados(int idAuthor, string firstName, string lastName)
        {
            int renglonesAfectados = 0;
            string connString = "Data Source = LAPTOP - TVTGUQSN\\SQLEXPRESS02; Initial Catalog = Books; ";
            connString += "Integrated Security= True";
            string sqlString = "UPDATE Authors";
            sqlString += "SET FirstName = @FirstName, LastName = @LastName";
            sqlString += "WHERE AuthorId = @AuthorId";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlString, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.Add(new SqlParameter("@AuthorId", idAuthor));
                        command.Parameters.Add(new SqlParameter("@FirstName", firstName));
                        command.Parameters.Add(new SqlParameter("@LastName", lastName));
                        renglonesAfectados = (int)command.ExecuteScalar();
                        Console.WriteLine("Autores modificados: {0}", renglonesAfectados);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se encontro el siguiente error al intentar establecer la conexion: \n{0}", ex.Message);
            }
            return renglonesAfectados;
        }

        static int ConsultarAutores()
        {
            int renglonesAfectados = 0;
            SqlDataReader reader = null;
            string connString = "Data Source = LAPTOP - TVTGUQSN\\SQLEXPRESS02; Initial Catalog = Books; ";
            connString += "Integrated Security= True";
            string sqlString = "SELECT * FROM Authors";            
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlString, connection))
                    {
                        command.CommandType = CommandType.Text;
                        reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                        if (reader.HasRows)
                        {


                            Console.WriteLine("Id Autor\tNombres\t\tApellidos");
                            while (reader.Read())
                            {
                                Console.WriteLine("{0}\t\t{1}\t\t{2}", reader.GetInt32(0), reader["FirstName"].ToString(), reader["LastName"].ToString());
                            }
                        }
                        else Console.WriteLine("No se encontraron renglones");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Se encontro el siguiente error al intentar establecer la conexion: \n{0}", ex.Message);
            }
            return renglonesAfectados;
        }
    }
}
 
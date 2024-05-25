using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;

namespace Ventas
{
    public class DataAccess
    {
        public const string CONNECTION_STRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\LENOVO FLEX\\Desktop\\C#\\BDSISTEMAVENTA\\BDSISTEMAVENTA\\SistemaVentas.mdf\";Integrated Security=True";
        //ADO.NET

        public const string CADENA_SQL_SERVER = "Server=DESKTOP-JNQ2MF0\\SQLEXPRESS;Integrated Security=true;Initial Catalog=SistemaVenta";
        public List<Ventas> GetAllAdoNet()
        {
            List<Ventas> Ventas1 = new List<Ventas>();
            try
            {
                SqlConnection conn = new SqlConnection(CONNECTION_STRING);
                conn.Open();
                string query = "SELECT Id, Cliente, IdProducto, Cantidad, Monto FROM Ventas";
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {

                    Ventas Ventas = new Ventas
                    {
                        Id = reader.GetInt32(0),
                        Cliente = reader.GetString(1),
                        IdProducto = reader.GetInt32(2),
                        Cantidad = reader.GetString(3),
                        Monto = reader.GetString(4),
                    };
                    Ventas1.Add(Ventas);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ventas1;


        }

        public List<Ventas> GetAllDapper()
        {
            List<Ventas> Ventas1 = new List<Ventas>();
            try
            {
                SqlConnection conn = new SqlConnection(CADENA_SQL_SERVER);
                conn.Open();
                string query = "SELECT v.Id, v.Cliente,v.IdProducto,  v.Cantidad, v.Monto, p.nombre AS Nombre FROM Ventas v JOIN  Productos p ON  v.IdProducto = p.Id";
                Ventas1 = conn.Query<Ventas>(query).ToList();
                conn.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ventas1;
        }

        public int Create(Ventas ventas)
        {
            int resultado = 0;
            try
            {
                SqlConnection conn = new SqlConnection(CADENA_SQL_SERVER);
                conn.Open();
                string query = @"INSERT INTO Ventas (Id, Cliente, IdProducto, Cantidad, Monto) 
                                VALUES (@Id, @Cliente, @IdProducto, @Cantidad, @Monto)";

                resultado = conn.Execute(query, new
                {
                    Id = ventas.Id,
                    Cliente = ventas.Cliente,
                    IdProducto = ventas.IdProducto,
                    Cantidad = ventas.Cantidad,
                    Monto = ventas.Monto
                });
                conn.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            return resultado;
        }
        public Ventas GetById(int idVentas)
        {
            Ventas Ventas1 = new Ventas();
            try
            {
                SqlConnection conn = new SqlConnection(CADENA_SQL_SERVER);
                conn.Open();
                string query = "SELECT Id, Cliente, IdProducto, Cantidad, Monto FROM Ventas WHERE id=@id";
                Ventas1 = conn.QuerySingle<Ventas>(query, new { id = idVentas });
                conn.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ventas1;
        }

        public int Update(Ventas ventas)
        {
            int resultado = 0;
            try
            {
                SqlConnection conn = new SqlConnection(CADENA_SQL_SERVER);
                conn.Open();
                string query = @"UPDATE  Ventas SET Cliente=@Cliente, IdProducto=@IdProducto, Cantidad=@Cantidad, Monto=@Monto 
                                WHERE Id = @Id";

                resultado = conn.Execute(query, new
                {
                    Id = ventas.Id,
                    Cliente = ventas.Cliente,
                    IdProducto = ventas.IdProducto,
                    Cantidad = ventas.Cantidad,
                    Monto = ventas.Monto
                });
                conn.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            return resultado;
        }

        public int Delete(int id)
        {
            int resultado = 0;
            try
            {
                SqlConnection conn = new SqlConnection(CADENA_SQL_SERVER);
                conn.Open();
                string query = @"DELETE FROM Ventas WHERE Id = @Id";

                resultado = conn.Execute(query, new
                {
                    id = id
                });
                conn.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            return resultado;
        }

        public List<Productos> GetProductos()
        {
            List<Productos> Productos1 = new List<Productos>();
            try
            {
                SqlConnection conn = new SqlConnection(CADENA_SQL_SERVER);
                conn.Open();
                string query = "SELECT Id, nombre, precio FROM Productos";
                Productos1 = conn.Query<Productos>(query).ToList();
                conn.Close();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Productos1;
        }
    }
}

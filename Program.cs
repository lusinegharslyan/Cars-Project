using Cars_project;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cars_project
{

    class Direction
    {
        public Direction(int id, string from, string to, string distance, int fixedPrice)
        {
            Id = id;
            From = from;
            To = to;
            Distance = distance;
            FixedPrice = fixedPrice;

        }

        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Distance { get; set; }
        public int FixedPrice { get; set; }

    }

    public class CarType
    {
        public CarType(int id, string name, float coefficient)
        {
            Id = id;
            Name = name;
            Coefficient = coefficient;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Coefficient { get; set; }
    }

    class Container
    {
        public Container(int id, bool isOpen, float coefficient)
        {
            Id = id;
            IsOpen = isOpen;
            Coefficient = coefficient;
        }

        public int Id { get; set; }
        public bool IsOpen { get; set; }
        public float Coefficient { get; set; }
    }



    class CrashedCar
    {
        public CrashedCar(int id, bool isChared, float coefficient)
        {
            Id = id;
            IsChared = isChared;
            Coefficient = coefficient;
        }

        public int Id { get; set; }
        public bool IsChared { get; set; }
        public float Coefficient { get; set; }

    }

    class CarBrand
    {
        public CarBrand(int id, string brand)
        {
            Id = id;
            Brand = brand;
        }

        public int Id { get; set; }
        public string Brand { get; set; }
    }

    class CarModel
    {
        public CarModel(int id, string name, CarType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public CarType Type { get; set; }
    }


    public interface ICarTypeRepository
    {
        void AddCarType(CarType carType);
        CarType FindCarType(int id);
        List<CarType> FindAllCarTypes();
        void UpdateCarType(CarType carType);
        void DeleteCarType(int id);
    }

    public class CarTypeRepository : ICarTypeRepository
    {
        public const string CONNECTCION_STRING = "Data Source=LUSINE1985\\MSSQLSERVER01;Initial Catalog=CarsDb;Integrated Security=True;Encrypt=False";

        public void AddCarType(CarType carType)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTCION_STRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "insert into CarType values(@CarTypeId,@Name,@Coefficient)";
                    command.Parameters.Add(new SqlParameter("@CarTypeId", carType.Id));
                    command.Parameters.Add(new SqlParameter("@Name", carType.Name));
                    command.Parameters.Add(new SqlParameter("@Coefficient", carType.Coefficient));
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCarType(int id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTCION_STRING))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Delete from CarType where CarTypeId=@Id";
                    command.Parameters.Add(new SqlParameter("@CarTypeId", id));
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<CarType> FindAllCarTypes()
        {
            using (SqlConnection connection = new SqlConnection(CONNECTCION_STRING))
            {
                connection.Open();
                List<CarType> carTypes = new List<CarType>();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from CarType";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CarType carType = new CarType();
                            carType.Id = int.Parse(reader["CarTypeId"].ToString());
                            carType.Name = reader["Name"].ToString();
                            carType.Coefficient = int.Parse(reader["Coefficient"].ToString());
                            carTypes.Add(carType);
                        }
                    }
                }
                return carTypes;

            }
        }

        public CarType FindCarType(int id)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTCION_STRING))
            {
                connection.Open();
                CarType carType = new CarType();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select * from CarType where CarTypeId=@Id";
                    command.Parameters.Add(new SqlParameter("@CarTypeId", id));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            carType.Id = int.Parse(reader["CarTypeId"].ToString());
                            carType.Name = reader["Name"].ToString();
                            carType.Coefficient = int.Parse(reader["Coefficient"].ToString());
                        }
                    }
                }
                return carType;
            }
        }

        public void UpdateCarType(CarType carType)
        {
            using(SqlConnection connection= new SqlConnection(CONNECTCION_STRING))
            {
                connection.Open();
                using(SqlCommand command= new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "update CarType set " +
                        " Name=@Name,Coefficient=@Coefficient where CarTypeId= @Id";

                    command.Parameters.Add(new SqlParameter("@Id", carType.Id));
                    command.Parameters.Add(new SqlParameter("@Name", carType.Name));
                    command.Parameters.Add(new SqlParameter("@Coefficient", carType.Coefficient));
                    command.ExecuteNonQuery();

                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            ICarTypeRepository carRepository = new CarTypeRepository();
            CarType carType1 = new CarType(1, "BMW", 1);
            CarType carType2 = new CarType(2, "Opel", 1);
            CarType carType3 = new CarType(3, "Nissan", 1);
            CarType carType4 = new CarType(4, "BMW", 1);
            CarType carType5 = new CarType(5, "Ford", 1);
            carRepository.AddCarType(carType1);
            carRepository.AddCarType(carType2);
            carRepository.AddCarType(carType3);
            carRepository.AddCarType(carType4);
            carRepository.AddCarType(carType5);
        }
    }
}

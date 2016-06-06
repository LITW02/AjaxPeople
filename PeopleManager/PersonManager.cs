using System.Collections.Generic;
using System.Data.SqlClient;

namespace People.Data
{
    public class PersonManager
    {
        private string _connectionString;

        public PersonManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM People";
                connection.Open();
                List<Person> people = new List<Person>();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Person p = new Person();
                    p.Id = (int)reader["Id"];
                    p.FirstName = (string)reader["FirstName"];
                    p.LastName = (string)reader["LastName"];
                    p.Age = (int)reader["Age"];
                    people.Add(p);
                }


                return people;
            }
        }

        public Person GetPerson(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM People WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                reader.Read();
                Person p = new Person();
                p.Id = (int)reader["Id"];
                p.FirstName = (string)reader["FirstName"];
                p.LastName = (string)reader["LastName"];
                p.Age = (int)reader["Age"];
                return p;
            }
        }

        public void DeletePerson(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM People WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void AddPerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO People (FirstName, LastName, Age) " +
                                      "VALUES (@firstName, @lastName, @age) SELECT @@Identity";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                person.Id = (int) (decimal) command.ExecuteScalar();
            }
        }

        public void UpdatePerson(Person person)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UPDATE People SET FirstName = @firstName, LastName = @lastName," +
                                      " Age = @age WHERE Id = @id";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                command.Parameters.AddWithValue("@id", person.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
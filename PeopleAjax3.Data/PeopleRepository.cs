using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleAjax3.Data
{
    public class PeopleRepository
    {
        private string _connString;

        public PeopleRepository(string connString)
        {
            _connString = connString;
        }

        public List<Person> GetAllPeople()
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People ORDER BY FirstName";
            conn.Open();

            var people = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }

        public void AddPerson(Person person)
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO People (FirstName, LastName, Age) VALUES (@firstName, @lastName, @age)";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void EditPerson(Person person)
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age WHERE Id = @id";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeletePerson(int id)
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

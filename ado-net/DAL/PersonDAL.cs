using ado_net.DAL.Interfaces;
using ado_net.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ado_net.DAL
{
    public class PersonDAL : IPersonDAL
    {
        private IConfiguration _configuration;
        public string connectionString { get; private set; }
        public PersonDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("Connection");
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string commandSQL = $"delete Person where Id = @id";

                SqlCommand cmd = new SqlCommand(commandSQL, con);
                cmd.CommandType = CommandType.Text;
                
                cmd.Parameters.AddWithValue("@id", id);
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<Person> GetAll()
        {
            var persons = new List<Person>();

            //Dependency - SqlClient

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var query = $"select Id, Fullname, Gender, SocialSecurityNumber, CellPhone, Email from Person";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var person = new Person();

                    person.Id = Convert.ToInt32(rdr["Id"]);
                    person.SocialSecurityNumber = rdr["SocialSecurityNumber"].ToString();
                    person.FullName = rdr["FullName"].ToString();
                    person.Email = rdr["Email"].ToString();
                    person.CellPhone = rdr["CellPhone"].ToString();

                    persons.Add(person);
                }

                con.Close();
            }
            return persons;
        }

        public void New(Person person)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var commandSql = $"insert into Person (Fullname, Gender, SocialSecurityNumber, CellPhone, Email) values (@FullName, @Gender, @SocialSecurityNumber, @CellPhone, @Email)";

                SqlCommand cmd = new SqlCommand(commandSql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@SocialSecurityNumber", person.SocialSecurityNumber);
                cmd.Parameters.AddWithValue("@Gender", person.Gender);
                cmd.Parameters.AddWithValue("@FullName", person.FullName);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@CellPhone", person.CellPhone);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void Update(Person person)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string commandSql = $"update Person set CellPhone = @CellPhone, Email = @Email where Id = @Id";

                SqlCommand cmd = new SqlCommand(commandSql, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@CellPhone", person.CellPhone);
                cmd.Parameters.AddWithValue("@Email", person.Email);
                cmd.Parameters.AddWithValue("@Id", person.Id);
                
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

    }
}

using Isi.ShoppingApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;
using Isi.Utility.Authentication;

namespace Isi.ShoppingApp.Data.Repositories
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository()
        {
           connectionString = ConfigurationManager.ConnectionStrings["ShoppingDatabase"].ConnectionString;
        }
        //Created by Hector Fonseca
        public User GetUser(long id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT Users.IdUser, Users.Username, Users.Name, Roles.IdRole, Roles.Name as Role " +
                                    "FROM Users " +
                                    "INNER JOIN Roles " +
                                    "ON Roles.IdRole = Users.FK_IdRole " +
                                    "WHERE Users.IdUser = @Id;";

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ReadNextUser(reader);
            return null;
        }
        //Created by Hector Fonseca
        public User GetUserByUsername(string username)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT Users.IdUser, Users.Username, Users.Name, Roles.IdRole, Roles.Name as Role " +
                                    "FROM Users " +
                                    "INNER JOIN Roles " +
                                    "ON Roles.IdRole = Users.FK_IdRole " +
                                    "WHERE Users.Username = @Username;";

            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ReadNextUser(reader);
            return null;
        }

        //Created by Hector Fonseca
        public User CreateUser(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Users (Username, Name, PasswordHash, PasswordSalt, FK_IdRole, Created, Updated) " +
                                    "OUTPUT inserted.IdUser " +
                                    "VALUES(@Username, @Name, @PasswordHash, @PasswordSalt, @FK_IdRole, @Created, @Updated); ";
            DateTime now = DateTime.UtcNow;
            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
            command.Parameters.Add("@PasswordHash", SqlDbType.VarBinary).Value = user.PasswordHash;
            command.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary).Value = user.PasswordSalt;
            command.Parameters.Add("@FK_IdRole", SqlDbType.BigInt).Value = user.Role.IdRole;
            command.Parameters.Add("@Created", SqlDbType.DateTime2).Value = now;
            command.Parameters.Add("@Updated", SqlDbType.DateTime2).Value = now;

            long id = (long)command.ExecuteScalar();

            return new User(id, user);
        }

        //Created by Hector Fonseca and by edited Ximena Carrillo
        public HashedPassword GetPassword(string userName)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Users.PasswordHash, Users.PasswordSalt " +
                                    "FROM Users " +
                                    "WHERE Users.Username = @Username;";
            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                byte[] storedPasswordHash = (byte[])reader[0];
                byte[] storedPasswordSalt = (byte[])reader[1];
                return new HashedPassword(storedPasswordSalt, storedPasswordHash);
            }
            return null;
        }

        //Created by Hector Fonseca
        private User ReadNextUser(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string username = reader.GetString(1);
            string name = reader.GetString(2);
            long idRole = reader.GetInt64(3);
            string nameRole = reader.GetString(4);


            return new User(id, username, name, null, null, new Role(idRole, nameRole));
        }
    }
}

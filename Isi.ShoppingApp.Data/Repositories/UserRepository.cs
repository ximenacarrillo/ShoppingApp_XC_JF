﻿using Isi.ShoppingApp.Core.Entities;
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
        public User GetUserByUsername(string username)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT Users.IdUser, Users.Username, Users.Name, Roles.IdRole, Roles.Name as Role " +
                                    "FROM Users " +
                                    "WHERE Users.Username = @Username;";

            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;


            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ReadNextUser(reader);
            return null;
        }

        public User CreateUser(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Products (Username, Name, Password, FK_IdRole, Created, Updated) " +
                                    "OUTPUT inserted.IdProduct " +
                                    "VALUES(@Username, @Name, @Password, @FK_IdRole, @Created, @Updated); ";
            DateTime now = DateTime.UtcNow;
            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = user.Name;
            command.Parameters.Add("@Password", SqlDbType.VarBinary).Value = user.Password;
            command.Parameters.Add("@FK_IdRole", SqlDbType.BigInt).Value = user.Role.IdRole;
            command.Parameters.Add("@Created", SqlDbType.DateTime2).Value = now;
            command.Parameters.Add("@Updated", SqlDbType.DateTime2).Value = now;

            long id = (long)command.ExecuteScalar();

            return new User(id, user);
        }

        public byte[] GetPassword(string userName)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Users.Password " +
                                    "FROM Users " +
                                    "WHERE Users.Username = @Username;";
            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                //string? storedPassword = reader.IsDBNull(0) ? null : reader.GetString(0);
                //if(storedPassword != null)
                //    return PasswordHasher.HashPassword(storedPassword);
                return ReadPassword(reader);
            }
            return null;
        }

        private byte[] ReadPassword(SqlDataReader reader)
        {
            byte[] password = (byte[])reader[0];
            return password;
        }

        private User ReadNextUser(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string username = reader.GetString(1);
            string name = reader.GetString(2);
            long idRole = reader.GetInt64(3);
            string nameRole = reader.GetString(4);


            return new User(id, username, name, new Role(idRole, nameRole));
        }
    }
}

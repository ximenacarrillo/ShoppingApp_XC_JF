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


        
        private User ReadNextUser(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string username = reader.GetString(1);
            string name = reader.GetString(2);
            long idRole = reader.GetInt64(3);
            string nameRole = reader.GetString(4);
            

            return new User(id, username, name, new Role(idRole,nameRole));
        }

        public HashedPassword GetPassword(string userName)
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
                string? storedPassword = reader.IsDBNull(0) ? null : reader.GetString(0);
                if(storedPassword != null)
                    return PasswordHasher.HashPassword(storedPassword);
            }
            return null;
        }
    }
}

﻿using Isi.ShoppingApp.Core.Entities;
using Microsoft.Data.SqlClient;
using System.Configuration;


namespace Isi.ShoppingApp.Data.Repositories
{
    public class CartRepository
    {
        private readonly string connectionString;

        public CartRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingDatabase"].ConnectionString;
        }

        public Cart CreateCart(Cart cart)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            //TODO
            //command.CommandText = "";
            

            return new Cart(0, cart.User);
        }
    }
}

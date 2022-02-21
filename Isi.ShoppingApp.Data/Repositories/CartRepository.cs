using Isi.ShoppingApp.Core.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

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

            command.CommandText = "INSERT INTO Carts (Discount, Subtotal, Taxes, Total, FK_IdUser, Created, Updated) " +
                                     "OUTPUT inserted.IdCart " +
                                     "VALUES(@Discount, @Subtotal, @Taxes, @Total, @FK_IdUser, @Created, @Updated); ";
            DateTime now = DateTime.UtcNow;
            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = cart.Discount;
            command.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = cart.Subtotal;
            command.Parameters.Add("@Taxes", SqlDbType.Decimal).Value = cart.Taxes;
            command.Parameters.Add("@Total", SqlDbType.Decimal).Value = cart.Total;
            command.Parameters.Add("@FK_IdUser", SqlDbType.BigInt).Value = cart.User.IdUser;
            command.Parameters.Add("@Created", SqlDbType.DateTime2).Value = now;
            command.Parameters.Add("@Updated", SqlDbType.DateTime2).Value = now;

            long id = (long)command.ExecuteScalar();

            return new Cart(id, cart);
        }

        public List<CartSold> GetAllCarts()
        {

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Carts.Created, Carts.Discount, Carts.Subtotal, Carts.Taxes, Carts.Total, Users.Name " +
                                    "FROM Carts " +
                                    "INNER JOIN Users " +
                                    "ON Users.IdUser = Carts.FK_IdUser; ";

            using SqlDataReader reader = command.ExecuteReader();

            List<CartSold> carts = new List<CartSold>();
            while (reader.Read())
                carts.Add(ReadNextCartSold(reader));

            return carts;
        }

        public List<CartSold> GetAllCartsOfClient(User user)
        {

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Carts.Created, Carts.Discount, Carts.Subtotal, Carts.Taxes, Carts.Total, Users.Name " +
                                    "FROM Carts " +
                                    "INNER JOIN Users " +
                                    "ON Users.IdUser = Carts.FK_IdUser " +
                                    "WHERE Carts.FK_IdUser = @Id";

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = user.IdUser;

            using SqlDataReader reader = command.ExecuteReader();

            List<CartSold> carts = new List<CartSold>();
            while (reader.Read())
                carts.Add(ReadNextCartSold(reader));

            return carts;
        }

        public decimal GetDataStore()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT SUM(Carts.Total) as TotalSells " +
                                    "FROM Carts;";

            using SqlDataReader reader = command.ExecuteReader();

            return reader.GetDecimal(0);
          
        }

        private CartSold ReadNextCartSold(SqlDataReader reader)
        {
            DateTime dateSold = reader.GetDateTime(0);
            decimal discount = reader.GetDecimal(1);
            decimal subtotal = reader.GetDecimal(2);
            decimal taxes = reader.GetDecimal(3);
            decimal total = reader.GetDecimal(4);
            string customerName = reader.GetString(5);

            return new CartSold(dateSold, discount, subtotal, taxes, total, customerName);
        }
    }
}

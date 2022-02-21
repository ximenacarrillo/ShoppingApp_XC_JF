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
    }
}

using Isi.ShoppingApp.Core.Entities;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Isi.ShoppingApp.Data.Repositories
{
    public class CartProductRepository
    {
        private readonly string connectionString;

        public CartProductRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingDatabase"].ConnectionString;
        }

        public Cart_Products CreateCartProducts(Cart_Products cartProducts)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            //TODO
            //command.CommandText = "";


            return new Cart_Products(cartProducts.CartObject, cartProducts.ProductObject, cartProducts.Quantity, cartProducts.Price, cartProducts.Discount, cartProducts.Subtotal);
        }
    }
}

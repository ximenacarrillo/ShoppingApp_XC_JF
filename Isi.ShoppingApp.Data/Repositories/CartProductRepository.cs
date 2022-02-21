using Isi.ShoppingApp.Core.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace Isi.ShoppingApp.Data.Repositories
{
    public class CartProductRepository
    {
        private readonly string connectionString;

        public CartProductRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingDatabase"].ConnectionString;
        }

        public Cart_Products CreateCartProducts(Cart_Products cartProduct, long idCart)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "INSERT INTO Cart_Products (FK_IdProduct, FK_IdCart, Quantity, Price,Discount, Subtotal) " +
                                      "OUTPUT inserted.IdCart_Product " +
                                      "VALUES(@FK_IdProduct, @FK_IdCart, @Quantity, @Price,@Discount, @Subtotal);";
            DateTime now = DateTime.UtcNow;
            command.Parameters.Add("@FK_IdProduct", SqlDbType.BigInt).Value = cartProduct.ProductObject.IdProduct;
            command.Parameters.Add("@FK_IdCart", SqlDbType.BigInt).Value = idCart;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = cartProduct.Price;
            command.Parameters.Add("@Quantity", SqlDbType.Int).Value = cartProduct.Quantity;
            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = cartProduct.DiscountValue;
            command.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = cartProduct.Subtotal;

            long id = (long)command.ExecuteScalar();

            return new Cart_Products(id, cartProduct);
        }


        public List<Cart_Products> CreateProductsFromCart(Cart cart)
        {
            List<Cart_Products> products = new List<Cart_Products>();
            foreach (Cart_Products cart_Products in cart.Products)
            {
                products.Add(CreateCartProducts(cart_Products, cart.IdCart));
            }
            return products;
        }
    }
}

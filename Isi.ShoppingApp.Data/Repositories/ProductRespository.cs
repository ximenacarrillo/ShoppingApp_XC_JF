using Isi.ShoppingApp.Core.Entities;
using Isi.Utility.Results;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Isi.ShoppingApp.Data.Repositories
{
    public class ProductRespository
    {
        private readonly string connectionString;
        public ProductRespository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingDatabase"].ConnectionString;
        }
        
        public Product GetProductById(long id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT Products.IdProduct, Products.Name, Products.Price,Products.Stock, Products.UnitSold, Products.Discount, Categories.IdCategory, Categories.Name " +
                                    "FROM Products " +
                                    "INNER JOIN Categories " +
                                    "ON Products.FK_IdCategory = Categories.IdCategory " +
                                    "WHERE Products.IdProduct = @Id; ";

            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                return ReadNextProduct(reader);
            return null;
        }

        public List<Product> GetAllProducts()
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Products.IdProduct, Products.Name, Products.Price,Products.Stock, Products.UnitSold, Products.Discount, Categories.IdCategory, Categories.Name " +
                                    "FROM Products " +
                                    "INNER JOIN Categories " +
                                    "ON Products.FK_IdCategory = Categories.IdCategory";



            using SqlDataReader reader = command.ExecuteReader();

            List<Product> employees = new List<Product>();
            while (reader.Read())
                employees.Add(ReadNextProduct(reader));
            return employees;
        }

        private Product ReadNextProduct(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string name = reader.GetString(1);
            decimal price = reader.GetDecimal(2);
            int stock = reader.GetInt32(3);
            int unitSold = reader.GetInt32(4);
            decimal? discount = reader.GetDecimal(5);
            long idCategory = reader.GetInt64(6);
            string categoryName = reader.GetString(7);
            
            
            return new Product
                (
                    id,
                    name,
                    price,
                    stock,
                    discount,
                    unitSold,
                    new Category
                        (
                            idCategory,
                            categoryName
                        )
                );

        }

        
    }
}

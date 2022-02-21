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

        public bool ProductExist(long id)
        {
            return GetProductById(id) != null;
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

        public List<Product> GetProductsByFirstName(string filterText)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Products.IdProduct, Products.Name, Products.Price,Products.Stock, Products.UnitSold, Products.Discount, Categories.IdCategory, Categories.Name " +
                                    "FROM Products " +
                                    "INNER JOIN Categories " +
                                    "ON Products.FK_IdCategory = Categories.IdCategory " +
                                    "WHERE Products.Name LIKE @FilterName";


            command.Parameters.Add("@FilterName", SqlDbType.NVarChar).Value = $"%{filterText}%";


            using SqlDataReader reader = command.ExecuteReader();

            List<Product> employees = new List<Product>();
            while (reader.Read())
                employees.Add(ReadNextProduct(reader));
            return employees;
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

        public bool DeleteProduct(Product product)
        {
            return DeleteProduct(product.IdProduct);
        }

        public bool DeleteProduct(long id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Products " +
                                    "WHERE Products.IdProduct = @Id;";
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = id;

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0;
        }

        public Product CreateProduct(Product product)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Products (Name, Price, Stock,UnitSold,Discount, FK_IdCategory, Created, Updated) " +
                                    "OUTPUT inserted.IdProduct " +
                                    "VALUES(@Name, @Price, @Stock, @UnitSold, @Discount, @FK_IdCategory, @Created, @Modified); ";
            DateTime now = DateTime.UtcNow;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
            command.Parameters.Add("@Stock", SqlDbType.Int).Value = product.Stock;
            command.Parameters.Add("@UnitSold", SqlDbType.Int).Value = product.UnitSold;
            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = product.Discount ?? DBNull.Value as object;
            command.Parameters.Add("@FK_IdCategory", SqlDbType.BigInt).Value = product.Category.IdCategory;
            command.Parameters.Add("@Created", SqlDbType.DateTime2).Value = now;
            command.Parameters.Add("@Modified", SqlDbType.DateTime2).Value = now;

            long id = (long)command.ExecuteScalar();

            return new Product(id, product);
        }

        public bool UpdateProduct(Product product)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Products " +
                                    "SET Name = @Name, Price = @Price, Stock = @Stock, UnitSold = @UnitSold, Discount = @Discount, FK_IdCategory = @FK_IdCategory, Updated = @Updated " +
                                    "WHERE IdProduct = @Id;";
            DateTime now = DateTime.UtcNow;
            command.Parameters.Add("@Id", SqlDbType.BigInt).Value = product.IdProduct;
            command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = product.Name;
            command.Parameters.Add("@Price", SqlDbType.Decimal).Value = product.Price;
            command.Parameters.Add("@Stock", SqlDbType.Int).Value = product.Stock;
            command.Parameters.Add("@UnitSold", SqlDbType.Int).Value = product.UnitSold;
            command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = product.Discount ?? DBNull.Value as object;
            command.Parameters.Add("@FK_IdCategory", SqlDbType.BigInt).Value = product.Category.IdCategory;
            command.Parameters.Add("@Updated", SqlDbType.DateTime2).Value = now;

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }

        private Product ReadNextProduct(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string name = reader.GetString(1);
            decimal price = reader.GetDecimal(2);
            int stock = reader.GetInt32(3);
            int unitSold = reader.GetInt32(4);
            decimal? discount = reader.IsDBNull(5) ? null : reader.GetDecimal(5);
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

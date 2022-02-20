using Isi.ShoppingApp.Core.Entities;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isi.ShoppingApp.Data.Repositories
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingDatabase"].ConnectionString;
        }

        public User GetEmployee(long id)
        {
            return null;
        }


        
        private User ReadNextUser(SqlDataReader reader)
        {
            long id = reader.GetInt64(0);
            string username = reader.GetString(1);
            string name = reader.GetString(2);
            byte[] password = null; //TODO = (byte[])reader.GetValues(3);
            long idRole = reader.GetInt64(4);

            return new User(id, username, name, password, idRole);
        }

        public User GetPassword(string userName)
        {
            byte[] pass = { 1 };
            User user = new User(10001, "JuanL", "Juan Lopez",pass,1);
            //TODO: implement authentication library

            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT IdUser,Username ,Name,Password,FK_IdRole,Created,Updated " +
                "FROM dbo.Users WHERE Username = @Username";
            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = userName;

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
                return ReadNextUser(reader);
            return null;
        }
    }
}

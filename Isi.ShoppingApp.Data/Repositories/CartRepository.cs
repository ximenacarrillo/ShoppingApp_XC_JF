using System.Configuration;


namespace Isi.ShoppingApp.Data.Repositories
{
    public class CartRepository
    {
        private readonly string connectionString;

        public CartRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingApp"].ConnectionString;
        }
    }
}

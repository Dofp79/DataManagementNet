using System.Data.SqlClient;

namespace DataManagementNet.Datos
{
    /// <summary>
    /// This class provides a method named "getStringSQL" to get the SQL Server connection string from the "appsettings.json" configuration 
    /// file. The connection string is stored in the "StringSQL" private field, and it is fetched and returned by the "getStringSQL" method.
    /// </summary>
    public class Conexion
    {
        private string StringSQL = string.Empty;

        public Conexion()
        {
            // Create a ConfigurationBuilder and set the base path to the current directory
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            // Get the connection string from the "appsettings.json" configuration file under the "ConnectionStrings" section with the key "StringSQL"
            StringSQL = builder.GetSection("ConnectionStrings:StringSQL").Value;
        }

        // A method to get the connection string
        public string getStringSQL()
        {
            return StringSQL;
        }
    }
}

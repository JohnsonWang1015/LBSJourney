using Microsoft.Data.SqlClient;

namespace LBSJourney.Models
{
    public class ConnectionFactory
    {
        private String _connectionString;

        public String ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}

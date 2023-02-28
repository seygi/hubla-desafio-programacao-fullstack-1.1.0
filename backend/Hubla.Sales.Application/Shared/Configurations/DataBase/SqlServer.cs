using System.Data.SqlClient;

namespace Hubla.Sales.Application.Shared.Configurations.DataBase
{
    public sealed class SqlServer
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public int Timeout { get; set; }

        public int Lifetime { get; set; }

        public int MinPoolSize { get; set; }

        public int MaxPoolSize { get; set; }

        public bool MultipleActiveResultSets { get; set; }

        public string GetConnectionString()
        {
            return new SqlConnectionStringBuilder
            {
                DataSource = Server,
                Password = Password,
                InitialCatalog = Database,
                UserID = User,
                ConnectTimeout = Timeout,
                LoadBalanceTimeout = Lifetime,
                MinPoolSize = MinPoolSize,
                MaxPoolSize = MaxPoolSize,
                MultipleActiveResultSets = MultipleActiveResultSets
            }.ConnectionString;
        }
    }
}
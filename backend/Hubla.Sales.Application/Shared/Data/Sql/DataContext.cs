using Hubla.Sales.Application.Shared.Configurations;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Shared.Data.Sql
{
    [ExcludeFromCodeCoverage]
    internal sealed class DataContext : IDataContext
    {
        private readonly string _connectionString;
        public IDbConnection GetConnection() => new SqlConnection(_connectionString);

        public DataContext(IOptions<ConnectionStrings> configuration)
        {
            _connectionString = configuration.Value.SqlServer.GetConnectionString();
        }
    }
}
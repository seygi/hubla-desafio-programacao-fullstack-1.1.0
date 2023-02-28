using Dapper;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Shared.Data.Sql
{
    [ExcludeFromCodeCoverage]
    internal sealed class SqlService : ISqlService
    {
        private readonly IDataContext _dataContext;

        public SqlService(IDataContext dataContext) => _dataContext = dataContext;

        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = default)
        {
            using (var connection = _dataContext.GetConnection())
                return await connection.QueryAsync<T>(sql, param);
        }

        public async Task<int> InsertAsync(string sql, object param = default)
        {
            using (var connection = _dataContext.GetConnection())
                return await connection.ExecuteAsync(sql, param);
        }
    }
}
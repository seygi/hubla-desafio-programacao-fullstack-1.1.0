namespace Hubla.Sales.Application.Shared.Data.Sql
{
    public interface ISqlService
    {
        Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = default);

        Task<int> InsertAsync(string sql, object param = default);
    }
}
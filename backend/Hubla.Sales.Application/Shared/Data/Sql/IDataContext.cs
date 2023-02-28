using System.Data;

namespace Hubla.Sales.Application.Shared.Data.Sql
{
    public interface IDataContext
    {
        IDbConnection GetConnection();
    }
}
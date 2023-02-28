using Hubla.Sales.Application.Shared.Data.Sql;
using Hubla.Sales.Application.Shared.Models;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Microsoft.Extensions.Logging;

namespace Hubla.Sales.Application.Shared.Sales.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private const string ColumnsSelectSale = @"
          SELECT   [Id]
                   ,[SaleType]
                   ,[Date]
                   ,[Description]
                   ,[Value]
                   ,[SalesmanName]
        ";

        public static readonly string QueryListAll = @$"
           {ColumnsSelectSale}
            FROM [dbo].[Sales] WITH (NOLOCK)
        ";

        public const string InsertCommand = @"
            INSERT INTO [Sales] (SaleType, Date, Description, Value, SalesmanName)
                   VALUES (@saletype, @date, @description, @value, @salesmanname)";

        private readonly ISqlService _sqlService;
        private readonly ILogger<SaleRepository> _logger;

        public SaleRepository(ILogger<SaleRepository> logger, ISqlService sqlService)
        {
            _logger = logger;
            _sqlService = sqlService;
        }

        public async Task<IList<Sale>> ListAsync()
        {
            try
            {
                var result = await _sqlService.QueryListAsync<Sale>(
                                 QueryListAll) ??
                             Array.Empty<Sale>();

                _logger.LogInformation("Lista de vendas consultada com sucesso");

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Falha ao consultar lista de vendas");

                return Array.Empty<Sale>();
            }
        }

        public async Task<OperationResult> SaveAsync(IList<Sale> sales)
        {
            try
            {
                var rowsAffected = await _sqlService.InsertAsync(
                    InsertCommand,
                    new
                    {
                        salesType = sales[0].SaleType,
                        date = sales[0].Date,
                        description = sales[0].Description,
                        value = sales[0].Value,
                        salesmanName = sales[0].SalesmanName
                    });

                if (rowsAffected > 0)
                {
                    _logger.LogInformation("venda cadastrada com sucesso {@sale}", sales);
                    return OperationResult.Success(rowsAffected);
                }

                _logger.LogWarning("venda {@sale} não cadastrada, porém sem falhas no banco de dados.", sales);

                return OperationResult.Success(rowsAffected);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Falha ao salvar a venda no banco de dados");

                return OperationResult.Error;
            }
        }
    }
}
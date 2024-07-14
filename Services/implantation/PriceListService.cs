// PriceListService.cs
using System.Threading.Tasks;
using Dapper;
using YourNamespace.Helpers;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class PriceListService : IPriceListService
    {
        private readonly DatabaseConnectionHelper _dbConnectionHelper;

        public PriceListService(DatabaseConnectionHelper dbConnectionHelper)
        {
            _dbConnectionHelper = dbConnectionHelper;
        }

        public async Task<PriceList> GetECommercePriceList()
        {
            using (var connection = _dbConnectionHelper.GetConnection())
            {
                string priceListQuery;
                if (_dbConnectionHelper.ServerType == "HANA")
                {
                    priceListQuery = @"
                        SELECT 
                            ""ListNum"" AS ""PriceListCode"",
                            ""ListName"" AS ""PriceListName""
                        FROM 
                            ""OPLN""
                        WHERE 
                            ""ListName"" = 'E-Commerce'
                    ";
                }
                else // Assuming SQL Server
                {
                    priceListQuery = @"
                        SELECT 
                            ListNum AS PriceListCode,
                            ListName AS PriceListName
                        FROM 
                            OPLN
                        WHERE 
                            ListName = 'E-Commerce'
                    ";
                }

                var priceList = await connection.QueryFirstOrDefaultAsync<PriceList>(priceListQuery);
                return priceList;
            }
        }
    }
}

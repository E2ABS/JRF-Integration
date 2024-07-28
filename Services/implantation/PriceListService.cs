using System.Linq;
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
                string itemsQuery;
                string uomPricesQuery;

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

                    itemsQuery = @"
                        SELECT 
                            A.""ItemCode"",
                            B.""ItemName""
                        FROM 
                            ""ITM1"" A
                        INNER JOIN ""OITM"" B ON A.""ItemCode"" = B.""ItemCode""
                        WHERE 
                            A.""PriceList"" = @PriceListCode
                    ";

                    uomPricesQuery = @"
                        SELECT 
                            A.""ItemCode"",
                            A.""UomEntry"" AS ""UoM"",
                            B.""UomName"",
                            A.""Price""
                        FROM 
                            ""ITM1"" A
                        INNER JOIN ""OUOM"" B ON A.""UomEntry"" = B.""UomEntry""
                        WHERE 
                            A.""PriceList"" = @PriceListCode AND A.""ItemCode"" = @ItemCode
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

                    itemsQuery = @"
                        SELECT 
                            A.ItemCode,
                            B.ItemName
                        FROM 
                            ITM1 A
                        INNER JOIN OITM B ON A.ItemCode = B.ItemCode
                        WHERE 
                            A.PriceList = @PriceListCode
                    ";

                    uomPricesQuery = @"
                        SELECT 
                            A.ItemCode,
                            A.UomEntry AS UoM,
                            B.UomName,
                            A.Price
                        FROM 
                            ITM1 A
                        INNER JOIN OUOM B ON A.UomEntry = B.UomEntry
                        WHERE 
                            A.PriceList = @PriceListCode AND A.ItemCode = @ItemCode
                    ";
                }

                var priceList = await connection.QueryFirstOrDefaultAsync<PriceList>(priceListQuery);

                if (priceList != null)
                {
                    var items = await connection.QueryAsync<PriceListItem>(itemsQuery, new { PriceListCode = priceList.PriceListCode });

                    foreach (var item in items)
                    {
                        var uomPrices = await connection.QueryAsync<UoMPrice>(uomPricesQuery, new { PriceListCode = priceList.PriceListCode, ItemCode = item.ItemCode });
                        item.UoMPrices = uomPrices.ToList();
                    }

                    priceList.Items = items.ToList();
                }

                return priceList;
            }
        }
    }
}

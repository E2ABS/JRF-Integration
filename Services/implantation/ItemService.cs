// Services/ItemService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using YourNamespace.Helpers;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class ItemService : IItemService
    {
        private readonly DatabaseConnectionHelper _dbConnectionHelper;

        public ItemService(DatabaseConnectionHelper dbConnectionHelper)
        {
            _dbConnectionHelper = dbConnectionHelper;
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            using (var connection = _dbConnectionHelper.GetConnection())
            {
                string itemsQuery;
                if (_dbConnectionHelper.ServerType == "HANA")
                {
                    itemsQuery = @"
                        SELECT 
                            OITM.""ItemCode"", 
                            OITM.""ItemName"" AS ""Description"",
                            OITM.""CodeBars"" AS ""BarCode"",
                            OITM.""ItmsGrpCod"" AS ""ItemGroup"",
                            OITM.""SalUnitMsr"" AS ""ItemUOM"",
                            ITM1.""Price"" AS ""Price""
                        FROM 
                            ""OITM""
                        LEFT JOIN 
                            ""ITM1"" ON OITM.""ItemCode"" = ITM1.""ItemCode"" AND ITM1.""PriceList"" = 1
                        WHERE 
                            OITM.""validFor"" = 'Y'
                            AND OITM.""QryGroup64"" = 'Y'
                    ";
                }
                else // Assuming SQL Server
                {
                    itemsQuery = @"
                        SELECT 
                            OITM.ItemCode, 
                            OITM.ItemName AS Description,
                            OITM.CodeBars AS BarCode,
                            OITM.ItmsGrpCod AS ItemGroup,
                            OITM.SalUnitMsr AS ItemUOM,
                            ITM1.Price AS Price
                        FROM 
                            OITM
                        LEFT JOIN 
                            ITM1 ON OITM.ItemCode = ITM1.ItemCode AND ITM1.PriceList = 1
                        WHERE 
                            OITM.validFor = 'Y'
                            AND OITM.QryGroup64 = 'Y'
                    ";
                }

                var items = await connection.QueryAsync<Item>(itemsQuery);
                return items;
            }
        }
    }
}

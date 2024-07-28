// StockService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using YourNamespace.Helpers;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class StockService : IStockService
    {
        private readonly DatabaseConnectionHelper _dbConnectionHelper;

        public StockService(DatabaseConnectionHelper dbConnectionHelper)
        {
            _dbConnectionHelper = dbConnectionHelper;
        }

        public async Task<IEnumerable<ItemStock>> GetStock()
        {
            using (var connection = _dbConnectionHelper.GetConnection())
            {
                string stockQuery;
                if (_dbConnectionHelper.ServerType == "HANA")
                {
                    stockQuery = @"
                        SELECT 
                            OITM.""ItemCode"", 
                            OITM.""ItemName"" AS ""Description"",
                            OITM.""CodeBars"" AS ""BarCode"",
                            OITM.""ItmsGrpCod"" AS ""ItemGroup"",
                            OITM.""SalUnitMsr"" AS ""ItemUOM"",
                            ITM1.""Price"" AS ""Price"",
                            OITW.""OnHand"" AS ""InStock"",
                            OITW.""IsCommited"" AS ""Committed"",
                            (OITW.""OnHand"" - OITW.""IsCommited"") AS ""AvailableQuantity"",
                            OITW.""WhsCode"" AS ""Warehouse""
                        FROM 
                            ""OITM""
                        LEFT JOIN 
                            ""ITM1"" ON OITM.""ItemCode"" = ITM1.""ItemCode""
                        LEFT JOIN 
                            ""OITW"" ON OITM.""ItemCode"" = OITW.""ItemCode""
                        WHERE 
                            OITM.""validFor"" = 'Y'
                            AND OITM.""QryGroup64"" = 'Y'
                            AND OITM.""frozenFor"" = 'N'
                    ";
                }
                else // Assuming SQL Server
                {
                    stockQuery = @"
                        SELECT 
                            OITM.ItemCode, 
                            OITM.ItemName AS Description,
                            OITM.CodeBars AS BarCode,
                            OITM.ItmsGrpCod AS ItemGroup,
                            OITM.SalUnitMsr AS ItemUOM,
                            ITM1.Price AS Price,
                            OITW.OnHand AS InStock,
                            OITW.IsCommited AS Committed,
                            (OITW.OnHand - OITW.IsCommited) AS AvailableQuantity,
                            OITW.WhsCode AS Warehouse 
                        FROM 
                            OITM
                        LEFT JOIN 
                            ITM1 ON OITM.ItemCode = ITM1.ItemCode
                        LEFT JOIN 
                            OITW ON OITM.ItemCode = OITW.ItemCode
                        WHERE 
                            OITM.validFor = 'Y'
                            AND OITM.QryGroup64 = 'Y'
                            AND OITM.frozenFor = 'N'
                    ";
                }

                var stock = await connection.QueryAsync<ItemStock>(stockQuery);
                return stock;
            }
        }
    }
}

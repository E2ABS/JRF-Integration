// IStockService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public interface IStockService
    {
        Task<IEnumerable<ItemStock>> GetStock();
    }
}

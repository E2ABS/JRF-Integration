// IItemService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetItems();
    }
}

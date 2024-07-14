// IPriceListService.cs
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public interface IPriceListService
    {
        Task<PriceList> GetECommercePriceList();
    }
}

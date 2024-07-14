// ICustomerService.cs
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public interface ICustomerService
    {
        Task<string> LoginToSapB1();
        Task<string> CreateCustomer(CustomerRequestModel customer);
    }
}

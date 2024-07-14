// IPaymentService.cs
using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public interface IPaymentService
    {
        Task<string> LoginToSapB1();
        Task<string> CreateIncomingPayment(IncomingPaymentRequestModel payment);
    }
}

using System.Threading.Tasks;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public interface ISapB1Service
    {
        Task<string> LoginToSapB1();
        Task<string> CreateARInvoice(ARInvoiceRequestModel arInvoice);
    }
}

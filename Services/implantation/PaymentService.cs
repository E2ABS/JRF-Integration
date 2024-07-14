// PaymentService.cs
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using YourNamespace.Models;

namespace YourNamespace.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly CompanyConnection _companyConnection;

        public PaymentService(IHttpClientFactory clientFactory, IOptions<CompanyConnection> config)
        {
            _clientFactory = clientFactory;
            _companyConnection = config.Value;
        }

        public async Task<string> LoginToSapB1()
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_companyConnection.ServiceLayerBase}Login");

            var loginData = new
            {
                UserName = _companyConnection.Username,
                Password = _companyConnection.Password,
                CompanyDB = _companyConnection.CompanyDBName
            };

            var jsonContent = JsonSerializer.Serialize(loginData);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);
                return loginResponse.SessionId;
            }

            return null;
        }

        public async Task<string> CreateIncomingPayment(IncomingPaymentRequestModel payment)
        {
            var sessionId = await LoginToSapB1();
            if (string.IsNullOrEmpty(sessionId))
            {
                throw new Exception("Failed to obtain SAP B1 session ID.");
            }

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_companyConnection.ServiceLayerBase}IncomingPayments");

            var jsonContent = JsonSerializer.Serialize(payment);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            request.Headers.Add("Cookie", $"B1SESSION={sessionId}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception(errorContent);
            }
        }

        private class LoginResponse
        {
            public string SessionId { get; set; }
        }
    }
}

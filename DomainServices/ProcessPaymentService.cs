using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderProcessManagement.DomainServices
{
    
    public interface IProcessPaymentService
    {
        public bool ChargePayment(string creditCardNumber, decimal amount);

    }

    public class ProcessPaymentService : IProcessPaymentService
    {
        private readonly IHttpClientFactory _httpClient;

        public ProcessPaymentService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        public bool ChargePayment(string creditCardNumber, decimal amount)
        {
            //Call third pary gateway service ... as don't have it return true
            //var client = _httpClient.CreateClient();
            //var response=client.PostAsync(gatewayApiUri, content);
           
            return true;
        }
    }
}

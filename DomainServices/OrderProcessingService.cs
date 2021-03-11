using OrderProcessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessManagement.DomainServices
{
    public interface IOrderProcessingService
    {
        public Task<OrderprocessStatus> Process(OrderDetails orderDetails);
    }
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IManageInventory _inventory;
        private readonly IProcessPaymentService _processPaymentService;
        private readonly IEmailService _emailService;

        public OrderProcessingService(IManageInventory inventory, IProcessPaymentService processPaymentService,IEmailService emailService)
        {
            _inventory = inventory;
            _processPaymentService = processPaymentService;
            _emailService = emailService;
        }

        public  async Task<OrderprocessStatus> Process(OrderDetails orderDetails)
        {
            _inventory.GetInventory();
            foreach (var orderedProduct in orderDetails.Products)
            {
                var inventoryStatus = _inventory.CheckInventory(orderedProduct.Product.ProductId, orderedProduct.Quantity);
                if (!inventoryStatus)
                    return new OrderprocessStatus
                    {
                        IsSuccess = false,
                        ProcessingStatusMessage = $"Insufficient inventory for Product:{orderedProduct.Product.Name} "

                    };

            }

            //If inventory exist then Charge payment

            var paymentStatus = _processPaymentService.ChargePayment(orderDetails.PaymentDetails.CreditCardNumber, orderDetails.OrderTotal);

            if (!paymentStatus)
                new OrderprocessStatus
                {
                    IsSuccess = false,
                    ProcessingStatusMessage = $"Payment Process failed"

                };

            //If payment charged then send email

            _emailService.SendEmail(orderDetails.ShippingAddress.ContactDetails.Email, orderDetails);

            return new OrderprocessStatus
            {
                IsSuccess = true,
                ProcessingStatusMessage = $"Order processed and sent email"

            };

        }
    }
}

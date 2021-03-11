using Moq;
using OrderProcessManagement.DomainServices;
using OrderProcessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrderProcessManagement.UnitTests
{
    public class OrderProcessingServiceTests
    {
        public readonly OrderProcessingService _sut;
        public readonly Mock<IManageInventory> _mockInventory;
        public readonly Mock<IProcessPaymentService> _mockProcessPayment;
        public readonly Mock<IEmailService> _mockEmailService;

        public OrderProcessingServiceTests()
        {
            _mockInventory = new Mock<IManageInventory>();
            _mockProcessPayment = new Mock<IProcessPaymentService>();
            _mockEmailService = new Mock<IEmailService>();
            _sut = new OrderProcessingService(_mockInventory.Object, _mockProcessPayment.Object, _mockEmailService.Object);
        }

        [Fact]
        public async Task WhenInventoryInsufficient_ShouldReturnOrderStatusFailed()
        {

            _mockInventory.Setup(x => x.CheckInventory(It.IsAny<string>(), It.IsAny<int>())).Returns(false);

            var orderDetails = new OrderDetails()
            {
                 Products=new List<OrderedProduct>() { new OrderedProduct() { Product=new Product {  ProductId="1", Name="Prod1", UnitPrice=11 } , Quantity=5, TotalPrice=55} },
                  OrderNumber=1234,
                   OrderTotal=55,
                    PaymentDetails=new Payment { CreditCardNumber="4111111111111"},
                     BillingAddress=new Address { ContactDetails=new Contact { FirstName="FN", LastName="LN", Email="test@test.com" }, Address1="add1", State="AZ", Country="US", Zip="54634" },
                      ShippingAddress= new Address { ContactDetails = new Contact { FirstName = "FN", LastName = "LN", Email = "test@test.com" }, Address1 = "add1", State = "AZ", Country = "US", Zip = "54634" }

            };
            var result = await _sut.Process(orderDetails);

            Assert.False(result.IsSuccess);
            Assert.Equal($"Insufficient inventory for Product:{orderDetails.Products[0].Product.Name} ", result.ProcessingStatusMessage);

        }

        [Fact]
        public async Task WhenPaymentProcessingFailed_ShouldReturnOrderStatusFailed()
        {

            _mockInventory.Setup(x => x.CheckInventory(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
            _mockProcessPayment.Setup(x => x.ChargePayment(It.IsAny<string>(), It.IsAny<decimal>())).Returns(false);

            var orderDetails = new OrderDetails()
            {
                Products = new List<OrderedProduct>() { new OrderedProduct() { Product = new Product { ProductId = "1", Name = "Prod1", UnitPrice = 11 }, Quantity = 5, TotalPrice = 55 } },
                OrderNumber = 1234,
                OrderTotal = 55,
                PaymentDetails = new Payment { CreditCardNumber = "4111111111111" },
                BillingAddress = new Address { ContactDetails = new Contact { FirstName = "FN", LastName = "LN", Email = "test@test.com" }, Address1 = "add1", State = "AZ", Country = "US", Zip = "54634" },
                ShippingAddress = new Address { ContactDetails = new Contact { FirstName = "FN", LastName = "LN", Email = "test@test.com" }, Address1 = "add1", State = "AZ", Country = "US", Zip = "54634" }

            };
            var result = await _sut.Process(orderDetails);

            Assert.False(result.IsSuccess);
            Assert.Equal($"Payment Process failed", result.ProcessingStatusMessage);

        }

        [Fact]
        public async Task WhenInventoryAndPaymentProcessed_ShouldReturnOrderStatusSuccess()
        {

            _mockInventory.Setup(x => x.CheckInventory(It.IsAny<string>(), It.IsAny<int>())).Returns(true);
            _mockProcessPayment.Setup(x => x.ChargePayment(It.IsAny<string>(), It.IsAny<decimal>())).Returns(true);
            _mockEmailService.Setup(x => x.SendEmail(It.IsAny<string>(), It.IsAny<OrderDetails>())).Returns(true);

            var orderDetails = new OrderDetails()
            {
                Products = new List<OrderedProduct>() { new OrderedProduct() { Product = new Product { ProductId = "1", Name = "Prod1", UnitPrice = 11 }, Quantity = 5, TotalPrice = 55 } },
                OrderNumber = 1234,
                OrderTotal = 55,
                PaymentDetails = new Payment { CreditCardNumber = "4111111111111" },
                BillingAddress = new Address { ContactDetails = new Contact { FirstName = "FN", LastName = "LN", Email = "test@test.com" }, Address1 = "add1", State = "AZ", Country = "US", Zip = "54634" },
                ShippingAddress = new Address { ContactDetails = new Contact { FirstName = "FN", LastName = "LN", Email = "test@test.com" }, Address1 = "add1", State = "AZ", Country = "US", Zip = "54634" }

            };
            var result = await _sut.Process(orderDetails);

            Assert.True(result.IsSuccess);
            Assert.Equal($"Order processed and sent email", result.ProcessingStatusMessage);

        }

    }
}

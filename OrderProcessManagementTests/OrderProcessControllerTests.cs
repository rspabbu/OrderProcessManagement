using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderProcessManagement.Controllers;
using OrderProcessManagement.DomainServices;
using OrderProcessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrderProcessManagement.UnitTests
{
    public class OrderProcessControllerTests
    {
        private readonly OrderProcessController _sut;
        private readonly Mock<IOrderProcessingService> _orderprocessingServiceMock;


        public OrderProcessControllerTests()
        {
            _orderprocessingServiceMock = new Mock<IOrderProcessingService>();
            _sut = new OrderProcessController(_orderprocessingServiceMock.Object);

        }

        [Fact]
        public async Task WhenProcessOrderRequested_ShouldReturnOrderStatus()
        {
            _orderprocessingServiceMock.Setup(x => x.Process(It.IsAny<OrderDetails>())).ReturnsAsync(new OrderprocessStatus { IsSuccess=true, ProcessingStatusMessage="Order Processed and Email Sent" });

            var request = new OrderDetails() {  OrderNumber=67567};

            var response= await _sut.Post(request) as OkObjectResult;

            var result = response.Value as OrderprocessStatus;
            Assert.True(result.IsSuccess);
            Assert.Equal("Order Processed and Email Sent", result.ProcessingStatusMessage);
        }

        [Fact]
        public async Task WhenInventoryInsufficient_ThenProcessOrder_ShouldReturnOrderStatusAsFalse()
        {
            _orderprocessingServiceMock.Setup(x => x.Process(It.IsAny<OrderDetails>())).ReturnsAsync(new OrderprocessStatus { IsSuccess = false, ProcessingStatusMessage = "Order Processed Failed: Insufficient Inventory" });

            var request = new OrderDetails() { OrderNumber=1234 };

            var response = await _sut.Post(request) as OkObjectResult;

            var result = response.Value as OrderprocessStatus;
            Assert.False(result.IsSuccess);
            Assert.Equal("Order Processed Failed: Insufficient Inventory", result.ProcessingStatusMessage);
        }
    }
}

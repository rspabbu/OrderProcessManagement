using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProcessManagement.DomainServices;
using OrderProcessManagement.Models;

namespace OrderProcessManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProcessController : ControllerBase
    {
        private readonly IOrderProcessingService _orderProcessingService;
        

        public OrderProcessController(IOrderProcessingService orderProcessingService)
        {
            _orderProcessingService = orderProcessingService;
            
        }
        [Route("Process")]
        [HttpPost]
        public async Task<IActionResult> Post(OrderDetails orderDetails)
        {
            return Ok(await _orderProcessingService.Process(orderDetails));
            
        }
    }
}

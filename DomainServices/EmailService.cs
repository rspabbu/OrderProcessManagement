using OrderProcessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessManagement.DomainServices
{
    public interface IEmailService
    {
        public bool SendEmail(string email, OrderDetails orderDetails);
    }
    public class EmailService : IEmailService
    {
        public bool SendEmail(string email, OrderDetails orderDetails)
        {
            //using mail component, send email

            return true;
        }
    }
}

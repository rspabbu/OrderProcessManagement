using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessManagement.Models
{
    public class OrderDetails
    {
        public int OrderNumber { get; set; }
        public IList<OrderedProduct> Products { get; set; }
        public decimal OrderTotal { get; set; }
        public Address BillingAddress { get; set; }
        public Address ShippingAddress { get; set; }
        public Payment PaymentDetails { get; set; }

    }

    public class OrderprocessStatus
    {
        public bool IsSuccess { get; set; }
        public string ProcessingStatusMessage { get; set; }
    }
    public class Payment
    {
        
        public string CreditCardNumber { get; set; }
        public string Expiry { get; set; }
        public string SecurityCode { get; set; }
        public string NameOnCard { get; set; }
    }

    public class Product
    {
        public string ProductId { get; set; }
        public string  Name { get; set; }
        public string  Description { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderedProduct
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

    }

    public class Address
    {
        public Contact ContactDetails { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }

    }

    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class Inventory
    {
        public Product ProductDetails { get; set; }
        public int Quantity { get; set; }
        
    }
}

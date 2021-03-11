using OrderProcessManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessManagement.DomainServices
{

    public interface IManageInventory
    {
        public void AddItem(Product product, int qty);
        public void GetInventory();
        public bool CheckInventory(string productId, int qty);
    }

    public class ManageInventory : IManageInventory
    {
        private IList<Inventory> inventories;        

        public void AddItem(Product product, int qty)
        {
            inventories.Add(new Inventory { ProductDetails = product, Quantity = qty });

        }
        public void GetInventory()
        {
            inventories = new List<Inventory>();
            inventories.Add(new Inventory
            {
                ProductDetails = new Product
                {
                    ProductId = "1",
                    Name = "Prod1",
                    Description = "Prod1 description",
                    UnitPrice = (decimal)11.5
                },
                Quantity = 10

            });

            inventories.Add(new Inventory
            {
                ProductDetails = new Product
                {
                    ProductId = "2",
                    Name = "Prod2",
                    Description = "Prod2 description",
                    UnitPrice = (decimal)15
                },
                Quantity = 10

            });
        }
        public bool CheckInventory(string productId, int qty)
        {
            return inventories.Any(x => x.ProductDetails.ProductId == productId && x.Quantity >= qty);        
        }


    }
}

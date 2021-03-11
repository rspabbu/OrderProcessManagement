using OrderProcessManagement.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrderProcessManagement.UnitTests
{
    public class ManageInventoryTests
    {

        private readonly ManageInventory _sut;

        public ManageInventoryTests()
        {
            _sut = new ManageInventory();

        }

        [Theory(DisplayName ="Validate CheckInventory")]
        [InlineData("1",5,true)]
        [InlineData("1", 15, false)]
        public async Task WhenValidProductQtyExist_CheckInventory_ShouldReturnTrue(string productId,int qty,bool expectedResult)
        {

            _sut.GetInventory();

            var result = _sut.CheckInventory(productId,qty);

            Assert.Equal(expectedResult, result);


        }

    }
}

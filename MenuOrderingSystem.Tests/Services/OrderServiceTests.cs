using MenuOrderingSystem.Models;
using MenuOrderingSystem.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MenuOrderingSystem.Tests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public void CreateOrder_NullRequest_Throws()
        {            
            var orderService = new OrderService();
            OrderRequest request = null;

            Assert.ThrowsAsync<ArgumentNullException>(() =>
                orderService.CreateOrder(request));
        }

        [Theory]
        [InlineData(MenuType.Breakfast, "1, 2, 3", "Eggs, Toast, Coffee")]
        [InlineData(MenuType.Lunch, "1, 2, 3", "Sandwich, Chips, Soda")]
        [InlineData(MenuType.Dinner, "1, 2, 3, 4", "Steak, Potatoes, Wine, Water, Cake")]
        public async Task CreateOrder_ValidRequest_ReturnsOrder(MenuType type, string menuItems, string expectedResult)
        {
            var orderService = new OrderService();
            OrderRequest request = new OrderRequest
            {
                Menu = type.ToString(),
                MenuItems = menuItems.Split(',')
                                .Select(int.Parse).ToList()
            };

            var result = await orderService.CreateOrder(request);

            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal(expectedResult, result.Display);
        }
    }
}

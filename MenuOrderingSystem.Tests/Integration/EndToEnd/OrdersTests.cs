using MenuOrderingSystem.Helpers;
using MenuOrderingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MenuOrderingSystem.Tests.Integration.EndToEnd
{
    public class OrdersTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public OrdersTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(MenuType.Breakfast, "1, 2, 3", "Eggs, Toast, Coffee")]
        [InlineData(MenuType.Breakfast, "1, 2", "Eggs, Toast, Water")]
        [InlineData(MenuType.Breakfast, "1, 2, 3, 3", "Eggs, Toast, Coffee(2)")]
        [InlineData(MenuType.Lunch, "1, 2, 3", "Sandwich, Chips, Soda")]
        [InlineData(MenuType.Lunch, "1, 2", "Sandwich, Chips, Water")]
        [InlineData(MenuType.Lunch, "1, 2, 2, 3", "Sandwich, Chips(2), Soda")]
        [InlineData(MenuType.Lunch, "1, 2, 2", "Sandwich, Chips(2), Water")]
        [InlineData(MenuType.Dinner, "1, 2, 3, 4", "Steak, Potatoes, Wine, Water, Cake")]
        [InlineData(MenuType.Dinner, "1, 2, 4", "Steak, Potatoes, Water, Cake")]
        public async Task CreateOrder_ValidRequest_ReturnsOkAndExpectedResponse(MenuType type, string menuItems, string expectedResult)
        {
            var orderRequest = new OrderRequest
            {
                Menu = type.ToString(),
                MenuItems = menuItems.Split(',')
                                .Select(int.Parse).ToList()
            };

            var data = new StringContent(
                JsonConvert.SerializeObject(orderRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            var client = _factory.CreateClient();
            
            var response = await client.PostAsync("orders", data);            
            var content = await response.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<string>(content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResult, contentResult.ToString());
        }

        [Theory]
        [InlineData(MenuType.Breakfast, "2, 3", "Unable to process: Main is missing")]
        [InlineData(MenuType.Breakfast, "1, 3", "Unable to process: Side is missing")]
        [InlineData(MenuType.Breakfast, "3", "Unable to process: Main is missing, side is missing")]
        [InlineData(MenuType.Breakfast, "1, 1, 2, 3", "Unable to process: Eggs cannot be ordered more than once")]
        [InlineData(MenuType.Breakfast, "1, 2, 2, 3", "Unable to process: Toast cannot be ordered more than once")]
        [InlineData(MenuType.Breakfast, "1, 1, 2, 2, 3", "Unable to process: Eggs cannot be ordered more than once, Toast cannot be ordered more than once")]
        [InlineData(MenuType.Lunch, "2, 3", "Unable to process: Main is missing")]
        [InlineData(MenuType.Lunch, "1, 3", "Unable to process: Side is missing")]
        [InlineData(MenuType.Lunch, "3", "Unable to process: Main is missing, side is missing")]
        [InlineData(MenuType.Lunch, "1, 1, 2, 3", "Unable to process: Sandwich cannot be ordered more than once")]
        [InlineData(MenuType.Lunch, "1, 2, 3, 3", "Unable to process: Soda cannot be ordered more than once")]
        [InlineData(MenuType.Lunch, "1, 1, 2, 3, 3", "Unable to process: Sandwich cannot be ordered more than once, Soda cannot be ordered more than once")]
        [InlineData(MenuType.Dinner, "2, 3, 4", "Unable to process: Main is missing")]
        [InlineData(MenuType.Dinner, "1, 3, 4", "Unable to process: Side is missing")]
        [InlineData(MenuType.Dinner, "1, 2, 3", "Unable to process: Dessert is missing")]
        [InlineData(MenuType.Dinner, "3", "Unable to process: Main is missing, side is missing, dessert is missing")]
        [InlineData(MenuType.Dinner, "1, 1, 2, 3, 4", "Unable to process: Steak cannot be ordered more than once")]
        [InlineData(MenuType.Dinner, "1, 2, 2, 3, 4", "Unable to process: Potatoes cannot be ordered more than once")]
        [InlineData(MenuType.Dinner, "1, 2, 3, 3, 4", "Unable to process: Wine cannot be ordered more than once")]
        [InlineData(MenuType.Dinner, "1, 2, 3, 4, 4", "Unable to process: Cake cannot be ordered more than once")]
        public async Task CreateOrder_InvalidRequest_ReturnsBadRequestAndExpectedResponse(MenuType type, string menuItems, string expectedResult)
        {
            var orderRequest = new OrderRequest
            {
                Menu = type.ToString(),
                MenuItems = menuItems.Split(',')
                                .Select(int.Parse).ToList()
            };

            var data = new StringContent(
                JsonConvert.SerializeObject(orderRequest),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            var client = _factory.CreateClient();

            var response = await client.PostAsync("orders", data);
            var content = await response.Content.ReadAsStringAsync();
            var contentResult = JsonConvert.DeserializeObject<string>(content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(expectedResult, contentResult.ToString());
        }

    }
}

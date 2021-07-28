using MenuOrderingSystem.Controllers;
using MenuOrderingSystem.Models;
using MenuOrderingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace MenuOrderingSystem.Tests.Integration.Controllers
{
    public class OrdersControllerTests
    {
        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public async Task Create_ValidRequest_ReturnsOk(MenuType type)
        {
            const string DISPLAY = "test";
            var orderRequest = new OrderRequest
            {
                Menu = type.ToString(),
                MenuItems = new List<int> { 1, 2, 3, 4 }
            };

            var orderServiceMock = Substitute.For<IOrderService>();
            orderServiceMock.CreateOrder(orderRequest)
                .Returns(new Order { IsValid = true, Display = DISPLAY });

            var orderController = new OrdersController(orderServiceMock);

            var response = await orderController.Create(orderRequest);

            Assert.IsType<OkObjectResult>(response);
            ObjectResult okResult = (ObjectResult)response;

            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);            
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public async Task Create_InvalidRequest_ReturnsBadRequest(MenuType type)
        {
            const string DISPLAY = "test";
            var orderRequest = new OrderRequest
            {
                Menu = type.ToString(),
                MenuItems = new List<int> { 2, 3, 4 }
            };

            var orderServiceMock = Substitute.For<IOrderService>();
            orderServiceMock.CreateOrder(orderRequest)
                .Returns(new Order { IsValid = false, Display = DISPLAY });

            var orderController = new OrdersController(orderServiceMock);

            var response = await orderController.Create(orderRequest);

            Assert.IsType<BadRequestObjectResult>(response);
            BadRequestObjectResult badResult = (BadRequestObjectResult)response;

            Assert.Equal((int)HttpStatusCode.BadRequest, badResult.StatusCode);
        }
    }
}

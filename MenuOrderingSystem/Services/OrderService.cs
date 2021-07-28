using MenuOrderingSystem.Models;
using MenuOrderingSystem.Models.Interfaces;
using MenuOrderingSystem.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MenuOrderingSystem.Services
{
    public class OrderService : IOrderService
    {
        public OrderService()
        {

        }

        public async Task<Order> CreateOrder(OrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)}");
            }

            IMenuOrder menuOrder = null;
            Enum.TryParse(request.Menu, out MenuType type);

            switch (type)
            {
                case MenuType.Breakfast:
                    menuOrder = new BreakfastOrder(request.MenuItems);
                    break;
                case MenuType.Lunch:
                    menuOrder = new LunchOrder(request.MenuItems);
                    break;
                case MenuType.Dinner:
                    menuOrder = new DinnerOrder(request.MenuItems);
                    break;
            }

            var order = menuOrder.CreateOrder();
            return await Task.FromResult(order);
        }
    }
}

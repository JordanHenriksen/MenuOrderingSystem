using MenuOrderingSystem.Helpers;
using MenuOrderingSystem.Models.Interfaces;
using MenuOrderingSystem.Models.Types;
using System.Collections.Generic;
using System.Linq;

namespace MenuOrderingSystem.Models
{
    public class LunchOrder : MenuOrderBase, IMenuOrder
    {
        public LunchOrder(IEnumerable<int> menuItems)
            : base(MenuType.Lunch, menuItems)
        {

        }

        protected string GetSideOrder()
        {
            var sideItems = GetSideItems();
            var sideOrder = LunchType.Side.GetDisplayName();

            if (sideItems.Count() > 1)
            {
                sideOrder = $"{LunchType.Side.GetDisplayName()}({sideItems.Count()})";
            }

            return sideOrder;
        }

        protected string ValidateDrinkItems(string display)
        {
            var drinkItems = GetDrinkItems();
            if (drinkItems.Count() > 1)
            {
                var drinkDisplay = $"{LunchType.Drink.GetDisplayName()} {Constants.MULTIPLE_ITEMS}";
                display = string.IsNullOrWhiteSpace(display) ?
                                        $"{Constants.UNABLE_TO_PROCESS} {drinkDisplay}" : $"{display}, {drinkDisplay}";
            }

            return display;
        }

        public Order CreateOrder()
        {
            var order = ValidateOrder();
            if (!order.IsValid)
            {
                return order;
            }

            var drinkOrder = GetDrinkOrder();
            var sideOrder = GetSideOrder();
            order.Display = $"{LunchType.Main.GetDisplayName()}, {sideOrder}, {drinkOrder}";

            return order;
        }

        public string GetDrinkOrder()
        {
            var drinkItems = GetDrinkItems();
            var drinkOrder = Constants.WATER;

            if (drinkItems.Any())
            {
                drinkOrder = LunchType.Drink.GetDisplayName();
            }

            return drinkOrder;
        }

        public Order ValidateOrder()
        {
            var order = new Order
            {
                Display = CheckMainAndSideItems()
            };

            order.Display = ValidateDrinkItems(order.Display);

            if (string.IsNullOrWhiteSpace(order.Display))
            {
                order.IsValid = true;
            }

            return order;
        }
    }
}

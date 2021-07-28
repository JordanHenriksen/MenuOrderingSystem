using MenuOrderingSystem.Helpers;
using MenuOrderingSystem.Models.Interfaces;
using MenuOrderingSystem.Models.Types;
using System.Collections.Generic;
using System.Linq;

namespace MenuOrderingSystem.Models
{
    public class BreakfastOrder : MenuOrderBase, IMenuOrder
    {
        public BreakfastOrder(IEnumerable<int> menuItems)
            : base (MenuType.Breakfast, menuItems)
        {

        }

        protected string ValidateSideItems(string display)
        {
            var sideItems = GetSideItems();
            if (sideItems.Count() > 1)
            {
                var sideDisplay = $"{BreakfastType.Side.GetDisplayName()} {Constants.MULTIPLE_ITEMS}";
                display = string.IsNullOrWhiteSpace(display) ? $"{Constants.UNABLE_TO_PROCESS} {sideDisplay}" : $"{display}, {sideDisplay}";
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
            order.Display = $"{BreakfastType.Main.GetDisplayName()}, {BreakfastType.Side.GetDisplayName()}, {drinkOrder}";

            return order;
        }

        public string GetDrinkOrder()
        {
            var drinkItems = GetDrinkItems();
            var drinkOrder = Constants.WATER;

            if (drinkItems.Any())
            {
                drinkOrder = drinkItems.Count() > 1 ? $"{BreakfastType.Drink.GetDisplayName()}({drinkItems.Count()})" : BreakfastType.Drink.GetDisplayName();
            }

            return drinkOrder;
        }

        public Order ValidateOrder()
        {
            var order = new Order
            {
                Display = CheckMainAndSideItems()
            };

            order.Display = ValidateSideItems(order.Display);            

            if (string.IsNullOrWhiteSpace(order.Display))
            {
                order.IsValid = true;
            }

            return order;
        }
    }
}

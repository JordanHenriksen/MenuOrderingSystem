using MenuOrderingSystem.Helpers;
using MenuOrderingSystem.Models.Interfaces;
using MenuOrderingSystem.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuOrderingSystem.Models
{
    public class DinnerOrder : MenuOrderBase, IMenuOrder
    {
        private readonly IEnumerable<int> _menuItems;

        public DinnerOrder(IEnumerable<int> menuItems)
            : base(MenuType.Dinner, menuItems)
        {
            _menuItems = menuItems;
        }

        protected IEnumerable<int> GetDessertItems()
        {
            if (_menuItems == null)
            {
                throw new ArgumentNullException($"{nameof(_menuItems)}");
            }

            return _menuItems.Where(x => x == (int)DinnerType.Dessert);
        }

        protected string ValidateDessertItems(string display)
        {
            var dessertItems = GetDessertItems();
            if (!dessertItems.Any())
            {
                display = string.IsNullOrWhiteSpace(display) ?
                                            $"{Constants.UNABLE_TO_PROCESS} Dessert is missing" : $"{display}, dessert is missing";
            }
            else if (dessertItems.Count() > 1)
            {
                var dessertDisplay = $"{Constants.UNABLE_TO_PROCESS} { DinnerType.Dessert.GetDisplayName()} {Constants.MULTIPLE_ITEMS}";
                display = string.IsNullOrWhiteSpace(display) ? dessertDisplay : $"{display}, {dessertDisplay}";
            }

            return display;
        }

        protected string ValidateDrinkItems(string display)
        {
            var drinkItems = GetDrinkItems();
            if (drinkItems.Count() > 1)
            {
                var drinkDisplay = $"{Constants.UNABLE_TO_PROCESS} { DinnerType.Drink.GetDisplayName()} {Constants.MULTIPLE_ITEMS}";
                display = string.IsNullOrWhiteSpace(display) ? drinkDisplay : $"{display}, {drinkDisplay}";
            }

            return display;
        }

        protected string ValidateSideItems(string display)
        {
            var sideItems = GetSideItems();
            if (sideItems.Count() > 1)
            {
                var sideDisplay = $"{Constants.UNABLE_TO_PROCESS} { DinnerType.Side.GetDisplayName()} {Constants.MULTIPLE_ITEMS}";
                display = string.IsNullOrWhiteSpace(display) ? sideDisplay : $"{display}, {sideDisplay}";
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
            order.Display = $"{DinnerType.Main.GetDisplayName()}, {DinnerType.Side.GetDisplayName()}, {drinkOrder}, {DinnerType.Dessert.GetDisplayName()}";

            return order;
        }

        public string GetDrinkOrder()
        {
            var drinkItems = GetDrinkItems();
            var drinkOrder = Constants.WATER;

            if (drinkItems.Any())
            {
                drinkOrder = $"{DinnerType.Drink.GetDisplayName()}, {Constants.WATER}";
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
            order.Display = ValidateDrinkItems(order.Display);
            order.Display = ValidateDessertItems(order.Display);

            if (string.IsNullOrWhiteSpace(order.Display))
            {
                order.IsValid = true;
            }

            return order;
        }
    }
}

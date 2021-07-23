using MenuOrderingSystem.Extensions;
using MenuOrderingSystem.Models;
using MenuOrderingSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuOrderingSystem.Services
{
    public class OrderService : IOrderService
    {
        private const string WATER = "water";

        protected void ValidateItems(IEnumerable<int> items)
        {
            if (items == null || !items.Any())
            {
                throw new ArgumentNullException($"{nameof(items)} cannot be null or empty.");
            }
        }

        protected string CreateBreakfast(IEnumerable<int> items)
        {
            ValidateItems(items);

            (bool isValid, string message) = items.ValidateBreakfastItems();            
            if (!isValid)
            {
                return message;
            }

            var menu = new Menu
            {
                MenuItems = new MenuItem
                {
                    Main = BreakfastType.Eggs.ToString(),
                    Side = BreakfastType.Toast.ToString(),
                    Drink = WATER
                }
            };

            var drink = items.Where(x => x == (int)BreakfastType.Coffee);
            if (drink.Any())
            {
                string drinkText = drink.Count() > 1 ? $"{BreakfastType.Coffee}{(drink.Count())}" : BreakfastType.Coffee.ToString();
                menu.MenuItems.Drink = drinkText;
            }

            return $"{menu.MenuItems.Main}, {menu.MenuItems.Side}, {menu.MenuItems.Drink}";
        }

        protected string CreateLunch(IEnumerable<int> items)
        {
            ValidateItems(items);

            (bool isValid, string message) = items.ValidateLunchItems();
            if (!isValid)
            {
                return message;
            }

            var menu = new Menu
            {
                MenuItems = new MenuItem
                {
                    Main = LunchType.Salad.ToString(),
                    Side = LunchType.Chips.ToString(),
                    Drink = WATER
                }
            };

            var side = items.Where(x => x == (int)LunchType.Chips);
            if (side.Any())
            {
                string sideText = side.Count() > 1 ? $"{LunchType.Chips}{(side.Count())}" : LunchType.Chips.ToString();
                menu.MenuItems.Side = sideText;
            }

            var drink = items.Where(x => x == (int)BreakfastType.Coffee);
            if (drink.Any())
            {
                menu.MenuItems.Drink = LunchType.Soda.ToString();
            }

            return $"{menu.MenuItems.Main}, {menu.MenuItems.Side}, {menu.MenuItems.Drink}";
        }

        protected string CreateDinner(IEnumerable<int> items)
        {
            ValidateItems(items);

            (bool isValid, string message) = items.ValidateDinnerItems();
            if (!isValid)
            {
                return message;
            }

            var menu = new Menu
            {
                MenuItems = new MenuItem
                {                    
                    Main = DinnerType.Steak.ToString(),
                    Side = DinnerType.Potatoes.ToString(),
                    Drink = WATER,
                    Dessert = DinnerType.Cake.ToString()
                }
            };

            var drink = items.Where(x => x == (int)DinnerType.Wine);
            if (drink.Any())
            {
                menu.MenuItems.Drink = $"Wine, {WATER}";
            }
            
            return $"{menu.MenuItems.Main}, {menu.MenuItems.Side}, {menu.MenuItems.Drink}, {menu.MenuItems.Dessert}";
        }

        public async Task<OrderResponse> CreateOrder(OrderRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} cannot be null or empty.");
            }

            var result = new OrderResponse();
            switch (request.Menu)
            {
                case MenuType.Breakfast:
                    result.OrderDisplay = CreateBreakfast(request.MenuItems);
                    break;
                case MenuType.Lunch:
                    result.OrderDisplay = CreateLunch(request.MenuItems);
                    break;
                case MenuType.Dinner:
                    result.OrderDisplay = CreateDinner(request.MenuItems);
                    break;
            }

            return await Task.FromResult(result);
        }
    }
}

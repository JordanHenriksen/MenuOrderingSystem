using MenuOrderingSystem.Helpers;
using MenuOrderingSystem.Models.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuOrderingSystem.Models
{
    public abstract class MenuOrderBase
    {
        private readonly IEnumerable<int> _menuItems;
        private readonly MenuType _type;        
        
        public MenuOrderBase(MenuType type, IEnumerable<int> menuItems)
        {
            _menuItems = menuItems;
            _type = type;
        }

        protected string GetMultipleMainDisplay()
        {
            var mainDisplay = string.Empty;
            switch (_type)
            {
                case MenuType.Breakfast:
                    mainDisplay = BreakfastType.Main.GetDisplayName();
                    break;
                case MenuType.Lunch:
                    mainDisplay = LunchType.Main.GetDisplayName();
                    break;
                case MenuType.Dinner:
                    mainDisplay = DinnerType.Main.GetDisplayName();
                    break;
            }

            return $"{Constants.UNABLE_TO_PROCESS} {mainDisplay} {Constants.MULTIPLE_ITEMS}";
        }

        protected void ValidateMenuItems()
        {
            if (_menuItems == null)
            {
                throw new ArgumentNullException($"{nameof(_menuItems)}");
            }
        }

        public string CheckMainAndSideItems()
        {
            ValidateMenuItems();

            var mainItems = GetMainItems();
            var sideItems = GetSideItems();
            var orderDisplay = string.Empty;
            
            if (!mainItems.Any())
            {
                orderDisplay = $"{Constants.UNABLE_TO_PROCESS} Main is missing";
            }
            else if (mainItems.Count() > 1)
            {
                orderDisplay = GetMultipleMainDisplay();
            }

            if (!sideItems.Any())
            {
                orderDisplay = string.IsNullOrWhiteSpace(orderDisplay) ?
                                            $"{Constants.UNABLE_TO_PROCESS} Side is missing" : $"{orderDisplay}, side is missing";
            }

            return orderDisplay;
        }

        public IEnumerable<int> GetDrinkItems()
        {
            ValidateMenuItems();

            IEnumerable<int> items = new List<int>();
            switch (_type)
            {
                case MenuType.Breakfast:
                    items = _menuItems.Where(x => x == (int)BreakfastType.Drink);
                    break;
                case MenuType.Lunch:
                    items = _menuItems.Where(x => x == (int)LunchType.Drink);
                    break;
                case MenuType.Dinner:
                    items = _menuItems.Where(x => x == (int)DinnerType.Drink);
                    break;
            }

            return items;
        }

        public IEnumerable<int> GetMainItems()
        {
            ValidateMenuItems();

            IEnumerable<int> items = new List<int>();
            switch (_type)
            {
                case MenuType.Breakfast:
                    items = _menuItems.Where(x => x == (int)BreakfastType.Main);
                    break;
                case MenuType.Lunch:
                    items = _menuItems.Where(x => x == (int)LunchType.Main);
                    break;
                case MenuType.Dinner:
                    items = _menuItems.Where(x => x == (int)DinnerType.Main);
                    break;
            }

            return items;
        }

        public IEnumerable<int> GetSideItems()
        {
            ValidateMenuItems();

            IEnumerable<int> items = new List<int>();
            switch (_type)
            {
                case MenuType.Breakfast:
                    items = _menuItems.Where(x => x == (int)BreakfastType.Side);
                    break;
                case MenuType.Lunch:
                    items = _menuItems.Where(x => x == (int)LunchType.Side);
                    break;
                case MenuType.Dinner:
                    items = _menuItems.Where(x => x == (int)DinnerType.Side);
                    break;
            }

            return items;
        }
    }
}

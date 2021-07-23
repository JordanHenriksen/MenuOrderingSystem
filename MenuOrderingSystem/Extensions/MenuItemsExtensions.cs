using MenuOrderingSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace MenuOrderingSystem.Extensions
{
    public static class MenuItemsExtensions
    {
        private const string MULTIPLE_ITEMS = "cannot be ordered more than once";
        private const string UNABLE_TO_PROCESS = "Unable to process:";

        public static (bool IsValid, string Message) ValidateBreakfastItems(this IEnumerable<int> items)
        {
            var main = items.Where(x => x == (int)BreakfastType.Eggs);
            var side = items.Where(x => x == (int)BreakfastType.Toast);

            (bool isValid, string message) = (false, string.Empty);

            // -- Duplicate validation across all menu types -- //
            if (!main.Any())
            {
                message = $"{UNABLE_TO_PROCESS} Main is missing";
            }

            // -- Duplicate validation across all menu types -- //
            if (!side.Any())
            {
                message = string.IsNullOrWhiteSpace(message) ? $"{UNABLE_TO_PROCESS} Side is missing" : $"{message}, side is missing";
            }

            if (main.Count() > 1)
            {
                message = $"{BreakfastType.Eggs} {MULTIPLE_ITEMS}";
            }

            if (side.Count() > 1)
            {
                message = $"{BreakfastType.Toast} {MULTIPLE_ITEMS}";
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                isValid = true;
            }

            return (isValid, message);
        }

        public static (bool IsValid, string Message) ValidateLunchItems(this IEnumerable<int> items)
        {
            var main = items.Where(x => x == (int)LunchType.Salad);
            var side = items.Where(x => x == (int)LunchType.Chips);
            var drink = items.Where(x => x == (int)LunchType.Soda);

            (bool isValid, string message) = (false, string.Empty);

            // -- Duplicate validation across all menu types -- //
            if (!main.Any())
            {
                message = $"{UNABLE_TO_PROCESS} Main is missing";
            }

            // -- Duplicate validation across all menu types -- //
            if (!side.Any())
            {
                message = string.IsNullOrWhiteSpace(message) ? $"{UNABLE_TO_PROCESS} Side is missing" : $"{message}, side is missing";
            }

            if (main.Count() > 1)
            {
                message = $"{LunchType.Salad} {MULTIPLE_ITEMS}";
            }

            if (drink.Count() > 1)
            {
                message = $"{LunchType.Soda} {MULTIPLE_ITEMS}";
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                isValid = true;
            }

            return (isValid, message);
        }

        public static (bool IsValid, string Message) ValidateDinnerItems(this IEnumerable<int> items)
        {
            var main = items.Where(x => x == (int)DinnerType.Steak);
            var side = items.Where(x => x == (int)DinnerType.Potatoes);
            var drink = items.Where(x => x == (int)DinnerType.Wine);
            var dessert = items.Where(x => x == (int)DinnerType.Cake);

            (bool isValid, string message) = (false, string.Empty);

            // -- Duplicate validation across all menu types -- //
            if (!main.Any())
            {
                message = $"{UNABLE_TO_PROCESS} Main is missing";
            }

            // -- Duplicate validation across all menu types -- //
            if (!side.Any())
            {
                message = string.IsNullOrWhiteSpace(message) ? $"{UNABLE_TO_PROCESS} Side is missing" : $"{message}, side is missing";
            }

            if (!dessert.Any())
            {
                message = string.IsNullOrWhiteSpace(message) ? $"{UNABLE_TO_PROCESS} Dessert is missing" : $"{message}, dessert is missing";
            }

            if (main.Count() > 1)
            {
                message = $"{DinnerType.Steak} {MULTIPLE_ITEMS}";
            }

            if (side.Count() > 1)
            {
                message = $"{DinnerType.Potatoes} {MULTIPLE_ITEMS}";
            }

            if (drink.Count() > 1)
            {
                message = $"{DinnerType.Wine} {MULTIPLE_ITEMS}";
            }

            if (dessert.Count() > 1)
            {
                message = $"{DinnerType.Cake} {MULTIPLE_ITEMS}";
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                isValid = true;
            }

            return (isValid, message);
        }
    }
}

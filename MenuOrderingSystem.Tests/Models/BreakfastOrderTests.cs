using MenuOrderingSystem.Helpers;
using MenuOrderingSystem.Models;
using MenuOrderingSystem.Models.Types;
using System.Collections.Generic;
using Xunit;

namespace MenuOrderingSystem.Tests.Models
{
    public class BreakfastOrderTests
    {
        [Fact]
        public void CreateOrder_InvalidMenuItems_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var result = breakfastOrder.CreateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void CreateOrder_ValidMenuItems_ReturnsOrderIsValidTrueAndDisplay()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);
            var drinkOrder = breakfastOrder.GetDrinkOrder();

            var expected = $"{BreakfastType.Main.GetDisplayName()}, {BreakfastType.Side.GetDisplayName()}, {drinkOrder}";
            var result = breakfastOrder.CreateOrder();

            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal(expected, result.Display);
        }

        [Fact]
        public void GetDrinkOrder_EmptyDrinks_ReturnsWater()
        {
            const string WATER = "Water";
            IEnumerable<int> menuItems = new List<int> { 1, 2 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var result = breakfastOrder.GetDrinkOrder();

            Assert.NotEmpty(result);
            Assert.Equal(WATER, result);
        }

        [Fact]
        public void GetDrinkOrder_SingleDrink_ReturnsDrink()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var expected = BreakfastType.Drink.GetDisplayName();
            var result = breakfastOrder.GetDrinkOrder();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetDrinkOrder_MultipleDrinks_ReturnsDrinks()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var expected = $"{BreakfastType.Drink.GetDisplayName()}(2)";
            var result = breakfastOrder.GetDrinkOrder();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValidateOrder_EmptyMainAndValidSide_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 2, 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Main is missing";
            var result = breakfastOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_EmptyMainAndEmptySide_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Main is missing, side is missing";
            var result = breakfastOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_MultipleMainsAndEmptySide_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} {BreakfastType.Main.GetDisplayName()} {Constants.MULTIPLE_ITEMS}, side is missing";
            var result = breakfastOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_MultipleMainsAndValidSide_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 2, 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} {BreakfastType.Main.GetDisplayName()} {Constants.MULTIPLE_ITEMS}";
            var result = breakfastOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_ValidMainAndEmptySide_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Side is missing";
            var result = breakfastOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_ValidMainAndValidSide_ReturnsOrderIsValidTrue()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var breakfastOrder = new BreakfastOrder(menuItems);

            var result = breakfastOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Empty(result.Display);
        }

        [Fact]
        public void ValidateSideItems_ValidSides_ReturnsEmptyString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var breakfastOrder = new BreakfastOrderTest(menuItems);

            var result = breakfastOrder.ValidateSideItemsTest(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void ValidateSideItems_MultipleSides_ReturnsString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 2, 3 };
            var breakfastOrder = new BreakfastOrderTest(menuItems);

            var result = breakfastOrder.ValidateSideItemsTest("test");

            Assert.NotEmpty(result);
        }
    }

    public class BreakfastOrderTest : BreakfastOrder
    {
        public BreakfastOrderTest(IEnumerable<int> menuItems)
            : base(menuItems)
        {

        }

        public string ValidateSideItemsTest(string display)
        {
            return base.ValidateSideItems(display);
        }
    }
}

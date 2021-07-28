using MenuOrderingSystem.Helpers;
using MenuOrderingSystem.Models;
using MenuOrderingSystem.Models.Types;
using System.Collections.Generic;
using Xunit;

namespace MenuOrderingSystem.Tests.Models
{
    public class LunchOrderTests
    {
        [Fact]
        public void CreateOrder_InvalidMenuItems_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1 };
            var lunchOrder = new LunchOrder(menuItems);

            var result = lunchOrder.CreateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void CreateOrder_ValidMenuItems_ReturnsOrderIsValidTrueAndDisplay()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var lunchOrder = new LunchOrderTest(menuItems);
            var drinkOrder = lunchOrder.GetDrinkOrder();
            var sideOrder = lunchOrder.GetSideOrderTest();

            var expected = $"{LunchType.Main.GetDisplayName()}, {sideOrder}, {drinkOrder}";
            var result = lunchOrder.CreateOrder();

            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal(expected, result.Display);
        }

        [Fact]
        public void GetDrinkOrder_EmptyDrink_ReturnsWater()
        {
            const string WATER = "Water";
            IEnumerable<int> menuItems = new List<int> { 1, 2 };
            var lunchOrder = new LunchOrder(menuItems);

            var result = lunchOrder.GetDrinkOrder();

            Assert.NotEmpty(result);
            Assert.Equal(WATER, result);
        }

        [Fact]
        public void GetDrinkOrder_ValidDrinks_ReturnsDrink()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var lunchOrder = new LunchOrder(menuItems);

            var expected = LunchType.Drink.GetDisplayName();
            var result = lunchOrder.GetDrinkOrder();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSideOrder_SingleSide_ReturnsSide()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var lunchOrderTest = new LunchOrderTest(menuItems);
            
            var expected = LunchType.Side.GetDisplayName();
            var result = lunchOrderTest.GetSideOrderTest();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetSideOrder_MultipleSides_ReturnsSides()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 2, 3 };
            var lunchOrder = new LunchOrderTest(menuItems);

            var expected = $"{LunchType.Side.GetDisplayName()}(2)";
            var result = lunchOrder.GetSideOrderTest();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValidateOrder_EmptyMainAndValidSideAndValidDrink_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 2, 3 };
            var lunchOrder = new LunchOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Main is missing";
            var result = lunchOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_EmptyMainAndEmptySideAndValidDrink_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 3 };
            var lunchOrder = new LunchOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Main is missing, side is missing";
            var result = lunchOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_MultipleMainsAndEmptySideAndValidDrink_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 3 };
            var lunchOrder = new LunchOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} {LunchType.Main.GetDisplayName()} {Constants.MULTIPLE_ITEMS}, side is missing";
            var result = lunchOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_MultipleMainsAndValidSideAndValidDrink_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 2, 3 };
            var lunchOrder = new LunchOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} {LunchType.Main.GetDisplayName()} {Constants.MULTIPLE_ITEMS}";
            var result = lunchOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_ValidMainAndEmptySideAndValidDrink_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 3 };
            var lunchOrder = new LunchOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Side is missing";
            var result = lunchOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_ValidMainAndValidSideAndValidDrink_ReturnsOrderIsValidTrue()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var lunchOrder = new LunchOrder(menuItems);

            var result = lunchOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Empty(result.Display);
        }

        [Fact]
        public void ValidateDrinkItems_ValidDrinks_ReturnsEmptyString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var lunchOrder = new LunchOrderTest(menuItems);

            var result = lunchOrder.ValidateDrinkItemsTest(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void ValidateSideItems_MultipleSides_ReturnsString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 2, 3 };
            var lunchOrder = new LunchOrderTest(menuItems);

            var result = lunchOrder.ValidateDrinkItemsTest("test");

            Assert.NotEmpty(result);
        }
    }    

    public class LunchOrderTest : LunchOrder
    {
        public LunchOrderTest(IEnumerable<int> menuItems)
            : base (menuItems)
        {

        }

        public string GetSideOrderTest()
        {
            return base.GetSideOrder();
        }

        public string ValidateDrinkItemsTest(string display)
        {
            return base.ValidateDrinkItems(display);
        }
    }
}

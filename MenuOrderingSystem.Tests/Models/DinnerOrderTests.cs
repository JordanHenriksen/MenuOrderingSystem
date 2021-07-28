using MenuOrderingSystem.Helpers;
using MenuOrderingSystem.Models;
using MenuOrderingSystem.Models.Types;
using System;
using System.Collections.Generic;
using Xunit;

namespace MenuOrderingSystem.Tests.Models
{
    public class DinnerOrderTests
    {
        [Fact]
        public void CreateOrder_InvalidMenuItems_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var result = dinnerOrder.CreateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void CreateOrder_ValidMenuItems_ReturnsOrderIsValidTrueAndDisplay()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);
            var drinkOrder = dinnerOrder.GetDrinkOrder();

            var expected = $"{DinnerType.Main.GetDisplayName()}, {DinnerType.Side.GetDisplayName()}, {drinkOrder}, {DinnerType.Dessert.GetDisplayName()}";
            var result = dinnerOrder.CreateOrder();

            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Equal(expected, result.Display);
        }

        [Fact]
        public void GetDessertItems_NullMenuItems_Throws()
        {
            IEnumerable<int> menuItems = null;
            var dinnerOrder = new DinnerOrderTest(menuItems);

            Assert.Throws<ArgumentNullException>(() =>
                dinnerOrder.GetDessertItemsTest());
        }

        [Fact]
        public void GetDessertItems_ValidMenuItems_ReturnsDesserts()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 4 };
            var dinnerOrder = new DinnerOrderTest(menuItems);

            var results = dinnerOrder.GetDessertItemsTest();

            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.DoesNotContain(1, results);
            Assert.DoesNotContain(2, results);
            Assert.DoesNotContain(3, results);
            Assert.Contains(4, results);
        }

        [Fact]
        public void GetDrinkOrder_EmptyDrink_ReturnsWater()
        {
            const string WATER = "Water";
            IEnumerable<int> menuItems = new List<int> { 1, 2, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var result = dinnerOrder.GetDrinkOrder();

            Assert.NotEmpty(result);
            Assert.Equal(WATER, result);
        }

        [Fact]
        public void GetDrinkOrder_ValidDrink_ReturnsDrink()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var expected = dinnerOrder.GetDrinkOrder();
            var result = dinnerOrder.GetDrinkOrder();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValidateOrder_EmptyMainAndValidSideAndValidDrinkAndValidDessert_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 2, 3, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Main is missing";
            var result = dinnerOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_EmptyMainAndEmptySideAndValidDrinkAndValidDessert_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 3, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Main is missing, side is missing";
            var result = dinnerOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_MultipleMainsAndEmptySideAndValidDrinkAndValidDessert_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 3, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} {DinnerType.Main.GetDisplayName()} {Constants.MULTIPLE_ITEMS}, side is missing";
            var result = dinnerOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_MultipleMainsAndValidSideAndValidDrinkAndValidDessert_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 2, 3, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} {DinnerType.Main.GetDisplayName()} {Constants.MULTIPLE_ITEMS}";
            var result = dinnerOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_ValidMainAndEmptySideAndValidDrinkAndValidDessert_ReturnsOrderIsValidFalse()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 3, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var expectedDisplay = $"{Constants.UNABLE_TO_PROCESS} Side is missing";
            var result = dinnerOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.Equal(expectedDisplay, result.Display);
        }

        [Fact]
        public void ValidateOrder_ValidMainAndValidSideAndValidDrinkAndValidDessert_ReturnsOrderIsValidTrue()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 4 };
            var dinnerOrder = new DinnerOrder(menuItems);

            var result = dinnerOrder.ValidateOrder();

            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.Empty(result.Display);
        }

        [Fact]
        public void ValidateDessertItems_EmptyDessert_ReturnsString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3 };
            var dinnerOrder = new DinnerOrderTest(menuItems);

            var result = dinnerOrder.ValidateDessertItemsTest(string.Empty);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void ValidateDessertItems_MultipleDessert_ReturnsString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 4, 4 };
            var dinnerOrder = new DinnerOrderTest(menuItems);

            var result = dinnerOrder.ValidateDessertItemsTest(string.Empty);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void ValidateDessertItems_ValidDessert_ReturnsEmptyString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 4 };
            var dinnerOrder = new DinnerOrderTest(menuItems);

            var result = dinnerOrder.ValidateDessertItemsTest(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void ValidateDrinkItems_ValidDrinks_ReturnsEmptyString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 4 };
            var dinnerOrder = new DinnerOrderTest(menuItems);

            var result = dinnerOrder.ValidateDrinkItemsTest(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void ValidateDrinkItems_MultipleDrinks_ReturnsString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 3, 4 };
            var dinnerOrder = new DinnerOrderTest(menuItems);

            var result = dinnerOrder.ValidateDrinkItemsTest("test");

            Assert.NotEmpty(result);
        }

        [Fact]
        public void ValidateSideItems_ValidSides_ReturnsEmptyString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 4 };
            var dinnerOrder = new DinnerOrderTest(menuItems);

            var result = dinnerOrder.ValidateSideItemsTest(string.Empty);

            Assert.Empty(result);
        }

        [Fact]
        public void ValidateSideItems_MultipleSides_ReturnsString()
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 2, 3, 4 };
            var dinnerOrder = new DinnerOrderTest(menuItems);

            var result = dinnerOrder.ValidateSideItemsTest("test");

            Assert.NotEmpty(result);
        }
    }

    public class DinnerOrderTest : DinnerOrder
    {
        public DinnerOrderTest(IEnumerable<int> menuItems)
            : base(menuItems)
        {

        }

        public IEnumerable<int> GetDessertItemsTest()
        {
            return base.GetDessertItems();
        }

        public string ValidateDessertItemsTest(string display)
        {
            return base.ValidateDessertItems(display);
        }

        public string ValidateDrinkItemsTest(string display)
        {
            return base.ValidateDessertItems(display);
        }

        public string ValidateSideItemsTest(string display)
        {
            return base.ValidateSideItems(display);
        }
    }
}

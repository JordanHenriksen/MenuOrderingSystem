using MenuOrderingSystem.Models;
using MenuOrderingSystem.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace MenuOrderingSystem.Tests.Models
{
    public class MenuOrderBaseTests
    {
        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void CheckMainAndSideItems_NullMenuItems_Throws(MenuType type)
        {
            IEnumerable<int> menuItems = null;
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            Assert.Throws<ArgumentNullException>(() =>
                menuOrderBase.CheckMainAndSideItems());
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void CheckMainAndSideItems_EmptyMainItems_ReturnsDisplayMessage(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 2, 3 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            var expected = $"{Constants.UNABLE_TO_PROCESS} Main is missing";
            var result = menuOrderBase.CheckMainAndSideItems();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void CheckMainAndSideItems_MultipleMainItems_ReturnsDisplayMessage(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2, 3, 1 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);
            
            var expected = menuOrderBase.GetMultipleMainDisplayTest();
            var result = menuOrderBase.CheckMainAndSideItems();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void CheckMainAndSideItems_EmptySideItems_ReturnsDisplayMessage(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 1 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            var expected = $"{Constants.UNABLE_TO_PROCESS} Side is missing";
            var result = menuOrderBase.CheckMainAndSideItems();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void CheckMainAndSideItems_EmptyMainItemsAndEmptySideItems_ReturnsDisplayMessage(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 3 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);
            
            var expected = $"{Constants.UNABLE_TO_PROCESS} Main is missing, side is missing";
            var result = menuOrderBase.CheckMainAndSideItems();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void CheckMainAndSideItems_MultipleMainItemsAndEmptySideItems_ReturnsDisplayMessage(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            var expected = menuOrderBase.GetMultipleMainDisplayTest();
            expected = $"{expected}, side is missing";

            var result = menuOrderBase.CheckMainAndSideItems();

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void CheckMainAndSideItems_ValidMenuItems_ReturnsEmptyDisplayMessage(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 1, 2 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);
            
            var result = menuOrderBase.CheckMainAndSideItems();

            Assert.Empty(result);            
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void GetDrinkItems_NullMenuItems_Throws(MenuType type)
        {
            IEnumerable<int> menuItems = null;
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            Assert.Throws<ArgumentNullException>(() =>
                menuOrderBase.GetDrinkItems());
        }        

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void GetDrinkItems_ValidMenuItems_ReturnsDrinks(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 2, 3, 3, 2 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            var results = menuOrderBase.GetDrinkItems();

            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.DoesNotContain(1, results);
            Assert.DoesNotContain(2, results);
            Assert.Contains(3, results);            
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void GetMainItems_NullMenuItems_Throws(MenuType type)
        {
            IEnumerable<int> menuItems = null;
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            Assert.Throws<ArgumentNullException>(() =>
                menuOrderBase.GetMainItems());
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void GetMainItems_ValidMenuItems_ReturnsDrinks(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 2, 3, 3, 2 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            var results = menuOrderBase.GetMainItems();

            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.DoesNotContain(3, results);
            Assert.DoesNotContain(2, results);
            Assert.Contains(1, results);
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void GetSideItems_NullMenuItems_Throws(MenuType type)
        {
            IEnumerable<int> menuItems = null;
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            Assert.Throws<ArgumentNullException>(() =>
                menuOrderBase.GetSideItems());
        }        

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void GeSideItems_ValidMenuItems_ReturnsDrinks(MenuType type)
        {
            IEnumerable<int> menuItems = new List<int> { 1, 1, 2, 3, 3, 2 };
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            var results = menuOrderBase.GetSideItems();

            Assert.NotNull(results);
            Assert.NotEmpty(results);
            Assert.DoesNotContain(3, results);
            Assert.DoesNotContain(1, results);
            Assert.Contains(2, results);
        }

        [Theory]
        [InlineData(MenuType.Breakfast, "Eggs")]
        [InlineData(MenuType.Lunch, "Sandwich")]
        [InlineData(MenuType.Dinner, "Steak")]
        public void GetMultipleMainDisplay_Valid_ReturnsDisplayName(MenuType type, string expectedName)
        {
            IEnumerable<int> menuItems = null;
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            var result = menuOrderBase.GetMultipleMainDisplayTest();
            var expected = $"{Constants.UNABLE_TO_PROCESS} {expectedName} {Constants.MULTIPLE_ITEMS}";

            Assert.NotEmpty(result);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(MenuType.Breakfast)]
        [InlineData(MenuType.Lunch)]
        [InlineData(MenuType.Dinner)]
        public void ValidateItems_NullMenuItems_Throws(MenuType type)
        {
            IEnumerable<int> menuItems = null;
            var menuOrderBase = new MenuOrderBaseTest(type, menuItems);

            Assert.Throws<ArgumentNullException>(() =>
                menuOrderBase.ValidateItemsTest());
        }
    }    

    public class MenuOrderBaseTest : MenuOrderBase
    {
        public MenuOrderBaseTest(MenuType type, IEnumerable<int> menuItems)
            : base (type, menuItems)
        {

        }

        public string GetMultipleMainDisplayTest()
        {
            return base.GetMultipleMainDisplay();
        }

        public void ValidateItemsTest()
        {
            base.ValidateMenuItems();
        }
    }
}

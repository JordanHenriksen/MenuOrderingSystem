namespace MenuOrderingSystem.Models.Interfaces
{
    public interface IMenuOrder
    {
        Order CreateOrder();
        string GetDrinkOrder();
        Order ValidateOrder();
    }
}

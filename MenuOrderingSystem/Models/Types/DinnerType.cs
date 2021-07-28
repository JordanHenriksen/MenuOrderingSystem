using System.ComponentModel.DataAnnotations;

namespace MenuOrderingSystem.Models.Types
{
    public enum DinnerType
    {
        [Display(Name = "Steak")]
        Main = 1,

        [Display(Name = "Potatoes")]
        Side = 2,

        [Display(Name = "Wine")]
        Drink = 3,

        [Display(Name = "Cake")]
        Dessert = 4
    }
}

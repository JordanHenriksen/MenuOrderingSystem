using System.ComponentModel.DataAnnotations;

namespace MenuOrderingSystem.Models.Types
{
    public enum LunchType
    {
        [Display(Name = "Sandwich")]
        Main = 1,

        [Display(Name = "Chips")]
        Side = 2,

        [Display(Name = "Soda")]
        Drink = 3
    }
}

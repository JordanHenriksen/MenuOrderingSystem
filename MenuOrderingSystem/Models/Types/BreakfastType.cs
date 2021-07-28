using System.ComponentModel.DataAnnotations;

namespace MenuOrderingSystem.Models.Types
{
    public enum BreakfastType
    {
        [Display(Name = "Eggs")]
        Main = 1,

        [Display(Name = "Toast")]
        Side = 2,

        [Display(Name = "Coffee")]
        Drink = 3
    }
}

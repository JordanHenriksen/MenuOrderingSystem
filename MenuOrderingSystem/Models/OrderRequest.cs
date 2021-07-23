using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace MenuOrderingSystem.Models
{
    public class OrderRequest
    {
        public IEnumerable<int> MenuItems { get; set; }        
        public MenuType Menu { get; set; }
    }
}

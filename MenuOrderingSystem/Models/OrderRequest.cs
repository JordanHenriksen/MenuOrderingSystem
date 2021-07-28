using System.Collections.Generic;

namespace MenuOrderingSystem.Models
{
    public class OrderRequest
    {
        public string Menu { get; set; }
        public IEnumerable<int> MenuItems { get; set; }        
    }
}

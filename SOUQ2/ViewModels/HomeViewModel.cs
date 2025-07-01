using SOUQ2.Models;
using System.Collections.Generic;

namespace SOUQ2.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Service> Services { get; set; }  
        public List<Product> Products { get; set; }
    }
}

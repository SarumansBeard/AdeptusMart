
using AdeptusMart01.Core.Entities;

namespace AdeptusMart04.WebUI.Models
{
    public class ProductDetailsViewModel
    {
        public Product ProductDetail { get; set; }
        public List<Product> RandomProducts { get; set; } = new();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}

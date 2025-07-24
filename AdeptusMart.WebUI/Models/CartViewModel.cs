using AdeptusMart01.Core.Entities;

namespace AdeptusMart04.WebUI.Models
{
    public class CartViewModel
    {
        public CartItem CartItem { get; set; } = new CartItem();
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public Cart Cart { get; set; } = new Cart();
        public List<Product> Products { get; set; } = new List<Product>();
    }
}

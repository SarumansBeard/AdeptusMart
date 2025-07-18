using AdeptusMart01.Core.Entities;

namespace AdeptusMart04.WebUI.Models
{
    public class CartViewModel
    {
        public List<CartItem> cartItems { get; set; } = new List<CartItem>();
    }
}

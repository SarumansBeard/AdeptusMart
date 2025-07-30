using AdeptusMart01.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdeptusMart04.Api.DTOs
{
    public class CartItemDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid CartId { get; set; }
        public int Quantity { get; set; }        
    }
}

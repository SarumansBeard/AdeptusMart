using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdeptusMart01.Core.Entities
{
    public class CartItem 
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(CartId))]
        public virtual Cart Cart { get; set; }
    }
}

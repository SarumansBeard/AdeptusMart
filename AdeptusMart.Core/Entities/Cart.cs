using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdeptusMart01.Core.Entities
{
    public class Cart : BaseEntity
    {
        public Cart()
        {            
            Receipts = new HashSet<Receipt>();
            CartItems = new HashSet<CartItem>();
        }
                
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
                

        public Guid? UserId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(UserId))]
        public virtual Account Accounts { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }


    } 
    
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdeptusMart01.Core.Entities
{
    public class Receipt : BaseEntity
    {     
        public Guid CartId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(CartId))]
        public virtual Cart Cart { get; set; }

    }
}

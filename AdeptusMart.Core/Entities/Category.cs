
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdeptusMart01.Core.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string Name { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }

        
        public virtual ICollection<Product> Products { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdeptusMart01.Core.Entities
{
    public class Product :BaseEntity
    {
        [Required(ErrorMessage = "Bu alan gereklidir.")]      
        
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        public string Currency { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        public decimal Star { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        public string Details { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        public string Information { get; set; }


        [ValidateNever]
        public string ImageUrl1 { get; set; }

        [ValidateNever]
        public string ImageUrl2 { get; set; }

        [ValidateNever]
        public string ImageUrl3 { get; set; }

        [ValidateNever]
        public string ImageUrl4 { get; set; }     

        
        public Guid CategoryId { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}

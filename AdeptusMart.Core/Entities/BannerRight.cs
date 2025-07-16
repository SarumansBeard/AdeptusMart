

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdeptusMart01.Core.Entities
{
    public class BannerRight : BaseEntity
    {
        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string MainTitle { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string MainTitleColorCode { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string Title { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string TitleColorCode { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string GoShopButtonText { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string GoShopButtonTextColorCode { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string ButtonText { get; set; }


        [Required(ErrorMessage = "Bu alan gereklidir.")]
        [MinLength(5, ErrorMessage = "Bu alan en az 5 karakter olabilir.")]
        [MaxLength(50, ErrorMessage = "Bu alan en fazla 50 karakter olabilir.")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Bu alan en az 5 en fazla 50 karakter olabilir.")]
        [DataType(DataType.Text, ErrorMessage = "Veri tipi hatalı.")]
        public string ButtonTextColorCode { get; set; }


        [ValidateNever]
        public string BackGroundUrl { get; set; }


        [ValidateNever]
        public string? VideoUrl { get; set; }
    }
}

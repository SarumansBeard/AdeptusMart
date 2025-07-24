using AdeptusMart01.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AdeptusMart05.Api.DTOs
{
    public class ProductDTO
    {       

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }        
        public string Currency { get; set; }        
        public decimal Star { get; set; }     
        public string Details { get; set; }       
        public string Information { get; set; }        
        public string ImageUrl1 { get; set; }        
        public string ImageUrl2 { get; set; }      
        public string ImageUrl3 { get; set; }        
        public string ImageUrl4 { get; set; }
        public CategoryDTO Category { get; set; }

        
    }
}

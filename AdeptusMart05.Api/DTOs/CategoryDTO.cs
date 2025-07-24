using AdeptusMart01.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace AdeptusMart05.Api.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public string ImageUrl { get; set; }       
    }
}

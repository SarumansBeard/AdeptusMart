using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdeptusMart01.Core.Entities;

namespace AdeptusMart04.WebUI.Models
{
    public class ShopgridViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();

        public List<Category> Categories { get; set; } = new List<Category>();

       

    }
}

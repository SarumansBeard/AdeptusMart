using AdeptusMart01.Core.Entities;

namespace AdeptusMart04.WebUI.Models
{
    public class RegisterViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public Account Account{ get; set; } = new Account();
        public bool? IsRegisterSuccess { get; set; }
    }
}

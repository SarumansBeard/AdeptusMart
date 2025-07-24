using AdeptusMart01.Core.Entities;

namespace AdeptusMart06.WebUI.Models
{
    public class AccountViewModel
    {
        public List<Category> Categories { get; set; } = new List<Category>();

        public Account Account { get; set; } = new Account();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdeptusMart01.Core.Entities
{
    public class Account : BaseEntity
    {
        public Account()
        {
            Carts = new HashSet<Cart>();           
        }             

        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? FullName { get; set; }
        public string? Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }
        public bool IsSignIn { get; set; }
        public string SessionId { get; set; }


        public virtual ICollection<Cart> Carts { get; set; }
        


    }
}

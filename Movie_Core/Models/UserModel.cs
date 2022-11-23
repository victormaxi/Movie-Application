
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Core.Models
{
    public class UserModel : IdentityUser
    {
       // public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
       // public string UserName { get; set; } = null!;
        //public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        //public string PhoneNumber { get; set; } = null!;
        //public string Password { get; set; } = null!;
        //public DateTime DateCreated { get; set; } = DateTime.Now;
        //public string FullName { get; set; } = null!;
    }
}

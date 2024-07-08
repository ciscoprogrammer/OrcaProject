using Microsoft.AspNetCore.Identity;

namespace OrcaProject.Models
{
    public class User : IdentityUser
    {
        //public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } // Consider storing password hashes instead
    }
}

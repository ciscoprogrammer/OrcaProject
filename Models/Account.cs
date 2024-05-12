using System.Text.Json.Serialization;

namespace OrcaProject.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; } // e.g., Admin, User, Guest
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; } // Foreign key relation to User
        

    }
}

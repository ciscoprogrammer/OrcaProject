namespace OrcaProject.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string Payload { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; } // Navigation property
    }
}

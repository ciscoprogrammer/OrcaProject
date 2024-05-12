namespace OrcaProject.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderDetails { get; set; } // Detailed string or a complex type if needed
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; } // Foreign key relation to User
                                        //public User User { get; set; } // Navigation property for User
        public bool IsClosed { get; set; }
    }
}

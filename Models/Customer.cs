namespace Projektna.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public ApplicationUser? CustomerAcc {get; set;}
    }
}

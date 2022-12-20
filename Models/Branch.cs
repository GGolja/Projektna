namespace Projektna.Models
{
    public class Branch
    {
        public int ID { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        //public ICollection<Vehicle> Stock { get; set;}
        public int SellerID { get; set; }
        public Seller BranchSeller { get; set;}
        public ApplicationUser? BranchAdmin {get; set;}
        public int PhoneNumber {get; set;}
        public String Email { get; set; }
    }
}

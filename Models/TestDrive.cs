namespace Projektna.Models
{
    public class TestDrive
    {
        public int ID {get; set;}
        public int BranchID { get; set; }
        public int VehicleID { get; set; }
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public Branch Branch { get; set; }
        public Vehicle Vehicle { get; set; }
        public Customer Customer { get; set; }
    }
}
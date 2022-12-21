namespace Projektna.Models
{
    public class Vehicle
    {
        public int ID { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        public DateTime ModelYear { get; set; }
        public int TrimID { get; set; }
        public Trim TrimLevel { get; set; }
        public bool Sold { get; set; } = false;


    }
}

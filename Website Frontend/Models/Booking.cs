namespace Website_Frontend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CentreName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string BookedBy { get; set; }
    }
}

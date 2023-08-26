using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BookedRide
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public int OfferRideId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; } 
        public DateTime Date { get; set; }
        public string SlotTime { get; set; }
        public float Charge { get; set; }  
        public int NoOfSeats { get; set; }
        
    }
}

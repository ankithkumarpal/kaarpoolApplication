using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class RideDetails
    {
        [Key]
        public int Id { get; set; }
        public string Source { get ; set; } 
        public string Destination { get ; set; } 
        public float Distance { get ; set ; }
        public DateTime Date { get; set; }
        public string SlotTime { get; set; }
        public int Capacity { get ; set ; }
        public int FairPerKm { get ; set ; }
        public int OwnerId { get ; set ; }
        public string Email { get; set; }
        public List<Location> Stops { get ; set ; }
    }
}

namespace ViewLayer
{
    public class BookingRequestDTO
    {
        public string Email { get; set; }
        public int RideId { get; set; }
        public DateTime Date { get; set; }
        public string SlotTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int OwnerId { get; set; }
        public int UserId { get; set; }
        public float Fair { get; set; }
        public int NoOfPassenger { get; set; }
    }
}

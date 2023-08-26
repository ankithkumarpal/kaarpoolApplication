namespace ViewLayer
{
    public class RideRequestDTO
    {
        public string SourceName { get; set; }
        public string DestinationName { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string SlotTime { get; set; }
        public int NoOfPassenger { get; set; }

    }
}

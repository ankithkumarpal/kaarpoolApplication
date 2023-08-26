using Models;
using ViewLayer;
namespace Interfaces
{
    public interface IBookRideRepository
    {
        public List<RideDetails> GetMatchedRides(RideRequestDTO rideRequest);
        public RideDetails GetSelectedRideDetail(BookingRequestDTO bookingRequest);
        public List<BookedRide> GetBookedRides(int id);
        public bool AddBookedRide(BookedRide bookRide);
        public bool UpdateRideDetail(RideDetails rideDetails);
    }
}

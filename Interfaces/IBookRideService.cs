using Models;
using ViewLayer;

namespace Interfaces
{
    public interface IBookRideService
    {
        List<RideDetailDTO> GetAvailableRides(RideRequestDTO rideRequest);
        void BookRide(BookingRequestDTO bookingRequest);
        public List<BookedRideDTO> GetBookedRides(int id);
    }
}

using Models;
namespace Interfaces
{
    public interface IOfferRideRepository
    {
        public int OfferRide(RideDetails rideDetails);
        public List<RideDetails> GetOfferedRides(int id);
    }
}

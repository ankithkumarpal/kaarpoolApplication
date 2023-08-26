
using ViewLayer;

namespace Interfaces
{
    public interface IOfferRideService
    {
         int  OfferRide(RideDetailDTO rideDetails);
         public List<RideDetailDTO> GetOfferedRide(int id);
    }
}

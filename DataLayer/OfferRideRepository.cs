using Interfaces;
using Models;

namespace DataLayer
{
    public class OfferRideRepository : IOfferRideRepository
    {
        private CarPoolContext _context { get; set; }
        private ILog _logger; 
        public OfferRideRepository(CarPoolContext context , ILog logger ) 
        { 
            _context = context;
            _logger= logger;
        }
        public int OfferRide(RideDetails rideDetails)
        {
            try
            {
                _context.RideDetails.Add(rideDetails);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        public List<RideDetails> GetOfferedRides(int id)
        {
            return _context.RideDetails.Where(u => u.OwnerId == id).ToList();
        }

    }
}

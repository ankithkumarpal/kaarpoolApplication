using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewLayer;

namespace DataLayer
{
    public class BookRideRepository : IBookRideRepository
    {
        private CarPoolContext _context { get; set; }
        private ILog _logger; 
        public  BookRideRepository(CarPoolContext context , ILog logger)
        {
            _context= context;
            _logger= logger;
        }
        public List<RideDetails> GetMatchedRides(RideRequestDTO rideRequest)
        {
            return  _context.RideDetails.Where(r => r.Date == rideRequest.Date && r.SlotTime == rideRequest.SlotTime).Include("Stops").ToList();
        }
        public RideDetails GetSelectedRideDetail(BookingRequestDTO bookingRequest)
        {
            return  _context.RideDetails.Where(r => r.Id == bookingRequest.RideId).Include("Stops").FirstOrDefault();
        }
        public List<BookedRide> GetBookedRides(int id)
        {
            return _context.BookRides.Where(u => u.UserId == id).ToList();
        }
        public bool AddBookedRide(BookedRide bookRide)
        {
            try
            {
                _context.BookRides.Add(bookRide);
                return true;    
            }catch(Exception ex)
            {
                throw ex; 
            }
        }

        public bool UpdateRideDetail(RideDetails rideDetail)
        {
            try
            {
                _context.RideDetails.Update(rideDetail);
                _context.Locations.UpdateRange(rideDetail.Stops);
                return true;
            }
            catch(Exception ex )
            {
                throw ex; 
            }
        }
    }

}

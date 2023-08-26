using Interfaces;
using Models;
using ViewLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookRideController : ControllerBase
    {
        IBookRideService _bookRide; 
        public BookRideController(IBookRideService bookRide)
        {
            _bookRide = bookRide;
        }
        [HttpPost("getavailablerides")]
        public IActionResult GetAvailableRide(RideRequestDTO rideRequest)
        {
            try
            {
                List<RideDetailDTO> response = _bookRide.GetAvailableRides(rideRequest);
                return Ok(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        [HttpPost("bookride")]
        public void BookRide(BookingRequestDTO bookingRequest)
        {
            try
            {
                _bookRide.BookRide(bookingRequest);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("getbookedride")]
        public IActionResult  GetBookedRide(int id)
        {
            try
            {
                List<BookedRideDTO> response = _bookRide.GetBookedRides(id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

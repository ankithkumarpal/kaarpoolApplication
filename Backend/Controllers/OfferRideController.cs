using Interfaces;
using ViewLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OfferRideController : ControllerBase
    {
        IOfferRideService _OfferRideService;
        public OfferRideController(IOfferRideService offerRideService)
        {
            this._OfferRideService = offerRideService;
        }
        [HttpPost("/offerride")]
        public  void OfferRideDetails(RideDetailDTO rideDetails)
        {
            try
            {
               _OfferRideService.OfferRide(rideDetails);
            }
            catch(Exception ex)
            {
               throw ex;
            }
        }
        [HttpPost("/getOfferedRides")]
        public IActionResult  GetBookedRide(int id)
        {
            try
            {
                List<RideDetailDTO> response = _OfferRideService.GetOfferedRide(id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

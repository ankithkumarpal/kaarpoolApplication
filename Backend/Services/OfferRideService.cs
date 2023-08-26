using AutoMapper;
using Interfaces;
using Models;
using ViewLayer;

namespace CarPool.Services
{
    public class OfferRideService : IOfferRideService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private IMapper _mapper { get; set; }
        private ILog _logger { get; set; }  
        public OfferRideService(IUnitOfWork unitOfWork , IMapper mapper , ILog logger )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public int  OfferRide(RideDetailDTO rideDetails)
        {
            try
            {
                RideDetails ride = _mapper.Map<RideDetails>(rideDetails);
                int res =  _unitOfWork.offerRideRepository.OfferRide(ride);
                _unitOfWork.SaveChanges();
                return res;

            }
            catch(Exception ex) {
                _logger.Log(ex.Message);
                throw ex;
            }
        }
        public List<RideDetailDTO> GetOfferedRide(int id)
        {
            try
            {
                List<RideDetailDTO> rideDetails = new List<RideDetailDTO>();
                List<RideDetails> rides = _unitOfWork.offerRideRepository.GetOfferedRides(id);
                foreach (var ride in rides)
                {
                    var rideDTO = _mapper.Map<RideDetailDTO>(ride);
                    rideDetails.Add(rideDTO);
                }
                return rideDetails;
            }
            catch(Exception ex) {
                _logger.Log(ex.Message);
                throw ex;
            }  
        }
    }
}


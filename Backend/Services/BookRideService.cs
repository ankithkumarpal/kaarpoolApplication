using AutoMapper;
using Interfaces;
using Models;
using ViewLayer;

namespace CarPool.Services
{
    public class BookRideService : IBookRideService
    {
        private IUnitOfWork _unitOfWork; 
        private IMapper _mapper { get; set; }
        private ILog _logger { get; set; }
        public BookRideService(IUnitOfWork unitOfWork, IMapper mapper , ILog logger )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public List<RideDetailDTO> GetAvailableRides(RideRequestDTO rideRequest)
        {
            List<RideDetailDTO> availableRides = new List<RideDetailDTO>();
            try
            {
                List<RideDetails> rideDetailsList = _unitOfWork.bookRideRepository.GetMatchedRides(rideRequest);
                foreach (var ride in rideDetailsList)
                {
                    string  fromLocation = null;
                    string  toLocation = null;

                    List<Location> stops = ride.Stops.ToList();
                    int seatOccupied = 0;
                    for (int i = 0; i < stops.Count(); i++)
                    {
                        if (stops[i].Name == rideRequest.SourceName)
                        {
                            fromLocation = stops[i].Name;
                        }
                        else if (fromLocation != null && stops[i].Name == rideRequest.DestinationName)
                        {
                            toLocation = stops[i].Name;
                            break;
                        } 
                        if (fromLocation != null)
                        {
                            seatOccupied = Math.Max(seatOccupied , ride.Stops[i].Occupency);
                        }
                    }
                    if (fromLocation == rideRequest.SourceName && toLocation == rideRequest.DestinationName && (ride.Capacity - seatOccupied) >= rideRequest.NoOfPassenger)
                    {
                        RideDetailDTO rideDTO = _mapper.Map<RideDetailDTO>(ride);
                        availableRides.Add(rideDTO);
                    }
                }
                return availableRides;
            }
            catch(Exception ex)
            {
                _logger.Log(ex.Message);
                throw ex;
            }
        }
        public void BookRide(BookingRequestDTO bookingRequest)
        {
            try
            {
                RideDetails rideDetail = _unitOfWork.bookRideRepository.GetSelectedRideDetail(bookingRequest);
                if (rideDetail == null) return;
                BookedRide bookRide = _mapper.Map<BookedRide>(bookingRequest);
                bool flag = false;
                for (int i = 0; i < rideDetail.Stops.Count; i++)
                {   
                    
                    if (rideDetail.Stops[i].Name == bookingRequest.Source)
                    {   
                        flag = true;
                        rideDetail.Stops[i].Occupency += bookingRequest.NoOfPassenger;
                        continue ;
                    }

                    if (rideDetail.Stops[i].Name == bookingRequest.Destination) { break; }
                    if (flag == true)
                    {
                        rideDetail.Stops[i].Occupency += bookingRequest.NoOfPassenger ;
                    }
                }
                if (rideDetail == null) { return; }
                _unitOfWork.bookRideRepository.UpdateRideDetail(rideDetail);
                _unitOfWork.bookRideRepository.AddBookedRide(bookRide);
                _unitOfWork.SaveChanges();
            }catch(Exception ex)
            {
                _logger.Log(ex.Message);
                throw ex;
            }
        }

        public List<BookedRideDTO> GetBookedRides(int id)
        {
            List<BookedRideDTO> rideDetails = new List<BookedRideDTO>();
            List<BookedRide> rides = _unitOfWork.bookRideRepository.GetBookedRides(id);
            foreach (var ride in rides)
            {
                BookedRideDTO rideDTO = _mapper.Map<BookedRideDTO>(ride);
                rideDetails.Add(rideDTO);
            }
            return rideDetails;
        }
    }
}
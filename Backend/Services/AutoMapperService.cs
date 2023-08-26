using AutoMapper;
using Models;
using ViewLayer;

namespace CarPool.Services
{
    public class AutoMapperService : Profile
    {
        public AutoMapperService() {
            CreateMap<RideDetailDTO, RideDetails>().ReverseMap().ForMember(des => des.Id, act => act.MapFrom(src => src.Id));
            CreateMap<BookingRequestDTO, BookedRide>().ForMember(dest => dest.Charge, act => act.MapFrom(src => src.Fair)).
                  ForMember(dest => dest.OfferRideId, act => act.MapFrom(src => src.OwnerId)).
                  ForMember(dest => dest.NoOfSeats, act => act.MapFrom(src => src.NoOfPassenger)).ReverseMap();
            CreateMap<BookedRide , BookedRideDTO>().ReverseMap();
            CreateMap<UserDTO,User> ().ReverseMap();
            }
    }
}

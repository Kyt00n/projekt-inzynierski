using AutoMapper;
using LTL.Manager.Domain.Enums;
using LTL.Manager.Domain.Responses.OrderResponse;
using LTL.Manager.Domain.Responses.TripResponse;
using LTL.Manager.Infrastructure.Persistence.Models;

namespace LTL.Manager.Infrastructure.Mapper;

public class TripProfile : Profile
{
  public TripProfile()
  {
    CreateMap<AcceptAssignmentOrderResponse, Trip>()
      .ForMember(dest => dest.TripId, opt => opt.MapFrom(_ => Guid.NewGuid()))
      .ForMember(dest => dest.StartTime, opt => opt.MapFrom(_ => DateTime.Now))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => TripStatus.InProgress))
      .ForMember(dest => dest.Orders, opt => opt.Ignore()).ReverseMap();

    CreateMap<Trip, AssignTripResponse>();
    
  }
}

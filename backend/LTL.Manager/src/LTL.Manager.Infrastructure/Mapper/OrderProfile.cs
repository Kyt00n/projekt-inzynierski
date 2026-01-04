using AutoMapper;
using LTL.Manager.Domain.Enums;
using LTL.Manager.Domain.Requests.OrderRequests;
using LTL.Manager.Domain.Responses.OrderResponse;
using LTL.Manager.Domain.Responses.TripResponse;
using LTL.Manager.Infrastructure.Persistence.Models;

namespace LTL.Manager.Infrastructure.Mapper;

public class OrderProfile : Profile
{
  public OrderProfile()
  {
    CreateMap<CreateOrderRequest, Order>()
      .ForMember(d => d.OrderId, opt => opt.MapFrom(_ => Guid.NewGuid()))
      .ForMember(d => d.Status, opt => opt.MapFrom(_ => OrderStatus.Created))
      .ForMember(d => d.Loads, opt => opt.MapFrom(s => s.Loads))
      .ForMember(d => d.DriverNotes, opt => opt.MapFrom(_ => new List<DriverNote>()))
      .AfterMap((src, dest, context) =>
      {
        foreach (var load in dest.Loads)
        {
          load.OrderId = dest.OrderId;
          load.Order = dest;
        }
      }).ReverseMap();

    CreateMap<LTL.Manager.Domain.Entities.Load, Load>()
      .ForMember(d => d.LoadId, opt => opt.MapFrom(_ => Guid.NewGuid()))
      .ForMember(d => d.Id, opt => opt.Ignore())
      .ForMember(d => d.OrderId, opt => opt.Ignore()).ReverseMap();

    CreateMap<Order, GetOrderResponse>()
      .ForMember(d => d.Loads, opt => opt.MapFrom(s => s.Loads.Where(l => l.OrderId == s.OrderId).ToList()))
      .ForMember(d => d.DriverNotes, opt => opt.MapFrom(s => s.DriverNotes.Where(n => n.OrderId == s.OrderId).ToList()));

    CreateMap<UpdateOrderRequest, Order>()
      .ForMember(d => d.Loads, opt =>
      {
        opt.PreCondition(src => src.Loads != null);
        opt.MapFrom(src => src.Loads);
      })
      .AfterMap((src, dest) =>
      {
        if (src.Loads == null || dest.Loads == null) return;
        foreach (var load in dest.Loads)
        {
          load.OrderId = dest.OrderId;
          load.Order = dest;
        }
      })
      .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    CreateMap<Order, AssignTripResponse>();
  }

}

using AutoMapper;
using LTL.Manager.Domain.Enums;
using LTL.Manager.Domain.Requests.OrderRequests;
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
  }
  
}

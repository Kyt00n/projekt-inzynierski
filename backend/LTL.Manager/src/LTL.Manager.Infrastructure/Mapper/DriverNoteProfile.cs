using AutoMapper;
using LTL.Manager.Domain.Requests.OrderRequests;
using LTL.Manager.Infrastructure.Persistence.Models;

namespace LTL.Manager.Infrastructure.Mapper;

public class DriverNoteProfile : Profile
{
  public DriverNoteProfile()
  {
    CreateMap<AddDriverNoteRequest, DriverNote>()
      .ForMember(dest => dest.DriverNoteId, opt => opt.MapFrom(_ => Guid.NewGuid()))
      .ReverseMap();
    CreateMap<DriverNote, LTL.Manager.Domain.Entities.DriverNote>()
      .ReverseMap();
  }
}

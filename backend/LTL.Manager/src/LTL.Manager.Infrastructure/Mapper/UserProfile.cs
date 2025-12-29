using AutoMapper;
using LTL.Manager.Domain.Requests.UserRequests;
using LTL.Manager.Domain.Responses.UserResponses;
using LTL.Manager.Infrastructure.Persistence.Models;

namespace LTL.Manager.Infrastructure.Mapper;

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<AddUserRequest, User>()
      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => Guid.NewGuid()))
      .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => false))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Domain.Enums.DriverStatus.Offline))
      .ReverseMap();
    CreateMap<GetUserResponse, User>().ReverseMap();
    CreateMap<GetUserInternalResponse, User>().ReverseMap();
  }
}

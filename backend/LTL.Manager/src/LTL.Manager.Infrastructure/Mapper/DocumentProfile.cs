using AutoMapper;
using LTL.Manager.Domain.Requests.DocumentRequests;
using LTL.Manager.Infrastructure.Persistence.Models;

namespace LTL.Manager.Infrastructure.Mapper;

public class DocumentProfile : Profile
{
  public DocumentProfile()
  {
    CreateMap<CreateDocumentRequest, Document>()
      .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(_ => Guid.NewGuid()))
      .ReverseMap();
  }
}

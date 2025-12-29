using LTL.Manager.Domain.Responses.UserResponses;

namespace LTL.Manager.Application.Interfaces;

public interface ITokenProvider
{
  string Create(GetUserInternalResponse user);
}

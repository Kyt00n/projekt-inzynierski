using LTL.Manager.Domain.Requests.UserRequests;
using LTL.Manager.Domain.Responses.UserResponses;

namespace LTL.Manager.Application.Infrastructure;

public interface IUserRepository
{
  Task<GetUserResponse> AddUserAsync(AddUserRequest request);
  Task<GetUserDetailsResponse> GetUserDetailsAsync(Guid userId);
  Task<GetUserResponse> UpdateUserAsync(UpdateUserRequest request);
  Task ChangePasswordAsync(ChangePasswordRequest request);
  Task<IEnumerable<GetUserResponse>> GetAllUsersAsync();
}

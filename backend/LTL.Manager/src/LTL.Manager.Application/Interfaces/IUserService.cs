using LTL.Manager.Domain.Requests.UserRequests;
using LTL.Manager.Domain.Responses.UserResponses;

namespace LTL.Manager.Application.Interfaces;

public interface IUserService
{
  // Task<GetUserResponse> GetUserAsync(GetUserRequest request);
  // Task<GetUsersResponse> GetUsersAsync(GetUsersRequest request);
  Task<GetUserResponse> AddUserAsync(AddUserRequest user);
  Task<GetUserDetailsResponse> GetUserDetailsAsync(Guid userId);
  Task<GetUserResponse> UpdateUserAsync(UpdateUserRequest request);
  Task ChangePasswordAsync(ChangePasswordRequest request);
  Task<IEnumerable<GetUserResponse>> GetAllUsersAsync(GetUsersRequest request);
  Task<GetLoginResponse> LoginUserAsync(LoginUserRequest request);
}

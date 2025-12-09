using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Application.Interfaces;
using LTL.Manager.Domain.Requests.UserRequests;
using LTL.Manager.Domain.Responses.UserResponses;

namespace LTL.Manager.Application.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
  }


  public Task<GetUserResponse> AddUserAsync(AddUserRequest user)
  {
    return _userRepository.AddUserAsync(user);
  }

  public Task<GetUserDetailsResponse> GetUserDetailsAsync(Guid userId)
  {
    var user = _userRepository.GetUserDetailsAsync(userId);
    
    if (user == null)
    {
      throw new InvalidOperationException("User not found");
    }
    return user;
  }

  public Task<GetUserResponse> UpdateUserAsync(UpdateUserRequest request)
  {
    return _userRepository.UpdateUserAsync(request);
  }

  public Task ChangePasswordAsync(ChangePasswordRequest request)
  {
    return _userRepository.ChangePasswordAsync(request);
  }

  public Task<IEnumerable<GetUserResponse>> GetAllUsersAsync()
  {
    return _userRepository.GetAllUsersAsync();
  }
}

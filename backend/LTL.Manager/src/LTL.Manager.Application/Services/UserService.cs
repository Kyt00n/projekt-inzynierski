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


  public async Task<GetUserResponse> AddUserAsync(AddUserRequest user)
  {
    return await _userRepository.AddUserAsync(user);
  }

  public async Task<GetUserDetailsResponse> GetUserDetailsAsync(Guid userId)
  {
    var user = await _userRepository.GetUserDetailsAsync(userId);
    
    if (user == null)
    {
      throw new InvalidOperationException("User not found");
    }
    return user;
  }

  public async Task<GetUserResponse> UpdateUserAsync(UpdateUserRequest request)
  {
    return await _userRepository.UpdateUserAsync(request);
  }

  public Task ChangePasswordAsync(ChangePasswordRequest request)
  {
    return _userRepository.ChangePasswordAsync(request);
  }

  public async Task<IEnumerable<GetUserResponse>> GetAllUsersAsync()
  {
    return await _userRepository.GetAllUsersAsync();
  }
}

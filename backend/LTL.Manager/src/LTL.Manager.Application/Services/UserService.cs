using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Application.Interfaces;
using LTL.Manager.Domain.Requests.UserRequests;
using LTL.Manager.Domain.Responses.UserResponses;

namespace LTL.Manager.Application.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher _passwordHasher;
  private readonly ITokenProvider _tokenProvider;

  public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
  {
    _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
  }


  public async Task<GetUserResponse> AddUserAsync(AddUserRequest user)
  {
    var passwordHash = _passwordHasher.Hash(user.Password);
    user.PasswordHash = passwordHash;
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

  public async Task<IEnumerable<GetUserResponse>> GetAllUsersAsync(GetUsersRequest request)
  {
    return await _userRepository.GetAllUsersAsync(request);
  }

  public async Task<GetLoginResponse> LoginUserAsync(LoginUserRequest request)
  {
    try
    {
      var user = await _userRepository.GetUserInternalAsync(request.Email);
      if (!user.IsActive)
      {
        throw new InvalidOperationException("User account is not active");
      }
      var isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
      
      if (!isPasswordValid)
      {
        throw new InvalidOperationException("Invalid email or password");
      }
      var token = _tokenProvider.Create(user);
      return new GetLoginResponse()
      {
        Token = token
      };
    }
    catch (InvalidOperationException)
    {
      throw new InvalidOperationException("Invalid email or password");
    }
    
  }
}

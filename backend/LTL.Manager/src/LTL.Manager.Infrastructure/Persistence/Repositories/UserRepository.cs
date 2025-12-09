using AutoMapper;
using AutoMapper.QueryableExtensions;
using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Domain.Requests.UserRequests;
using LTL.Manager.Domain.Responses.UserResponses;
using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace LTL.Manager.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
  private readonly LtlMngrContext _context;
  private readonly IMapper _mapper;

  public UserRepository(IMapper mapper, LtlMngrContext context)
  {
    _mapper = mapper;
    _context = context;
  }


  public async Task<GetUserResponse> AddUserAsync(AddUserRequest request)
  {
    if (await _context.Users.AnyAsync(u => u.Email == request.Email))
    {
      throw new InvalidOperationException("Email is already in the database");
    }
    if (await _context.Users.AnyAsync(u => u.Username == request.Username))
    {
      throw new InvalidOperationException("Username is already in the database");
    }

    var userDb = _mapper.Map<User>(request);
    var mapperUser = _context.Users.Add(userDb);

    await _context.SaveChangesAsync();
    return _mapper.Map<GetUserResponse>(mapperUser.Entity);

  }

  public Task<GetUserDetailsResponse> GetUserDetailsAsync(Guid userId)
  {
    throw new NotImplementedException();
  }

  public Task<GetUserResponse> UpdateUserAsync(UpdateUserRequest request)
  {
    throw new NotImplementedException();
  }

  public async Task ChangePasswordAsync(ChangePasswordRequest request)
  {
    try
    {
      var user = await FindUserByIdAsync(request.UserId);
      _mapper.Map(request, user);
      await _context.SaveChangesAsync();
    }
    catch (InvalidOperationException )
    {
      throw new InvalidOperationException("Cannot change password. User not found");
    }
    catch (DbUpdateException )
    {
      throw new InvalidOperationException("Cannot change password due to database error");
    }
  }

  private async Task<User> FindUserByIdAsync(Guid userId)
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
    if (user == null)
    {
      throw new InvalidOperationException("User not found");
    }
    return user;
  }

  public Task<IEnumerable<GetUserResponse>> GetAllUsersAsync()
  {
    var list = _context.Users.OrderBy(u => u.Username).AsNoTracking();
    return list.ProjectTo<GetUserResponse>(_mapper.ConfigurationProvider)
      .ToListAsync()
      .ContinueWith(t => (IEnumerable<GetUserResponse>)t.Result);
  }
}

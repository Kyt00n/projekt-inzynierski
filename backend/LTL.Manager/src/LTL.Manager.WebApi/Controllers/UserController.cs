using System.Security.Claims;
using LTL.Manager.Application.Interfaces;
using LTL.Manager.Application.Services;
using LTL.Manager.Domain.Requests.UserRequests;
using LTL.Manager.Domain.Responses.UserResponses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LTL.Manager.WebApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(ILogger<UserController> logger, IUserService userService) : ControllerBase
{
  private readonly ILogger<UserController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  

  [HttpGet]
  public Task<ActionResult> HealthCheck()
  {
    return Task.FromResult<ActionResult>(Ok("User Service is running"));
  }

  [HttpPost]
  public async Task<ActionResult> AddUser(AddUserRequest user)
  {
    try
    {
      var result = await userService.AddUserAsync(user);
      return Created(result.UserId.ToString(), result);
    }
    catch (InvalidOperationException ex)
    {
      _logger.LogWarning(ex.Message);
      return Conflict(new { message = ex.Message });
    }
  }
  [HttpPost("login")]
  public async Task<ActionResult> LoginUser(LoginUserRequest request)
  {
    try
    {
      var result = await userService.LoginUserAsync(request);
      return Ok(result);
    }
    catch (InvalidOperationException ex)
    {
      _logger.LogWarning(ex.Message);
      return Unauthorized(new { message = ex.Message });
    }
  }
  
  [HttpGet("me")]
  [Authorize]
  public async Task<ActionResult> GetProfile()
  {
    var userId = GetCurrentUserId();
    if (userId == null) return Unauthorized();
    var result = await userService.GetUserDetailsAsync(userId.Value);
    return result == null ? NotFound() : Ok(result);
  }
  
  [HttpPut("me")]
  [Authorize]
  public async Task<ActionResult> UpdateProfile(UpdateUserRequest request)
  {
    var userId = GetCurrentUserId();
    if (userId == null || userId != request.UserId) return Unauthorized();
    try
    {
      var updated = await userService.UpdateUserAsync(request);
      return Ok(updated);
    }
    catch (InvalidOperationException ex)
    {
      _logger.LogWarning(ex, "Update profile failed for user {UserId}", userId);
      return Conflict(new { message = ex.Message });
    }
  }
  
  
  [HttpPost("me/change-password")]
  [Authorize]
  public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
  {
    var userId = GetCurrentUserId();
    if (userId == null || userId != request.UserId) return Unauthorized();
    try
    {
      await userService.ChangePasswordAsync(request);
      return NoContent();
    }
    catch (InvalidOperationException ex)
    {
      _logger.LogWarning(ex, "Change password failed for user {UserId}", userId);
      return Conflict(new { message = ex.Message });
    }
  }
  
  [HttpGet("/api/users")]
  [Authorize(Roles = "Admin")]
  public async Task<ActionResult> GetUsers([FromQuery] GetUsersRequest request)
  {
    var users = await userService.GetAllUsersAsync(request);
    return Ok(users);
  }
  
  [HttpPut("/api/users/{id:guid}/activate")]
  [Authorize(Roles = "Admin")]
  public async Task<ActionResult> ActivateUser(Guid id)
  {
    var user = await userService.GetUserDetailsAsync(id);
    if (user == null) return NotFound();
    if (user.IsActive) return BadRequest(new { message = "User is already active" });
    var request = new UpdateUserRequest() { UserId = id, IsActive = true };
    var success = await userService.UpdateUserAsync(request);
    return Ok(success);
  }

  [HttpGet("{id:guid}")]
  [Authorize]
  public async Task<ActionResult> GetById(Guid id)
  {
    var user = await userService.GetUserDetailsAsync(id);
    return user == null ? NotFound() : Ok(user);
  }

  private Guid? GetCurrentUserId()
  {
    var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
    return Guid.TryParse(idClaim, out var guid) ? guid : null;
  }
}

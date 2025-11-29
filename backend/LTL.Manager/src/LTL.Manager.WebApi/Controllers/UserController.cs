using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LTL.Manager.WebApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
  private readonly ILogger<UserController> _logger;
  
  public UserController(ILogger<UserController> logger)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }
  
  [HttpGet]
  public Task<ActionResult> HealthCheck()
  {
    return Task.FromResult<ActionResult>(Ok("User Service is running"));
  }
}

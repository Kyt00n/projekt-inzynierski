using LTL.Manager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LTL.Manager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
  private readonly ITripService _tripService;
  
  public TripController(ITripService tripService)
  {
    _tripService = tripService;
  }

  [HttpGet("{id:guid}/check-status")]
  public async Task<IActionResult> CheckTripStatus(Guid id)
  {
    var status = await _tripService.CheckTripStatusAsync(id);
    if (status == null) return NotFound();
    return new OkObjectResult(status);
  }
  
  [HttpPost("{id:guid}/start-trip")]
  public async Task<IActionResult> StartTrip(Guid id)
  {
    var result = await _tripService.StartTripAsync(id);
    if (!result) return BadRequest();
    return NoContent();
  }

  [HttpPost("{id:guid}/complete-trip")]
  public async Task<IActionResult> CompleteTrip(Guid id)
  {
    var result = await _tripService.CompleteTripAsync(id);
    if (!result) return BadRequest();
    return NoContent();
  }
}

using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Application.Interfaces;
using LTL.Manager.Domain.Requests.TripRequests;
using LTL.Manager.Domain.Responses.OrderResponse;
using LTL.Manager.Domain.Responses.TripResponse;

namespace LTL.Manager.Application.Services;

public class TripService : ITripService
{
  private readonly ITripRepository _tripRepository;

  public TripService(ITripRepository tripRepository)
  {
    _tripRepository = tripRepository;
  }

  public async Task<AssignTripResponse> AssignTripAsync(AcceptAssignmentOrderResponse request)
  {
    if (request.UserId == Guid.Empty)
    {
      throw new InvalidOperationException("UserId cannot be empty");
    }
    var existingTripId = await _tripRepository.GetTripIdAsync(request.UserId, request.DeliveryLocation);
    if (existingTripId != null)
    {
      return await _tripRepository.AssignOrderToTrip(existingTripId.Value, request.OrderId);
    }
    return await _tripRepository.CreateTripAndAssignOrder(request);
  }
}

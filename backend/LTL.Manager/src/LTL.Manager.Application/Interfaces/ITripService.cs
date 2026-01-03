using LTL.Manager.Domain.Requests.TripRequests;
using LTL.Manager.Domain.Responses.OrderResponse;
using LTL.Manager.Domain.Responses.TripResponse;

namespace LTL.Manager.Application.Interfaces;

public interface ITripService
{
  Task<AssignTripResponse> AssignTripAsync(AcceptAssignmentOrderResponse request);
}

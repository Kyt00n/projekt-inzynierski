using LTL.Manager.Domain.Responses.OrderResponse;
using LTL.Manager.Domain.Responses.TripResponse;

namespace LTL.Manager.Application.Infrastructure;

public interface ITripRepository
{
  Task<Guid?> GetTripIdAsync(Guid requestUserId, string requestDeliveryLocation);
  Task<AssignTripResponse> AssignOrderToTrip(Guid existingTripId, Guid requestOrderId);
  Task<AssignTripResponse> CreateTripAndAssignOrder(AcceptAssignmentOrderResponse request);
  Task<GetTripStatusResponse> GetTripStatusAsync(Guid id);
  Task CheckTripForCompletionAsync(Guid id);
  Task<bool> StartTripAsync(Guid id);
  Task<bool> CompleteTripAsync(Guid id);
}

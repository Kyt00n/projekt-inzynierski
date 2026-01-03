using AutoMapper;
using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Domain.Enums;
using LTL.Manager.Domain.Responses.OrderResponse;
using LTL.Manager.Domain.Responses.TripResponse;
using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace LTL.Manager.Infrastructure.Persistence.Repositories;

public class TripRepository : ITripRepository
{
  private readonly LtlMngrContext _context;
  private readonly IMapper _mapper;

  public TripRepository(LtlMngrContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<Guid?> GetTripIdAsync(Guid requestUserId, string requestDeliveryLocation)
  {
    var existingTrip = await _context.Trips
      .Include(t=>t.Orders)
      .Where(t => t.UserId == requestUserId && t.DeliveryLocation == requestDeliveryLocation && t.Status == TripStatus.Planned)
      .FirstOrDefaultAsync();
    return existingTrip?.TripId;
  }

  public async Task<AssignTripResponse> AssignOrderToTrip(Guid existingTripId, Guid requestOrderId)
  {
    try
    {
      var trip = await _context.Trips
        .Include(t => t.Orders)
        .FirstOrDefaultAsync(t => t.TripId == existingTripId);
      var order = await _context.Orders
        .FirstOrDefaultAsync(o => o.OrderId == requestOrderId);

      trip.Orders.Add(order);
      await _context.SaveChangesAsync();
      return _mapper.Map<AssignTripResponse>(order);
    }
    catch (InvalidOperationException)
    {
      throw new InvalidOperationException("Trip or Order not found");
    }
    catch (DbUpdateException)
    {
      throw new InvalidOperationException("Trip or Order not found");
    }
  }

  public async Task<AssignTripResponse> CreateTripAndAssignOrder(AcceptAssignmentOrderResponse request)
  {
    var tripDb = _mapper.Map<Trip>(request);
    var mappedTrip = _context.Trips.Add(tripDb);
    // Assign Order to Trip
    var order = await _context.Orders
      .FirstOrDefaultAsync(o => o.OrderId == request.OrderId);
    mappedTrip.Entity.Orders.Add(order);
    await _context.SaveChangesAsync();
    return _mapper.Map<AssignTripResponse>(mappedTrip.Entity);
  }
}

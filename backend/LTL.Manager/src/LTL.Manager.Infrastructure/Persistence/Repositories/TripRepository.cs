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

  public async Task<GetTripStatusResponse> GetTripStatusAsync(Guid id)
  {
    var trip = await _context.Trips
      .SingleOrDefaultAsync(t => t.TripId == id);
    if (trip == null) throw new InvalidOperationException("Trip not found");
    return new GetTripStatusResponse() { Status = trip.Status, };
  }

  public async Task CheckTripForCompletionAsync(Guid id)
  {
    var order = await _context.Orders
      .Include(o => o.Trip)
      .SingleOrDefaultAsync(o => o.OrderId == id);
    var tripId = order.Trip?.TripId;
    if (tripId == null) throw new InvalidOperationException("Trip not found");
    var trip = await _context.Trips
      .Include(t => t.Orders)
      .SingleOrDefaultAsync(t => t.TripId == tripId);
    
    var allCompleted = trip.Orders != null && trip.Orders.All(o => o.Status == OrderStatus.Completed);
    

    if (allCompleted && trip.Status != TripStatus.Finished)
    {
      trip.Status = TripStatus.Finished;
      await _context.SaveChangesAsync();
    }
  }

  public async Task<bool> StartTripAsync(Guid id)
  {
    var trip = await _context.Trips
      .SingleOrDefaultAsync(t => t.TripId == id);

    if (trip == null) throw new InvalidOperationException("Trip not found");

    if (trip.Status != TripStatus.Planned) return false;

    trip.Status = TripStatus.InProgress;
    await _context.SaveChangesAsync();
    return true;
  }

  public async Task<bool> CompleteTripAsync(Guid id)
  {
    var trip = await _context.Trips
      .Include(t => t.Orders)
      .SingleOrDefaultAsync(t => t.TripId == id);

    if (trip == null) throw new InvalidOperationException("Trip not found");

    // Do not complete trip if any order is not completed
    var allOrdersCompleted = trip.Orders == null || trip.Orders.All(o => o.Status == OrderStatus.Completed);
    if (!allOrdersCompleted) return false;

    if (trip.Status == TripStatus.Finished) return true;

    trip.Status = TripStatus.Finished;
    await _context.SaveChangesAsync();
    return true;
  }
}

using AutoMapper;
using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Domain.Enums;
using LTL.Manager.Domain.Requests.DocumentRequests;
using LTL.Manager.Domain.Requests.OrderRequests;
using LTL.Manager.Domain.Responses.OrderResponse;
using LTL.Manager.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace LTL.Manager.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
  private readonly LtlMngrContext _context;
  private readonly IMapper _mapper;

  public OrderRepository(LtlMngrContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<GetOrderResponse> CreateOrderAsync(CreateOrderRequest request)
  {
    var orderDb = _mapper.Map<Order>(request);
    var mappedOrder = _context.Orders.Add(orderDb);
    await _context.SaveChangesAsync();
    return _mapper.Map<GetOrderResponse>(mappedOrder.Entity);
  }

  public async Task<GetOrderResponse> UpdateOrderAsync(UpdateOrderRequest request)
  {
    try
    {
      var order = await FindOrderByIdAsync(request.OrderId);
      _mapper.Map(request, order);
      await _context.SaveChangesAsync();
      return _mapper.Map<GetOrderResponse>(order);
    }
    catch (InvalidOperationException)
    {
      throw new InvalidOperationException("Order not found");
    }
    catch (DbUpdateException)
    {
      throw new InvalidOperationException("Order not found");
    }
  }
  
  private async Task<Order> FindOrderByIdAsync(Guid id)
  {
    var order = await _context.Orders
      .Include(o=> o.Loads)
      .Include(o=> o.DriverNotes)
      .Include(o=> o.Documents)
      .SingleOrDefaultAsync(o => o.OrderId == id);
    if (order == null)
    {
      throw new InvalidOperationException("Order not found");
    }
    return order;
  }

  public async Task<GetOrderResponse> GetOrderAsync(Guid id)
  {
    try
    {
      var order = await FindOrderByIdAsync(id);
      return _mapper.Map<GetOrderResponse>(order);
    }
    catch (InvalidOperationException)
    {
      return null;
    }
  }

  public async Task<ICollection<GetOrderResponse>> GetOrdersAsync(GetOrdersRequest request)
  {
    var query = _context.Orders
      .Include(o => o.Loads)
      .AsQueryable();

    if (request.Status != null)
    {
      query = query.Where(o => o.Status == request.Status);
    }
    if (request.DriverId != null)
    {
      query = query.Where(o => o.UserId == request.DriverId);
    }
    var orders = await query.ToListAsync();
    return _mapper.Map<ICollection<GetOrderResponse>>(orders);
  }

  public async Task<GetOrderResponse> AddDriverNoteAsync(AddDriverNoteRequest request)
  {
    var order = await FindOrderByIdAsync(request.OrderId);
    var driversNoteDb = _mapper.Map<DriverNote>(request);
    order.DriverNotes.Add(driversNoteDb);

    await _context.SaveChangesAsync();
    return _mapper.Map<GetOrderResponse>(order);
  }

  public async Task<GetOrderResponse> AddOrderDocumentAsync(CreateDocumentRequest docRequest)
  {
    var order = await FindOrderByIdAsync(docRequest.OrderId);
    var documentDb = _mapper.Map<Document>(docRequest);
    order.Documents.Add(documentDb);
    await _context.SaveChangesAsync();
    return _mapper.Map<GetOrderResponse>(order);
  }
}

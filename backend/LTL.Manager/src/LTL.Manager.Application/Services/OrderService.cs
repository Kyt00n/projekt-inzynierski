using LTL.Manager.Application.Infrastructure;
using LTL.Manager.Application.Interfaces;
using LTL.Manager.Domain.Enums;
using LTL.Manager.Domain.Requests.DocumentRequests;
using LTL.Manager.Domain.Requests.OrderRequests;
using LTL.Manager.Domain.Responses.OrderResponse;

namespace LTL.Manager.Application.Services;

public class OrderService : IOrderService
{
  private readonly IOrderRepository _orderRepository;

  public OrderService(IOrderRepository orderRepository)
  {
    _orderRepository = orderRepository;
  }


  public async Task<GetOrderResponse> CreateOrderAsync(CreateOrderRequest request)
  {
    return await _orderRepository.CreateOrderAsync(request);
  }

  public async Task<GetOrderResponse> GetOrderAsync(Guid id)
  {
    var order = await _orderRepository.GetOrderAsync(id);
    if (order == null)
    {
      throw new InvalidOperationException("Order not found");
    }
    return order;
  }

  public async Task<bool> AssignDriverAsync(Guid id, Guid driverId)
  {
    var updateRequest = new UpdateOrderRequest() {OrderId = id, UserId = driverId, Status = OrderStatus.Assigned};
    var result =  await _orderRepository.UpdateOrderAsync(updateRequest);
    return result != null;
  }

  public async Task<AcceptAssignmentOrderResponse> AcceptAssignmentAsync(Guid id)
  {
    var updateRequest = new UpdateOrderRequest() { OrderId = id, Status = OrderStatus.InProgress };
    var result = await _orderRepository.UpdateOrderAsync(updateRequest);
    return new AcceptAssignmentOrderResponse()
    {
      OrderId = result.OrderId,
      UserId = result.UserId ?? Guid.Empty,
      DeliveryLocation = result.DeliveryLocation
    };
}

  public async Task<bool> UpdateStatusAsync(Guid id, OrderStatus status)
  {
    var updateRequest = new UpdateOrderRequest() {OrderId = id, Status = status };
    var result = await _orderRepository.UpdateOrderAsync(updateRequest);
    return result != null;
  }

  public async Task<GetOrderResponse> UpdateOrderAsync(Guid id, UpdateOrderRequest request)
  {
    if (id != request.OrderId)
    {
      throw new ArgumentException("Order ID mismatch");
    }
    return await _orderRepository.UpdateOrderAsync(request);
  }

  public Task<ICollection<GetOrderResponse>> GetActiveOrdersAsync()
  {
    var ordersRequest = new GetOrdersRequest() { Status = OrderStatus.Created };
    var result =  _orderRepository.GetOrdersAsync(ordersRequest);
    return result;
  }

  public Task<ICollection<GetOrderResponse>> GetUserOrdersAsync(Guid userId)
  {
    var ordersRequest = new GetOrdersRequest() { DriverId = userId };
    var result =  _orderRepository.GetOrdersAsync(ordersRequest);
    return result;
  }

  public Task<GetOrderResponse> AddDriverNoteAsync(Guid orderId, AddDriverNoteRequest request)
  {
    request.OrderId = orderId;
    return _orderRepository.AddDriverNoteAsync(request);
  }

  public Task<GetOrderResponse> AddOrderDocumentAsync(Guid id, CreateDocumentRequest docRequest)
  {
    docRequest.OrderId = id;
    return _orderRepository.AddOrderDocumentAsync(docRequest);
  }
}

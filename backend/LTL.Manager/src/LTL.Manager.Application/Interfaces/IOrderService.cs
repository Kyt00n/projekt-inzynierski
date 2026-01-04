using LTL.Manager.Domain.Enums;
using LTL.Manager.Domain.Requests.DocumentRequests;
using LTL.Manager.Domain.Requests.OrderRequests;
using LTL.Manager.Domain.Responses.OrderResponse;

namespace LTL.Manager.Application.Interfaces;

public interface IOrderService 
{
  Task<GetOrderResponse> CreateOrderAsync(CreateOrderRequest request);
  Task<GetOrderResponse> GetOrderAsync(Guid id);
  Task<bool> AssignDriverAsync(Guid id, Guid driverId);
  Task<AcceptAssignmentOrderResponse> AcceptAssignmentAsync(Guid id);
  Task<bool> UpdateStatusAsync(Guid id, OrderStatus status);

  Task<GetOrderResponse> UpdateOrderAsync(Guid id, UpdateOrderRequest status);
  Task<ICollection<GetOrderResponse>> GetActiveOrdersAsync();
  Task<ICollection<GetOrderResponse>> GetUserOrdersAsync(Guid userId);
  Task<GetOrderResponse> AddDriverNoteAsync(Guid id, AddDriverNoteRequest request);
  Task<GetOrderResponse> AddOrderDocumentAsync(Guid id, CreateDocumentRequest docRequest);
}

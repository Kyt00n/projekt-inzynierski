using LTL.Manager.Domain.Requests.OrderRequests;
using LTL.Manager.Domain.Responses.OrderResponse;

namespace LTL.Manager.Application.Infrastructure;

public interface IOrderRepository
{
  public Task<GetOrderResponse> CreateOrderAsync(CreateOrderRequest request);
  Task<GetOrderResponse> UpdateOrderAsync(UpdateOrderRequest request);
  Task<GetOrderResponse> GetOrderAsync(Guid id);
  Task<ICollection<GetOrderResponse>> GetOrdersAsync(GetOrdersRequest request);
}

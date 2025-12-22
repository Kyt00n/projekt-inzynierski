using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Domain.Requests.OrderRequests;

public class GetOrdersRequest
{
  public Guid? DriverId { get; set; }
  public OrderStatus? Status { get; set; }
}

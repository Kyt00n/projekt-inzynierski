using LTL.Manager.Domain.Entities;
using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Domain.Requests.OrderRequests;

public class UpdateStatusRequest 
{
  public OrderStatus Status { get; set; }
}

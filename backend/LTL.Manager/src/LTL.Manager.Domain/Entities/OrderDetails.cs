using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Domain.Entities;

public class OrderDetails : Order
{
  public Guid OrderId { get; set; }
  public OrderStatus Status { get; set; }
  public Guid? UserId { get; set; }
  public string DriverNotes { get; set; }
}

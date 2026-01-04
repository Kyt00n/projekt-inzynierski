using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Domain.Entities;

public class OrderDetails : Order
{
  public Guid OrderId { get; set; }
  public OrderStatus Status { get; set; }
  public Guid? UserId { get; set; }
  public Guid? TripId { get; set; }
  public ICollection<DriverNote> DriverNotes { get; set; }
}

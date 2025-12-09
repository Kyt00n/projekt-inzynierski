#nullable enable
using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Infrastructure.Persistence.Models;

public class Order : BaseEntity<int>
{
  public Guid OrderId { get; set; }
  public OrderStatus Status { get; set; }
  public Guid? UserId { get; set; }
  public User? Driver { get; set; }
  public Guid? TripId { get; set; }
  public Trip? Trip { get; set; }
  public ICollection<Load> Loads { get; set; } = new List<Load>();
  public ICollection<Document>? Documents { get; set; }
}

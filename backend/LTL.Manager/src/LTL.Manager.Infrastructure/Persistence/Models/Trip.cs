using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Infrastructure.Persistence.Models;

public class Trip : BaseEntity<int>
{
  public Guid TripId { get; set; }
  public Guid UserId { get; set; }
  public DateTime? StartTime { get; set; }
  public DateTime? EndTime { get; set; }
  
  public string DeliveryLocation { get; set; }
  public ICollection<Order> Orders { get; set; }
  public TripStatus Status { get; set; }
}

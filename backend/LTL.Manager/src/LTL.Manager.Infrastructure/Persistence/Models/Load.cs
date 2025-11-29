using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Infrastructure.Persistence.Models;

public class Load : BaseEntity<int>
{
  public Guid LoadId { get; set; }
  public string Description { get; set; } = String.Empty;
  public double Weight { get; set; }
  public string PickupLocation { get; set; } = String.Empty;
  public string DeliveryLocation { get; set; } = String.Empty;
  public LoadStatus Status { get; set; }
}

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
  public string PickupLocation { get; set; } = String.Empty;
  public string DeliveryLocation { get; set; } = String.Empty;
  
  public string Description { get; set; } = String.Empty;
  public string SpecialInstructions { get; set; } = String.Empty;
  public string DriverNotes { get; set; } = String.Empty;
  public Trip? Trip { get; set; }
  public ICollection<Load> Loads { get; set; } = new List<Load>();
  public ICollection<Document>? Documents { get; set; }
}

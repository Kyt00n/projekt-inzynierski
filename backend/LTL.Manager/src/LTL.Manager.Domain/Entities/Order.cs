namespace LTL.Manager.Domain.Entities;

public class Order
{
  public string PickupLocation { get; set; }
  public string DeliveryLocation { get; set; }
  public string Description { get; set; }
  public string SpecialInstructions { get; set; }
  public ICollection<Load> Loads { get; set; }
}

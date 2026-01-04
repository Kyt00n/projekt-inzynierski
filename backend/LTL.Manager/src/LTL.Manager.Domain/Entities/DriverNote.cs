namespace LTL.Manager.Domain.Entities;

public class DriverNote
{
  public Guid UserId { get; set; }
  public Guid OrderId { get; set; }
  public string Note { get; set; }
}

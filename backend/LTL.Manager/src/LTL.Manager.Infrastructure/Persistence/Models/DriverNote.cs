namespace LTL.Manager.Infrastructure.Persistence.Models;

public class DriverNote : BaseEntity<int>
{
  public Guid DriverNoteId { get; set; }
  public Guid UserId { get; set; }
  public Guid OrderId { get; set; }
  public Order Order { get; set; } = null!;
  public string Note { get; set; } = string.Empty;
}

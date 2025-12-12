#nullable enable
using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Infrastructure.Persistence.Models;

public class Load : BaseEntity<int>
{
  public Guid LoadId { get; set; }
  public string Description { get; set; } = String.Empty;
  public double Weight { get; set; }
  public double Length { get; set; }
  public double Width { get; set; }
  public double Height { get; set; }
  public Guid? OrderId { get; set; }
  public Order? Order { get; set; } = null;
}

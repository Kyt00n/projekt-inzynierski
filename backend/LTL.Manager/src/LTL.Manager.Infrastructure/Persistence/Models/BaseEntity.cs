namespace LTL.Manager.Infrastructure.Persistence.Models;

public abstract class BaseEntity<T>
{
  public T Id { get; set; }
  public DateTime CreatedOn { get; set; }
  public DateTime LastUpdated { get; set; }
}

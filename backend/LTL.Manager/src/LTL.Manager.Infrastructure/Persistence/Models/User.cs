using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Infrastructure.Persistence.Models;

public class User : BaseEntity<int>
{
  public Guid UserId { get; init; }
  public string Username { get; init; } = String.Empty;
  public string Name { get; init; } = String.Empty;
  public string Surname { get; init; } = String.Empty;
  public string Email { get; init; } = String.Empty;
  public string PasswordHash { get; init; } = String.Empty;
  public bool IsActive { get; init; }
  public bool IsAdmin { get; init; }
  
  public ICollection<Order> Orders { get; set; }
  public DriverStatus Status { get; set; }
}

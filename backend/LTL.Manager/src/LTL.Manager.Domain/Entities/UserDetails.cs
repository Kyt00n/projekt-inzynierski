namespace LTL.Manager.Domain.Entities;

public class UserDetails : User
{
  public Guid UserId { get; set; }
  public string Name { get; set; }
  public string Surname { get; set; }
  public string Email { get; set; }
  public bool IsActive { get; set; }
  
}

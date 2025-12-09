namespace LTL.Manager.Domain.Requests.UserRequests;

public class ChangePasswordRequest
{
  public Guid UserId { get; set; }
  public string PasswordHash { get; set; }
}

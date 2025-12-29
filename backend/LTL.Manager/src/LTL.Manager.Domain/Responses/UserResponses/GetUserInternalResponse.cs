namespace LTL.Manager.Domain.Responses.UserResponses;

public class GetUserInternalResponse: GetUserDetailsResponse
{
  public string PasswordHash { get; set; }
}

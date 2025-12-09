using LTL.Manager.Domain.Entities;
using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Domain.Responses.UserResponses;

public class GetUserDetailsResponse : GetUserResponse
{
  public ICollection<Order> Orders { get; set; }
  public DriverStatus Status { get; set; }
}

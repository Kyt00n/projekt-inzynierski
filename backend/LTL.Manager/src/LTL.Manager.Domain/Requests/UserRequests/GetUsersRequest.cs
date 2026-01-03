using LTL.Manager.Domain.Enums;

namespace LTL.Manager.Domain.Requests.UserRequests;

public class GetUsersRequest
{
  public DriverStatus? Status { get; set; }
  public bool? IsActive { get; set; }
}

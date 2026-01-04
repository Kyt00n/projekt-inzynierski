namespace LTL.Manager.Domain.Requests.OrderRequests;

public class AddDriverNoteRequest
{
  public Guid UserId { get; set; }
  public Guid OrderId { get; set; }
  public string Note { get; set; }
}

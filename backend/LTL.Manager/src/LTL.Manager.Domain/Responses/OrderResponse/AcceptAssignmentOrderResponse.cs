namespace LTL.Manager.Domain.Responses.OrderResponse;

public class AcceptAssignmentOrderResponse
{
  public Guid UserId { get; set; }
  
  public Guid OrderId { get; set; }
  public string DeliveryLocation { get; set; }
}

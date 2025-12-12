namespace LTL.Manager.Domain.Responses.LoadResponse;

public class LoadResponse
{
  public Guid? LoadId { get; set; }
  public string Description { get; set; }
  public double Weight { get; set; }
  public double Length { get; set; }
  public double Width { get; set; }
  public double Height { get; set; }
}

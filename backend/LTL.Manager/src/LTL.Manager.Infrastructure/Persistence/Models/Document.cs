namespace LTL.Manager.Infrastructure.Persistence.Models;

public class Document : BaseEntity<int>
{
  public Guid DocumentId { get; set; }
  public Guid OrderId { get; set; }
  public Order Order { get; set; } = null!;
  public string FileName { get; set; } = null!;
  public byte[] FileContent { get; set; } = null!;
  public string FileType { get; set; } = null!;
}

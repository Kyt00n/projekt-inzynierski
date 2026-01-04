using LTL.Manager.Application.Interfaces;
using LTL.Manager.Domain.Requests.DocumentRequests;
using LTL.Manager.Domain.Requests.OrderRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTL.Manager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
  private readonly IOrderService _orderService;
  private readonly ITripService _tripService;

  public OrderController(IOrderService orderService, ITripService tripService)
  {
    _orderService = orderService;
    _tripService = tripService;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
  {
    var created = await _orderService.CreateOrderAsync(request);
    return Created(created.OrderId.ToString(), created);
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> Get(Guid id)
  {
    var order = await _orderService.GetOrderAsync(id);
    if (order == null) return NotFound();
    return Ok(order);
  }

  [HttpGet("active-orders")]
  public async Task<IActionResult> GetActiveOrders()
  {
    var orders = await _orderService.GetActiveOrdersAsync();
    return Ok(orders);
  }

  [HttpGet("{userId:guid}/orders")]
  public async Task<IActionResult> GetUserOrders(Guid userId)
  {
    var orders = await _orderService.GetUserOrdersAsync(userId);
    return Ok(orders);
  }

  [HttpPut("{id:guid}/assign-driver")]
  public async Task<IActionResult> AssignDriver(Guid id, [FromBody] AssignDriverRequest request)
  {
    var success = await _orderService.AssignDriverAsync(id, request.DriverId);
    if (!success) return BadRequest();
    return NoContent();
  }

  [HttpPut("{id:guid}/accept")]
  public async Task<IActionResult> AcceptAssignment(Guid id)
  {
    try
    {
      var order = await _orderService.AcceptAssignmentAsync(id);
      var trip = await _tripService.AssignTripAsync(order);
      return Ok(trip);
    }catch (InvalidOperationException ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut("{id:guid}/status")]
  public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
  {
    var success = await _orderService.UpdateStatusAsync(id, request.Status);
    await _tripService.CheckTripForCompletionAsync(id);
    if (!success) return BadRequest();
    return NoContent();
  }
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderRequest request)
  {
    var updated = await _orderService.UpdateOrderAsync(id, request);
    if (updated == null) return NotFound();
    return Ok(updated); 
  }
  [HttpPost("{id:guid}/driver-note")]
  public async Task<IActionResult> AddDriverNote(Guid id, [FromBody] AddDriverNoteRequest request)
  {
    try
    {
      if (string.IsNullOrWhiteSpace(request.Note)) return BadRequest("Note is required.");
      var success = await _orderService.AddDriverNoteAsync(id, request);
      return Ok(success);
    }
    catch (InvalidOperationException)
    {
      return BadRequest();
    }
  }

  [HttpPost("{id:guid}/documents")]
  public async Task<IActionResult> UploadDocument(Guid id, IFormFile file)
  {
    try
    {
      if (file == null || file.Length == 0) return BadRequest("File is empty.");
      using var ms = new MemoryStream();
      await file.CopyToAsync(ms);

      var docRequest = new CreateDocumentRequest
      {
        FileName = Path.GetFileName(file.FileName),
        FileType = file.ContentType ?? "application/octet-stream",
        FileContent = ms.ToArray()
      };

      var created = await _orderService.AddOrderDocumentAsync(id, docRequest);
      return Ok(created);
    } catch (InvalidOperationException)
    {
      return BadRequest();
    }
    
  }
}

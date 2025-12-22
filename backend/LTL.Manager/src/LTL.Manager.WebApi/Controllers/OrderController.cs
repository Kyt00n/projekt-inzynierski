using LTL.Manager.Application.Interfaces;
using LTL.Manager.Domain.Requests.OrderRequests;
using Microsoft.AspNetCore.Mvc;

namespace LTL.Manager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
  private readonly IOrderService _orderService;

  public OrderController(IOrderService orderService)
  {
    _orderService = orderService;
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
    var success = await _orderService.AcceptAssignmentAsync(id);
    if (!success) return BadRequest();
    return NoContent();
  }

  [HttpPut("{id:guid}/status")]
  public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusRequest request)
  {
    var success = await _orderService.UpdateStatusAsync(id, request.Status);
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
}

using DevOpsPlatform.Domain.Entities;
using DevOpsPlatform.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
	private readonly AppDbContext _db;

	public OrdersController(AppDbContext db)
	{
		_db = db;
	}

	[HttpPost]
	public async Task<IActionResult> Create(Order order)
	{
		order.Id = Guid.NewGuid();
		order.CreatedAt = DateTime.UtcNow;

		_db.Orders.Add(order);
		await _db.SaveChangesAsync();

		return Ok(order);
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var orders = await _db.Orders.ToListAsync();
		return Ok(orders);
	}
}

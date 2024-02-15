using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly ILogger<ProductsController> _logger;
	private readonly StoreContext _context;

	public ProductsController(ILogger<ProductsController> logger, StoreContext context)
	{
		_context = context;
		_logger = logger;
	}

	[HttpGet]
	public async Task<ActionResult<List<Product>>> GetProducts()
	{
		var products = await _context.Products.ToListAsync();

		return products;
	}

	[HttpGet]
	[Route("{id}")]
	public async Task<ActionResult<Product>> GetProduct(int id)
	{
		var product = await _context.Products.FindAsync(id);
		return Ok(product);
	}

}

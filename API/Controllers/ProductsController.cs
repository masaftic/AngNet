using System.Linq.Expressions;
using Azure.Core.Pipeline;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
	private readonly ILogger<ProductsController> _logger;
	private readonly IGenericRepository<Product> _productsRepo;
	private readonly IGenericRepository<ProductBrand> _productBrandRepo;
	private readonly IGenericRepository<ProductType> _productTypeRepo;

	public ProductsController(
		ILogger<ProductsController> logger,
		IGenericRepository<Product> productsRepo,
		IGenericRepository<ProductBrand> productBrandRepo,
		IGenericRepository<ProductType> productTypeRepo)
	{
		_logger = logger;
		_productsRepo = productsRepo;
		_productBrandRepo = productBrandRepo;
		_productTypeRepo = productTypeRepo;
	}

	[HttpGet]
	public async Task<ActionResult<List<Product>>> GetProducts()
	{
		var products = await _productsRepo.ListAsync(new ProductsWithTypesAndBrandsSpecification());

		return Ok(products);
	}

	[HttpGet]
	[Route("{id}")]
	public async Task<ActionResult<Product>> GetProduct(int id)
	{
		var spec = new ProductsWithTypesAndBrandsSpecification(id);
		var product = await _productsRepo.GetEntityWithSpec(spec);
		return Ok(product);
	}

	[HttpGet]
	[Route("brands")]
	public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
	{
		return Ok(await _productBrandRepo.ListAllAsync());
	}

	[HttpGet]
	[Route("types")]
	public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
	{
		return Ok(await _productTypeRepo.ListAllAsync());
	}
}

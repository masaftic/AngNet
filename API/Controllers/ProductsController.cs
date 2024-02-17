using System.Linq.Expressions;
using API.Dtos;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public ProductsController(
		ILogger<ProductsController> logger,
		IGenericRepository<Product> productsRepo,
		IGenericRepository<ProductBrand> productBrandRepo,
		IGenericRepository<ProductType> productTypeRepo,
		IMapper mapper)
	{
		_logger = logger;
		_productsRepo = productsRepo;
		_productBrandRepo = productBrandRepo;
		_productTypeRepo = productTypeRepo;
        _mapper = mapper;
    }

	[HttpGet]
	public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
	{
		var spec = new ProductsWithTypesAndBrandsSpecification();
		var products = await _productsRepo.ListAsync(spec);

		return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
	}

	[HttpGet]
	[Route("{id}")]
	public async Task<ActionResult<Product>> GetProduct(int id)
	{
		var spec = new ProductsWithTypesAndBrandsSpecification(id);
		var product = await _productsRepo.GetEntityWithSpec(spec);

		return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
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

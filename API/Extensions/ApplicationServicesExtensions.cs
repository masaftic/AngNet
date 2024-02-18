using Infrastructure.Data;
using Core.Interfaces;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using API.Errors;

namespace API.Extensions;

public static class ApplicationServicesExtensions
{
	public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration config)
	{
		services.Configure<ApiBehaviorOptions>(options =>
		{
			options.InvalidModelStateResponseFactory = actionContext =>
			{
				var errors = actionContext.ModelState
					.Where(e => e.Value.Errors.Count > 0)
					.SelectMany(x => x.Value.Errors)
					.Select(x => x.ErrorMessage)
					.ToArray();
				
				var errorResponse = new ApiValidationErrorResponse
				{
					Errors = errors
				};

				return new BadRequestObjectResult(errorResponse);
			};
		});

		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddAutoMapper(typeof(MappingProfiles));
	}
}

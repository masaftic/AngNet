using Infrastructure.Data;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using API.Helpers;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
	public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration config)
	{
		services.AddControllers();
		services.AddDbContext<StoreContext>(options =>
		{
			options.UseSqlServer(connectionString: config.GetConnectionString("DefaultConnection"));
		});

		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddAutoMapper(typeof(MappingProfiles));
	}
}

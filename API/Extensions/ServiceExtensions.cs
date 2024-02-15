using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ServiceExtensions
{
	public static void ConfigureDbContext(this IServiceCollection services, string? connectionString)
	{
		if (connectionString is null) throw new Exception("bad connection string");
		services.AddDbContext<StoreContext>(options =>
		{
			options.UseSqlServer(connectionString: connectionString);
		});
	}
}

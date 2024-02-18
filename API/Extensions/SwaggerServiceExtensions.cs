using Microsoft.OpenApi.Models;

namespace API.Extensions;

public static class SwaggerServiceExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AngNet API", Version = "v1" });
        });
    }


    public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/vi/swagger.json", "AngNet API v1");
        });

        return app;
    }
}
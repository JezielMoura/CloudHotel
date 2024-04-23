namespace CloudHotel.Infrastructure.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseReDoc(options => 
            (options.DocumentTitle, options.RoutePrefix, options.SpecUrl) = ("CloudHotel Docs", "api/swagger", "/swagger/v1/swagger.json"));
    }
}
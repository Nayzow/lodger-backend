namespace LodgerBackend.Configuration;

public static class CorsConfiguration
{
    public static void AddDevCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowDevOrigins", policy => policy
                .WithOrigins("http://localhost:4200", "http://localhost:80", "http://localhost")
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE"));
        });
    }
    
    public static void AddProdCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowProdOrigins", policy => policy
                .WithOrigins("http://exemple.com")
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE"));
        });
    }
}

using LodgerBackend.Auth.Models.Payloads;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Configuration.DbContext;
using LodgerBackend.User.Services;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.Configuration.Helpers
{
    public static class SwaggerAuthHelper
    {
        public static async Task<string> InitializeSwaggerUserAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LodgerDbContext>();
            db.Database.Migrate();

            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

            var email = "swagger@test.fr";
            var password = "Swagger123!";
            var user = await userService.GetByEmail(email);

            if (user == null)
            {
                var newUser = new SignupRequest
                {
                    Name = "toto",
                    Email = email,
                    Password = password
                };
                await authService.HandleSignUp(newUser);
                user = await userService.GetByEmail(email); // Reload user
            }

            var loginRequest = new LoginRequest { Email = email, Password = password };
            var authResponse = await authService.HandleLogin(loginRequest);

            if (authResponse != null)
            {
                return authResponse.Token;
            }
            return string.Empty;
        }

        public static void MapSwaggerCustomJs(this WebApplication app, string token)
        {
            app.MapGet("/swagger/custom-swagger.js", async context =>
            {
                context.Response.ContentType = "application/javascript";

                var jsContent = $@"
                    const token = '{token}'; 
                    if (token) {{
                        localStorage.setItem('authorized', JSON.stringify({{
                            Bearer: {{
                                name: 'Bearer',
                                schema: {{
                                    type: 'http',
                                    description: 'Entrez le token JWT ici. Exemple : Bearer {{votre_token}}',
                                    scheme: 'bearer',
                                    bearerFormat: 'JWT'
                                }},
                                value: token
                            }}
                        }}));
                    }}
                ";
                await context.Response.WriteAsync(jsContent);
            });
        }
    }
}

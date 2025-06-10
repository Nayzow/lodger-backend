using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using LodgerBackend.Auth;
using LodgerBackend.Configuration;
using LodgerBackend.Configuration.Helpers;
using LodgerBackend.Configuration.MappingProfile;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Minio;
using QuestPDF.Infrastructure;
QuestPDF.Settings.License = LicenseType.Community; // Nécessaire pour la génération de pdf

var builder = WebApplication.CreateBuilder(args);
var jwtSecretKey = builder.Configuration["Jwt:SecretKey"] ?? string.Empty;
var key = Encoding.ASCII.GetBytes(jwtSecretKey);

var minioEndpoint = builder.Environment.IsDevelopment() ? "localhost" : "minio";

builder.Services.AddSingleton<IMinioClient>(_ => new MinioClient()
    .WithEndpoint(minioEndpoint, 9000)
    .WithCredentials("admin", "password")
    .WithSSL(false) // HTTP
    .Build());

// ----- Services de base -----
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

// ----- Services custom -----
builder.Services.AddAppOptions(builder.Configuration);
builder.Services.AddCustomDbContexts(builder.Configuration);
builder.Services.AddCustomRepositories();
builder.Services.AddCustomServices();
builder.Services.AddDevCors();
builder.Services.AddProdCors();
builder.Services.AddSwaggerWithJwt();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
        };
    });

builder.Services.AddAuthorization();
// Auto Mapper pour mapper les dto avec les models 
builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // On laisse le comportement par d�faut = pas de $id
        options.JsonSerializerOptions.ReferenceHandler = null;
    });
// ----- App -----
var app = builder.Build();
app.UseStaticFiles();

Console.WriteLine($"Current environment: {builder.Environment.EnvironmentName}");

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
    app.UseCors("AllowDevOrigins");

#if DEBUG
    var token = await SwaggerAuthHelper.InitializeSwaggerUserAsync(app);
    app.MapSwaggerCustomJs(token);
#endif

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ton API V1");
#if DEBUG
        c.InjectJavascript("/swagger/custom-swagger.js"); // Injecte notre JS dynamique
        c.EnablePersistAuthorization();
#endif
    });
}

else
{
    app.UseExceptionHandler("/error");
    app.UseCors("AllowProdOrigins");
    app.UseHsts();
}

app.UseForwardedHeaders(); // Nécessaire pour récupérer l'adresse ip
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
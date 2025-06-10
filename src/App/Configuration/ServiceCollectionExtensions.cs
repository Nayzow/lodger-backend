using LodgerBackend.App.Auth.Repositories.Implementations;
using LodgerBackend.App.Auth.Repositories.Interfaces;
using LodgerBackend.App.Auth.Services.Implementations;
using LodgerBackend.App.Auth.Services.Interfaces;
using LodgerBackend.App.Configuration.DbContext;
using LodgerBackend.App.Configuration.Settings;
using LodgerBackend.App.Device.Repositories;
using LodgerBackend.App.Device.Services;
using LodgerBackend.App.Document.Repositories;
using LodgerBackend.App.Document.Services;
using LodgerBackend.App.File.Services;
using LodgerBackend.App.Payment.Repositories;
using LodgerBackend.App.Payment.Services;
using LodgerBackend.App.RentalFile.Repositories;
using LodgerBackend.App.RentalFile.Services;
using LodgerBackend.App.Settings;
using LodgerBackend.App.Settings.Repositories;
using LodgerBackend.App.User.Repositories;
using LodgerBackend.App.User.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.App.Configuration;

public static class ServiceCollectionExtensions
{
    public static void AddCustomRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IResetPasswordRequestRepository, ResetPasswordRequestRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ISettingsRepository, SettingsRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        services.AddScoped<IRentalFileRepository, RentalFileRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        // Sécurité
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordResetService, PasswordResetService>();
        services.AddScoped<IAuthService, AuthService>();
        
        // Mails
        services.AddScoped<IAuthMailsService, AuthMailsService>();

        // Fichiers
        services.AddScoped<IFileService, MinioService>();
        services.AddScoped<IDocumentService, DocumentService>();
        
        // Utilisateur
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ISettingsService, SettingsService>();
        services.AddScoped<IDeviceService, DeviceService>();
        services.AddScoped<IRentalFileService, RentalFileService>();
        services.AddScoped<ICommentService, CommentService>();
    }

    public static void AddCustomDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LodgerDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void AddAppOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.Configure<ForwardedHeadersOptions>(options =>
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        );
    }

}
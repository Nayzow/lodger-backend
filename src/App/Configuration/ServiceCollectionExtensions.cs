using LodgerBackend.Auth.Repositories.Implementations;
using LodgerBackend.Auth.Repositories.Interfaces;
using LodgerBackend.Auth.Services.Implementations;
using LodgerBackend.Auth.Services.Interfaces;
using LodgerBackend.Configuration.DbContext;
using LodgerBackend.Configuration.Settings;
using LodgerBackend.Device.Repositories;
using LodgerBackend.Device.Services;
using LodgerBackend.Document.Repositories;
using LodgerBackend.Document.Services;
using LodgerBackend.File.Services;
using LodgerBackend.Payment.Repositories;
using LodgerBackend.Payment.Services;
using LodgerBackend.RentalFile.Repositories;
using LodgerBackend.RentalFile.Services;
using LodgerBackend.Setting;
using LodgerBackend.Setting.Repositories;
using LodgerBackend.User.Repositories;
using LodgerBackend.User.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.Configuration;

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
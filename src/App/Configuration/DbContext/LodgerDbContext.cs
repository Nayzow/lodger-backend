

using LodgerBackend.App.Auth.Models.Entities;
using LodgerBackend.App.RentalFile;
using LodgerBackend.App.User.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.App.Configuration.DbContext;

public class LodgerDbContext(DbContextOptions<LodgerDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<User.Models.Entities.User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User.Models.Entities.Address> Addresses { get; set; }
    public DbSet<Payment.Models.Payment> Payments { get; set; }
    public DbSet<App.Settings.Models.Settings> Settings { get; set; }
    public DbSet<Device.Models.Device> Devices { get; set; }
    public DbSet<RentalFile.Models.RentalFile> RentalFiles { get; set; }
    public DbSet<global::LodgerBackend.App.Document.Models.Document> Documents { get; set; }


    public DbSet<ResetPasswordRequest> ResetPasswordRequests => Set<ResetPasswordRequest>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRole>()
            .HasKey(userRole => new { userRole.UserId, userRole.RoleId });

        modelBuilder.Entity<UserRole>()
            .HasOne(userRole => userRole.User)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(userRole => userRole.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(userRole => userRole.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(userRole => userRole.RoleId);

        modelBuilder.Entity<User.Models.Entities.User>()
            .Property(u => u.Gender)
            .HasConversion<int>(); // Conversion explicite vers int pour PostgreSQL

    }
}
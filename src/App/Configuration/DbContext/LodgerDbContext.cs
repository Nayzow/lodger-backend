using LodgerBackend.Auth.Models.Entities;
using LodgerBackend.User.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LodgerBackend.Configuration.DbContext;

public class LodgerDbContext(DbContextOptions<LodgerDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<User.Models.Entities.User> Users { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<User.Models.Entities.Address> Addresses { get; set; }
    public DbSet<Payment.Models.Payment> Payments { get; set; }
    public DbSet<Setting.Models.Settings> Settings { get; set; }
    public DbSet<Device.Models.Device> Devices { get; set; }
    public DbSet<RentalFile.Models.RentalFile> RentalFiles { get; set; }
    public DbSet<global::LodgerBackend.Document.Models.Document> Documents { get; set; }


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
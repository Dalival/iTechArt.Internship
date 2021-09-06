using ITechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysDbContext : DbContext
    {
        public SurveysDbContext(DbContextOptions options)
            : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
                b.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex").IsUnique();
                b.ToTable("Users");
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);
                b.Property(u => u.Email).HasMaxLength(256);
                b.Property(u => u.NormalizedEmail).HasMaxLength(256);

                b.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).IsRequired();
                b.HasData(new User
                {
                    Id = "4beb0654-3b7a-4601-8b81-b284cc25a903",
                    UserName = "EgorFedorenko",
                    NormalizedUserName = "EGORFEDORENKO",
                    Email = "egorfedorenko.w@gmail.com",
                    NormalizedEmail = "EGORFEDORENKO.W@GMAIL.COM",
                    EmailConfirmed = false,
                    PasswordHash =
                        "AQAAAAEAACcQAAAAEDxts21ZFCTO9PJMekWmZIcRpZFtuqrjSI4xwd76L0h5zF3WoQlhE015Xr+kBSDqsw==",
                    SecurityStamp = "9dd2b025-477a-4ab2-af59-dfe6f16ea4e7",
                    ConcurrencyStamp = "01fbbd27-bd79-4f36-a892-384df2a5cea6",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = false,
                    AccessFailedCount = 0
                });
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();
                b.ToTable("Roles");
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(r => r.Name).HasMaxLength(256);
                b.Property(r => r.NormalizedName).HasMaxLength(256);

                b.HasMany(r => r.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasData(
                    new Role
                    {
                        Id = "76e401a9-1e91-4dff-adb7-c455cefe6fa9",
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = "4179d8bd-907e-4293-bf2b-5a4598e34551"
                    },
                    new Role
                    {
                        Id = "b03bd4cc-93a8-4623-ab9d-606823a1547e",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = "a00343f0-cc82-452e-b00b-663216eadce8"
                    });
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
                b.ToTable("UserRoles");
                b.HasData(
                    new UserRole
                    {
                        UserId = "4beb0654-3b7a-4601-8b81-b284cc25a903",
                        RoleId = "76e401a9-1e91-4dff-adb7-c455cefe6fa9",
                    },
                    new UserRole
                    {
                        UserId = "4beb0654-3b7a-4601-8b81-b284cc25a903",
                        RoleId = "b03bd4cc-93a8-4623-ab9d-606823a1547e",
                    });
            });
        }
    }
}

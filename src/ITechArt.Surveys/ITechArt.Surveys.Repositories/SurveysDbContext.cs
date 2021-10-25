﻿using ITechArt.Surveys.DomainModel;
using ITechArt.Surveys.DomainModel.Responses;
using Microsoft.EntityFrameworkCore;

namespace ITechArt.Surveys.Repositories
{
    public class SurveysDbContext : DbContext
    {
        private const string AdminName = "EgorFedorenko";
        private const string AdminEmail = "egorfedorenko.w@gmail.com";
        private const string AdminId = "4beb0654-3b7a-4601-8b81-b284cc25a903";

        private const string AdminRoleName = "Admin";
        private const string AdminRoleId = "b03bd4cc-93a8-4623-ab9d-606823a1547e";

        private const string UserRoleName = "User";
        private const string UserRoleId = "76e401a9-1e91-4dff-adb7-c455cefe6fa9";

        private const int MaxStringLength = 256;
        private const int MaxTextLength = 5000;


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

                b.Property(u => u.UserName).HasMaxLength(MaxStringLength);
                b.Property(u => u.NormalizedUserName).HasMaxLength(MaxStringLength);
                b.Property(u => u.Email).HasMaxLength(MaxStringLength);
                b.Property(u => u.NormalizedEmail).HasMaxLength(MaxStringLength);

                b.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId).IsRequired();
                b.HasMany(u => u.Surveys).WithOne(s => s.Owner);
                b.HasMany(u => u.Responses).WithOne(r => r.Respondent);

                b.HasData(new User
                {
                    Id = AdminId,
                    UserName = AdminName,
                    NormalizedUserName = AdminName.ToUpper(),
                    Email = AdminEmail,
                    NormalizedEmail = AdminEmail.ToUpper(),
                    EmailConfirmed = false,
                    PasswordHash = "AQAAAAEAACcQAAAAEDxts21ZFCTO9PJMekWmZIcRpZFtuqrjSI4xwd76L0h5zF3WoQlhE015Xr+kBSDqsw==",
                    SecurityStamp = "9dd2b025-477a-4ab2-af59-dfe6f16ea4e7",
                    ConcurrencyStamp = "01fbbd27-bd79-4f36-a892-384df2a5cea6",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    RegistrationDate = new System.DateTime(2021, 9, 14, 13, 02, 32)
                });
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.HasKey(r => r.Id);
                b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();
                b.ToTable("Roles");
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.Property(r => r.Name).HasMaxLength(MaxStringLength);
                b.Property(r => r.NormalizedName).HasMaxLength(MaxStringLength);

                b.HasMany(r => r.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleId).IsRequired();
                b.HasData(
                    new Role
                    {
                        Id = UserRoleId,
                        Name = UserRoleName,
                        NormalizedName = UserRoleName.ToUpper(),
                        ConcurrencyStamp = "4179d8bd-907e-4293-bf2b-5a4598e34551"
                    },
                    new Role
                    {
                        Id = AdminRoleId,
                        Name = AdminRoleName,
                        NormalizedName = AdminRoleName.ToUpper(),
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
                        UserId = AdminId,
                        RoleId = AdminRoleId
                    },
                    new UserRole
                    {
                        UserId = AdminId,
                        RoleId = UserRoleId
                    });
            });

            modelBuilder.Entity<Survey>(b =>
            {
                b.HasKey(s => s.Id);
                b.ToTable("Surveys");

                b.Property(s => s.Name).HasMaxLength(MaxStringLength).IsRequired();

                b.HasOne(s => s.Owner).WithMany(u => u.Surveys);
                b.HasMany(s => s.Sections).WithOne(s => s.Survey);
                b.HasMany(s => s.Responses).WithOne(r => r.Survey);
            });

            modelBuilder.Entity<Section>(b =>
            {
                b.HasKey(s => s.Id);
                b.ToTable("Sections");

                b.Property(s => s.Name).HasMaxLength(MaxStringLength);
                b.Property(s => s.Description).HasMaxLength(MaxTextLength);

                b.HasOne(s => s.Survey).WithMany(s => s.Sections);
                b.HasMany(s => s.Questions).WithOne(q => q.Section);
            });

            modelBuilder.Entity<Question>(b =>
            {
                b.HasKey(q => q.Id);
                b.ToTable("Questions");

                b.Property(q => q.Title).HasMaxLength(MaxStringLength).IsRequired();
                b.Property(q => q.Description).HasMaxLength(MaxTextLength);

                b.HasOne(q => q.Section).WithMany(s => s.Questions);
            });

            modelBuilder.Entity<Option>(b =>
            {
                b.HasKey(q => q.Id);
                b.ToTable("Options");

                b.Property(c => c.Value).HasMaxLength(MaxTextLength);

                b.HasOne(c => c.Question);
            });

            modelBuilder.Entity<Response>(b =>
            {
                b.HasKey(r => r.Id);
                b.ToTable("Responses");

                b.HasOne(r => r.Respondent).WithMany(u => u.Responses);
                b.HasOne(r => r.Survey).WithMany(s => s.Responses);
            });

            modelBuilder.Entity<SingleChoiceResponse>(b =>
            {
                b.HasKey(r => r.Id);
                b.ToTable("SingleChoiceResponses");

                b.HasOne(scr => scr.Option);
                b.HasOne(scr => scr.Question);
                b.HasOne(scr => scr.Response);
            });

            modelBuilder.Entity<MultipleChoiceResponse>(b =>
            {
                b.HasKey(r => r.Id);
                b.ToTable("MultipleChoiceResponses");

                b.HasMany(scr => scr.Options);
                b.HasOne(scr => scr.Question);
                b.HasOne(scr => scr.Response);
            });

            modelBuilder.Entity<TextResponse>(b =>
            {
                b.HasKey(r => r.Id);
                b.ToTable("TextResponses");

                b.Property(tr => tr.Value).HasMaxLength(MaxTextLength);

                b.HasOne(scr => scr.Question);
                b.HasOne(scr => scr.Response);
            });

            modelBuilder.Entity<FileResponse>(b =>
            {
                b.HasKey(r => r.Id);
                b.ToTable("FileResponses");

                b.HasOne(scr => scr.Question);
                b.HasOne(scr => scr.Response);
            });

            modelBuilder.Entity<NumericResponse>(b =>
            {
                b.HasKey(r => r.Id);
                b.ToTable("NumericResponses");

                b.HasOne(scr => scr.Question);
                b.HasOne(scr => scr.Response);
            });
        }
    }
}

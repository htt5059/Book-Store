using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Book_Store.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed Roles (User, Admin, SuperAdmin)
            var adminRoleId = "c0f318c6-1274-4487-b57f-941d9392e202";
            var superAdminRoleId = "ceef8754-f983-46a5-ab2e-fb9dbf5ddd5c";
            var userRoleId = "b0f35376-c70b-4a33-a61e-e277adaeaa26";
            var superAdminUserId = "7ba1ceb6-7e08-44fc-9ee7-ac7f99fab488";
            //var userRoleId = "c0f318c6-1274-4487-b57f-941d9392e202";
            var roles = new List<IdentityRole> {
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole{
                    Name= "User",
                    NormalizedName = "User",
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                },
                new IdentityRole{ 
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
            // Seed SuperAdminUser
            var superAdminUser = new IdentityUser {
                UserName = "superadmin@donga.com",
                Email = "superadmin@donga.com",
                NormalizedEmail = "superadmin@donga.com".ToUpper(),
                NormalizedUserName = "superadmin@donga.com".ToUpper(),
                Id = superAdminUserId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "password");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all roles to SuperAdminUser
            var superAdminRoles = new List<IdentityUserRole<string>> {
                new IdentityUserRole<string> {
                    RoleId = adminRoleId,
                    UserId = superAdminUserId
                },
                new IdentityUserRole<string> {
                    RoleId = superAdminRoleId,
                    UserId = superAdminUserId
                },
                new IdentityUserRole<string> {
                    RoleId = userRoleId,
                    UserId = superAdminUserId
                }

            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}

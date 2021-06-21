using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace student_webapi.Models
{
    public class UserDbContext: IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }
        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            User user = new User()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin",
                PasswordHash = passwordHasher.HashPassword(null, "edutest2021")

            };

            builder.Entity<User>().HasData(user);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "admin", ConcurrencyStamp = "1", NormalizedName = "admin" }
                );


            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() 
                { 
                    RoleId = "fab4fac1-c546-41de-aebc-a14da6895711",
                    UserId = "b74ddd14-6340-4840-95c2-db12554843e5" 
                });


        }*/
    }
}

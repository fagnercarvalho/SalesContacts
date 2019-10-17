
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using SalesContacts.Data.Model;
using Microsoft.Extensions.DependencyInjection;
using SalesContacts.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SalesContacts.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void EncryptUserPasswords(this IApplicationBuilder app, string connectionstring)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var hasher = scope.ServiceProvider.GetService<IPasswordHasher<UserSys>>();

                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                optionsBuilder.UseSqlServer(connectionstring);

                using (var context = new Context(optionsBuilder.Options))
                {
                    context.Database.ExecuteSqlRaw("ALTER TABLE UserSys ALTER COLUMN [Password] VARCHAR(84) NOT NULL");

                    var users = context.Users.ToList();

                    foreach (var user in users)
                    {
                        if (user.Password.Length == 84)
                        {
                            // migrated password
                            continue;
                        }

                        var hashedPassword = hasher.HashPassword(user, user.Password);

                        var result = hasher.VerifyHashedPassword(user, hashedPassword, user.Password);

                        if (result == PasswordVerificationResult.Success)
                        {
                            // password is not hashed
                            user.Password = hashedPassword;
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}

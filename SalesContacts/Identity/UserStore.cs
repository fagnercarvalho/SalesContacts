using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SalesContacts.Data;
using SalesContacts.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SalesContacts.Identity
{
    public class UserStore : IUserStore<UserSys>, IUserPasswordStore<UserSys>, IUserRoleStore<UserSys>
    {
        private readonly IContext context;

        public UserStore(IContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context?.Dispose();
            }
        }

        public Task<string> GetUserIdAsync(UserSys user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(UserSys user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetUserNameAsync(UserSys user, string userName, CancellationToken cancellationToken)
        {
            throw new NotSupportedException(nameof(SetUserNameAsync));
        }

        public Task<string> GetNormalizedUserNameAsync(UserSys user, CancellationToken cancellationToken)
        {
            throw new NotSupportedException(nameof(GetNormalizedUserNameAsync));
        }

        public Task SetNormalizedUserNameAsync(UserSys user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)null);
        }

        public Task<IdentityResult> CreateAsync(UserSys user, CancellationToken cancellationToken)
        {
            throw new NotSupportedException(nameof(CreateAsync));
        }

        public Task<IdentityResult> UpdateAsync(UserSys user, CancellationToken cancellationToken)
        {
            throw new NotSupportedException(nameof(UpdateAsync));
        }

        public Task<IdentityResult> DeleteAsync(UserSys user, CancellationToken cancellationToken)
        {
            throw new NotSupportedException(nameof(DeleteAsync));
        }

        public async Task<UserSys> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await context.Users
                           .Include(i => i.UserRole)
                           .SingleOrDefaultAsync(p => p.Email.ToUpper().Equals(normalizedUserName), cancellationToken);

            return user;
        }

        public Task SetPasswordHashAsync(UserSys user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotSupportedException(nameof(SetPasswordHashAsync));
        }

        public Task<string> GetPasswordHashAsync(UserSys user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(UserSys user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.Password));
        }

        public async Task<UserSys> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (int.TryParse(userId, out int id))
            {
                return await context.Users.FindAsync(id);
            }
            else
            {
                return await Task.FromResult((UserSys)null);
            }
        }

        public Task AddToRoleAsync(UserSys user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(UserSys user, CancellationToken cancellationToken)
        {
            var role = await context.Roles.SingleAsync(r => r.Id == user.UserRoleId);

            IList<string> roles = new List<string> { role.Name.Trim() };

            return roles;
        }

        public Task<IList<UserSys>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(UserSys user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task RemoveFromRoleAsync(UserSys user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

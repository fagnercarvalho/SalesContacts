using Microsoft.AspNetCore.Identity;
using SalesContacts.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SalesContacts.Identity
{
    public class RoleStore : IRoleStore<UserRole>
    {
        public void Dispose()
        {
        }

        public Task<IdentityResult> CreateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task<IdentityResult> UpdateAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task<IdentityResult> DeleteAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task<string> GetRoleIdAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task<string> GetRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task SetRoleNameAsync(UserRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(UserRole role, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task SetNormalizedRoleNameAsync(UserRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task<UserRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }

        public Task<UserRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}


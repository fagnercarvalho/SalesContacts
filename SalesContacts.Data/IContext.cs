using Microsoft.EntityFrameworkCore;
using SalesContacts.Data.Model;

namespace SalesContacts.Data
{
    public interface IContext
    {
        DbSet<UserSys> Users { get; set; }

        DbSet<UserRole> Roles { get; set; }

        DbSet<City> Cities { get; set; }

        DbSet<Classification> Classifications { get; set; }

        DbSet<Customer> Customers { get; set; }

        DbSet<Gender> Genders { get; set; }

        DbSet<Region> Regions { get; set; }

        void Dispose();
    }
}

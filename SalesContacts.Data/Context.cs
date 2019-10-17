using Microsoft.EntityFrameworkCore;
using SalesContacts.Data.Model;


namespace SalesContacts.Data
{
    public class Context : DbContext, IContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<UserSys> Users { get; set; }

        public DbSet<UserRole> Roles { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Classification> Classifications { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<Region> Regions { get; set; }
    }
}

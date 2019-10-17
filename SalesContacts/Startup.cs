using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesContacts.Data;
using SalesContacts.Data.Model;
using SalesContacts.Extensions;
using SalesContacts.Identity;

namespace SalesContacts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IContext, Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<UserSys, UserRole>();

            services.AddTransient<IUserStore<UserSys>, UserStore>();
            services.AddTransient<IRoleStore<UserRole>, RoleStore>();

            services.AddTransient<IPasswordHasher<UserSys>, PasswordHasher<UserSys>>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
            });

            services
                .AddAuthentication();

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.EncryptUserPasswords(Configuration.GetConnectionString("Default"));

            app.UseStaticFiles();

            app.UseRouting();

            app
                .UseAuthentication()
                .UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

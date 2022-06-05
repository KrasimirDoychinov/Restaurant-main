using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.OpenIdConnect;
using Restaurant.Data;
using Restaurant.Data.Models;
using Restaurant.Services.Contracts;
using Restaurant.Services.Implementations;
using Restaurant.Services.Mapper;
using Restaurant.Services.Seeder;
using Restaurant.Web.Models;
using System.Reflection;
using Microsoft.Owin.Security;

namespace Restaurant.Web
{
    public class Startup
    {
        // TODO:
        // New user add to user role
        // Fix images
        // Modifiend on User
        // Add one more User
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddTransient<IGenericRepository<Product>, GenericRepository<Product>>();
            services.AddTransient<IGenericRepository<Order>, GenericRepository<Order>>();
            services.AddTransient<IGenericRepository<ProductOrder>, GenericRepository<ProductOrder>>();
            services.AddTransient<IGenericRepository<Coupon>, GenericRepository<Coupon>>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICouponService, CouponService>();
            services.AddTransient<IUserService, UserService>();

            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "557870745988731";
                options.AppSecret = "908e2db79f7199d2e416de9ffdc6aaa7";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            DataSeeder.SeedData(userManager, roleManager, context);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                      name: "areas",
                      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

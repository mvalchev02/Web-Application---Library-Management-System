using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1
{ 
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var connectionnString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BooksDbContext>(options =>
                options.UseSqlServer(connectionnString));

            var connectionnnString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ReadersDbContext>(options =>
                options.UseSqlServer(connectionnnString));

            var connectionnnnString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ReaderBookDbContext>(options =>
                options.UseSqlServer(connectionnnnString));

            var services = builder.Services;

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager =
                    scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Member", "Anonymous" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<IdentityRole>>();

                string emailAdmin = "admin@admin.com";
                string passwordAdmin = "Admin123!";

                string userIvKo = "IvKo@libr.com";
                string passIvKo = "IvKo123!";

                string userGoPo = "gopo@libr.com";
                string passGoPo = "GoPo123!";

                // Проверка и създаване на роли
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!await roleManager.RoleExistsAsync("Member"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Member"));
                }

                if (await userManager.FindByEmailAsync(emailAdmin) == null)
                {
                    var user = new IdentityUser();
                    user.UserName = emailAdmin;
                    user.Email = emailAdmin;


                    await userManager.CreateAsync(user, passwordAdmin);


                    await userManager.AddToRoleAsync(user, "Admin");

                }

                if (await userManager.FindByEmailAsync(userIvKo) == null)
                {
                    var user1 = new IdentityUser();
                    user1.UserName = userIvKo;
                    user1.Email = userIvKo;

                    await userManager.CreateAsync(user1, passIvKo);
                    await userManager.AddToRoleAsync(user1, "Member");

                }

                if (await userManager.FindByEmailAsync(userGoPo) == null)
                {
                    var user2 = new IdentityUser();
                    user2.UserName = userGoPo;
                    user2.Email = userGoPo;

                    await userManager.CreateAsync(user2, passGoPo);

                    await userManager.AddToRoleAsync(user2, "Member");
                }

            }


            app.Run();

        }


    }
}
    


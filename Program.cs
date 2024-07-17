using Microsoft.EntityFrameworkCore;
using StudentManager.Data;
using StudentManager.Services;
using Microsoft.AspNetCore.Identity;
using StudentManager.Areas.Identity.Data;

namespace StudentManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            builder.Services.AddScoped<IStudentService, StudentService>();

            builder.Services.AddDbContext<StudentContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentManagerConnectionString"))
            );

            builder.Services.AddDefaultIdentity<User>(
                options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<StudentContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}

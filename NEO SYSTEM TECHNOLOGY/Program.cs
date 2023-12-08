using Microsoft.EntityFrameworkCore;
using NEO_SYSTEM_TECHNOLOGY.Data;
using System.Globalization;

namespace NEO_SYSTEM_TECHNOLOGY
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru-RU");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("ru-RU") };
                options.RequestCultureProviders.Clear();
            });

            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlServer(connectionString));

            //builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            //opt.UseNpgsql(connectionString));

            //builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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
            app.UseRequestLocalization();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Organization}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
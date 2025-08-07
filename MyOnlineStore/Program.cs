using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace MyOnlineStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // === «·ﬂÊœ «·ÃœÌœ «·„÷«› ·Õ· „‘ﬂ·… «·√—ﬁ«„ ===
            // We define the culture we want to use, in this case, US English
            var supportedCultures = new[] { new CultureInfo("en-US") };

            // We apply this culture setting to the application
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            // === ‰Â«Ì… «·ﬂÊœ «·„÷«› ===

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
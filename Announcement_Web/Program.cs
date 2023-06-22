using Announcement_Repositories;
using Announcement_Repositories.Interfaces;
using Announcement_Repositories.Repositories;
using Announcement_Services.Interfaces;
using Announcement_Services.Services;
using Microsoft.EntityFrameworkCore;
using Task_Service.Filters;
using Task_Service.Mapper;

namespace Announcement_Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(AppMappingProfile).Assembly);

            builder.Services.AddControllersWithViews(options =>
                options.Filters.Add(typeof(NotImplExceptionFilterAttribute)));

            builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
            builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();

            var connection = builder.Configuration.GetConnectionString("AnnouncementConnection");

            builder.Services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(connection));

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
                pattern: "{controller=Home}/{action=HomePage}");

            app.Run();
        }
    }
}
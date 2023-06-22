using Announcement_Repositories;
using Announcement_Repositories.Interfaces;
using Announcement_Repositories.Repositories;
using Announcement_Services.Interfaces;
using Announcement_Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Task_Service.Filters;
using Task_Service.Mapper;

namespace Announcement_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(AppMappingProfile).Assembly);

            builder.Services.AddControllers(options =>
                options.Filters.Add(typeof(NotImplExceptionFilterAttribute)));

            builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
            builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();

            var connection = builder.Configuration.GetConnectionString("AnnouncementConnection");

            builder.Services.AddDbContext<ApplicationContext>(options =>
              options.UseSqlServer(connection));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseAuthorization();

            app.Run();
        }
    }
}
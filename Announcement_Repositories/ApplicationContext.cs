using Announcement_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Announcement_Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Announcement> Announcements { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
    }
}
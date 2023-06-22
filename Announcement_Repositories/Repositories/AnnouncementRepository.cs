using Announcement_Domain.Models;
using Announcement_Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Announcement_Domain.Exeptions;
using Announcement_Domain.Models.NotDbModels;
using Microsoft.Data.SqlClient;

namespace Announcement_Repositories.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ApplicationContext _db;

        public AnnouncementRepository(ApplicationContext context)
        {
            _db = context;
        }

        public IQueryable<Announcement> GetAll()
        {
            return _db.Announcements.Where(x => x.DateOfArchiving == null);
        }

        public async Task<Announcement> GetById(int id)
        {
            var announcement = await _db.Announcements.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (announcement == null)
                throw new NotFoundException();

            return announcement;
        }

        public async Task<List<Announcement>> GetSimilarAnnouncements(int id, string[] titleWords, string[] descriptionWords, int top)
        {
            var titleConditions = string.Join(" OR ", titleWords.Select((word, index) => $"A.Title LIKE @Title{index}"));
            var descriptionConditions = string.Join(" OR ", descriptionWords.Select((word, index) => $"A.Description LIKE @Description{index}"));
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id),
                new SqlParameter("@Top", top)
            };

            for (int i = 0; i < titleWords.Length; i++)
            {
                parameters.Add(new SqlParameter($"@Title{i}", $"%{titleWords[i]}%"));
            }

            for (int i = 0; i < descriptionWords.Length; i++)
            {
                parameters.Add(new SqlParameter($"@Description{i}", $"%{descriptionWords[i]}%"));
            }

            var query = $@"
                SELECT TOP (@Top) A.* 
                FROM Announcements A 
                WHERE A.Id <> @Id AND ({titleConditions}) AND ({descriptionConditions})
                ORDER BY (LEN(A.Title) - LEN(REPLACE(A.Title, ' ', ''))) + (LEN(A.Description) - LEN(REPLACE(A.Description, ' ', ''))) DESC";

            var announcements = await _db.Announcements.FromSqlRaw(query, parameters.ToArray()).ToListAsync();

            return announcements;
        }

        public async Task<bool> Add(Announcement announcement)
        {
            if(announcement == null) 
                throw new ValidationException();

            await _db.Announcements.AddAsync(announcement);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteById(int id)
        {
            var dbAnnouncment = await GetById(id);

            _db.Announcements.Attach(dbAnnouncment);

            dbAnnouncment.DateOfArchiving = DateTime.Now;

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Announcement announcement)
        {
            if(announcement == null)
                throw new ValidationException();

            _db.Announcements.Attach(announcement);

            announcement.DateOfArchiving = DateTime.Now;

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(Announcement announcement)
        {
            if (announcement == null)
                throw new ValidationException();

            _db.Announcements.Update(announcement);

            await _db.SaveChangesAsync();

            return true;
        }       
    }
}

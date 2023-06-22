using Announcement_Domain.Models;

namespace Announcement_Repositories.Interfaces
{
    public interface IAnnouncementRepository
    {
        IQueryable<Announcement> GetAll();
        Task<Announcement> GetById(int id);
        Task<List<Announcement>> GetSimilarAnnouncements(int id, string[] titleWords, string[] descriptionWords, int top);
        Task<bool> Add(Announcement announcement);
        Task<bool> DeleteById(int id);
        Task<bool> Delete(Announcement announcement);
        Task<bool> Update(Announcement announcement);
    }
}

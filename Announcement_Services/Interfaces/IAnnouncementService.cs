using Announcement_Domain.CreateModels;
using Announcement_Domain.DtoModels;
using Announcement_Domain.Models;

namespace Announcement_Services.Interfaces
{
    public interface IAnnouncementService
    {
        Task<List<DtoAnnouncement>> GetAllTitle();
        Task<Announcement> GetById(int id);
        Task<List<DtoAnnouncement>> GetSimilarTitle(int id);
        Task Add(CreateAnnouncement announcement);
        Task DeleteById(int id);
        Task Update(CreateAnnouncement announcement, int id);
    }
}

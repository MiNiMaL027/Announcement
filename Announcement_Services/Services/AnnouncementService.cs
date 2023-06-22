using Announcement_Domain.CreateModels;
using Announcement_Domain.DtoModels;
using Announcement_Domain.Exeptions;
using Announcement_Domain.Models;
using Announcement_Repositories.Interfaces;
using Announcement_Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Announcement_Services.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IMapper _mapper;

        public AnnouncementService(IAnnouncementRepository announcementRepository, IMapper mapper)
        {
            _announcementRepository = announcementRepository;
            _mapper = mapper;
        }

        public async Task<List<DtoAnnouncement>> GetAllTitle()
        {
            var announcement = _announcementRepository.GetAll();
            return await announcement.Select(x => _mapper.Map<DtoAnnouncement>(x)).ToListAsync();
        }

        public async Task<Announcement> GetById(int id)
        {
            return await _announcementRepository.GetById(id);
        }

        public async Task<List<DtoAnnouncement>> GetSimilarTitle(int id)
        {
            var announcement = await _announcementRepository.GetById(id);

            string[] titleWords = announcement.Title.Split('.', '?', '!', ',', ';', ':', '(', ')', ' ');
            string[] descriptionWords = announcement.Description.Split('.', '?', '!', ',', ';', ':', '(', ')', ' ');

            var announcements = await _announcementRepository.GetSimilarAnnouncements(id, titleWords, descriptionWords, 3);

            return _mapper.Map<List<DtoAnnouncement>>(announcements);
        }

        public async Task Add(CreateAnnouncement announcement)
        {
            if (announcement == null)
                throw new ValidationException();

            var newAnnouncement = _mapper.Map<Announcement>(announcement);

            newAnnouncement.DateAdded = DateTime.Now;

            await _announcementRepository.Add(newAnnouncement);
        }

        public async Task DeleteById(int id)
        {
            await _announcementRepository.DeleteById(id);
        }

        public async Task Update(CreateAnnouncement announcement, int id)
        {
            var oldAnnoucement = await _announcementRepository.GetById(id);

            var newAnnoucement = _mapper.Map<Announcement>(announcement);

            if(announcement.Title == null)
                newAnnoucement.Title = oldAnnoucement.Title;

            if(announcement.Description == null)
                newAnnoucement.Description = oldAnnoucement.Description;

            newAnnoucement.DateAdded = oldAnnoucement.DateAdded;
            newAnnoucement.Id = id;

            await _announcementRepository.Update(newAnnoucement);
        }
    }
}

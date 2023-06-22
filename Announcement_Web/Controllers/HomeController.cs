using Announcement_Domain.CreateModels;
using Announcement_Domain.DtoModels;
using Announcement_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Announcement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnnouncementService _announcementService;

        public HomeController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public IActionResult HomePage()
        {
            return View("HomePage");
        }

        [HttpGet]
        public async Task<ActionResult<List<DtoAnnouncement>>> GetAllAnnouncementsTitle()
        {
            return Ok(await _announcementService.GetAllTitle());
        }

        [HttpGet]
        public async Task<ActionResult<Announcement_Domain.Models.Announcement>> GetAnnouncementById(int id)
        {
            return Ok(await _announcementService.GetById(id));
        }

        [HttpGet]
        public async Task<ActionResult<List<DtoAnnouncement>>> GetSimilarAnnouncementsTitle(int id)
        {
            return Ok(await _announcementService.GetSimilarTitle(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement(CreateAnnouncement announcement)
        {
            await _announcementService.Add(announcement);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAnnouncementById(int id)
        {
            await _announcementService.DeleteById(id);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAnnouncement(CreateAnnouncement announcement, int id)
        {
            await _announcementService.Update(announcement, id);

            return Ok();
        }
    }
}

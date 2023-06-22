using Announcement_Domain.CreateModels;
using Announcement_Domain.DtoModels;
using Announcement_Domain.Models;
using Announcement_Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Announcement_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }
        
        [HttpGet("GetAllAnnouncementTitle")]
        public async Task<ActionResult<List<DtoAnnouncement>>> GetAllAnnouncementsTitle()
        {
            return Ok(await _announcementService.GetAllTitle());
        }

        [HttpGet("GetAnnouncementById")]
        public async Task<ActionResult<Announcement>> GetAnnouncementById(int id)
        {
            return Ok(await _announcementService.GetById(id));
        }

        [HttpGet("GetSimilarAnnouncement")]
        public async Task<ActionResult<List<DtoAnnouncement>>> GetSimilarAnnouncementsTitle(int id)
        {
            return Ok(await _announcementService.GetSimilarTitle(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAnnouncement(CreateAnnouncement announcement)
        {
            await _announcementService.Add(announcement);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAnnouncementById(int id)
        {
            await _announcementService.DeleteById(id);

            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAnnouncement(CreateAnnouncement announcement, int id)
        {
            await _announcementService.Update(announcement,id);

            return Ok();
        }
    }
}

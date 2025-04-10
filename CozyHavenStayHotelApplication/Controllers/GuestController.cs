using CozyHavenStayHotelApplication.Interfaces;
using CozyHavenStayHotelApplication.Models.DTOs;
using CozyHavenStayHotelApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayHotelApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpPost("RegisterGuest")]
        [AllowAnonymous]
        public async Task<ActionResult<CreateGuestResponse>> RegisterGuest(CreateGuestRequest request)
        {
            try
            {
                var result = await _guestService.RegisterGuest(request);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllGuest")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetGuestResponse>>> GetAllGuests()
        {
            try
            {
                var result = await _guestService.GetAllGuests();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetGuestBy/{id}")]
        [Authorize]
        public async Task<ActionResult<GetGuestResponse>> GetGuestById(int id)
        {
            try
            {
                var result = await _guestService.GetGuestById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("UpdateGuest")]
        [Authorize]
        public async Task<ActionResult<GetGuestResponse>> UpdateGuest(UpdateGuestRequest request)
        {
            try
            {
                var result = await _guestService.UpdateGuestDetails(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost("SearchGuest")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetGuestResponse>>> FilterGuests([FromBody] GuestRequest request)
        {
            try
            {
                var result = await _guestService.GetGuestsByFilter(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteGuestBy/{guestId}")]
        [Authorize]
        public async Task<ActionResult> DeleteGuest(int guestId)
        {
            try
            {
                var deleted = await _guestService.DeleteGuest(guestId);
                if (deleted)
                    return Ok("Guest deleted successfully");
                return NotFound("Guest not found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}

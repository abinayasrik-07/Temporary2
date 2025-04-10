using CozyHavenStayHotelApplication.Interfaces;
using CozyHavenStayHotelApplication.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayHotelApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("RegisterAdmin")]
        [AllowAnonymous]
        public async Task<ActionResult<CreateAdminResponse>> RegisterAdmin(CreateAdminRequest request)
        {
            try
            {
                var result = await _adminService.RegisterAdmin(request);
                return Created("", result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllAdmin")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetAdminResponse>>> GetAllAdmins()
        {
            try
            {
                var result = await _adminService.GetAllAdmins();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAdminById")]
        [Authorize]
        public async Task<ActionResult<GetAdminResponse>> GetAdminById(int id)
        {
            try
            {
                var result = await _adminService.GetAdminById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("UpdateAdmin")]
        [Authorize]
        public async Task<ActionResult<GetAdminResponse>> UpdateAdmin(UpdateAdminRequest request)
        {
            try
            {
                var result = await _adminService.UpdateAdmin(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("SearchAdmin")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<GetAdminResponse>>> SearchAdmin([FromBody] AdminRequest request)
        {
            try
            {
                var result = await _adminService.GetAdminsByFilter(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpDelete("DeleteAdminById")]
        [Authorize]
        public async Task<ActionResult> DeleteAdmin(int id)
        {
            try
            {
                var deleted = await _adminService.DeleteAdmin(id);
                if (deleted)
                    return Ok("Admin deleted successfully");
                return NotFound("Admin not found");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

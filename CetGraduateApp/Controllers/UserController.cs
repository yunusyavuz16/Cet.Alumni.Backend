using System.Security.Claims;
using CetGraduateApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CetGraduateApp.Controllers;

[EnableCors("AllowAnyOrigin")]
[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserManager _userManager;

    public UserController(UserManager userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("getUserInfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        var user = await _userManager.GetUserByIdAsync(int.Parse(userId));
        if (user == null)
        {
            return NotFound();
        }

        // return 
        return Ok(new
        {
            AlumniStudentNo = user.AlumniStudentNo, FirstName = user.FirstName, LastName = user.LastName,
            Email = user.EmailAddress
        });
    }
}
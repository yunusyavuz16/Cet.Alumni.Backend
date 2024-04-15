using CetGraduateApp.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CetGraduateApp.Controllers;

[EnableCors("AllowSpecificOrigin")]
[ApiController]
[Route("api/announcement")]
public class AnnouncementController : ControllerBase
{
    private readonly AlumniDbContext _context;

    public AnnouncementController(AlumniDbContext context)
    {
        _context = context;
    }

    // Get By Length
    [HttpGet("getByLength/{length}")]
    public async Task<IActionResult> GetByLength(int length)
    {
        var announcements = await _context.Announcements.OrderByDescending(a => a.AnouncementDateTime).Take(length)
            .ToListAsync();
        return Ok(announcements);
    }
}
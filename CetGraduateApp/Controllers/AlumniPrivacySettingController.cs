using CetGraduateApp.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CetGraduateApp.Controllers;


[ApiController]
[Route("api")]
[EnableCors("AllowSpecificOrigin")]
public class AlumniPrivacySettingController : ControllerBase
{
    // get data privacy settings
    private readonly AlumniDbContext _context;

    public AlumniPrivacySettingController(AlumniDbContext context)
    {
        _context = context;
    }

    [HttpGet("getAlumniPrivacySettings")]
    public async Task<IActionResult> GetAlumniPrivacySettings()
    {
        var alumniPrivacySettings = await _context.AlumniPrivacySettings.ToListAsync();
        return Ok(alumniPrivacySettings);
    }
}
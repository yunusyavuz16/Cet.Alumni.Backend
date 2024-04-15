using CetGraduateApp.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CetGraduateApp.Controllers;

[ApiController]
[Route("api/alumni")]
[EnableCors("AllowSpecificOrigin")]
public class AlumniController : ControllerBase
{
    private readonly AlumniDbContext _context;

    public AlumniController(AlumniDbContext context)
    {
        _context = context;
    }

    // gel privacy public alumnies by term id
    [HttpGet("getAlumniByTerm/{termId}")]
    public async Task<IActionResult> GetAlumniByTerm(int termId)
    {
        // Validate input to prevent unexpected behavior
        if (termId <= 0)
        {
            return BadRequest("Invalid termId");
        }

        var alumnies = await _context.Alumni.Where(a => a.AlumniPrivacySettingId == 1 && a.TermId == termId)
            .Include(a => a.Term)
            .ToListAsync();
        return Ok(alumnies);
    }

    // get all alumnies public
    [HttpGet("getAllAlumni")]
    public async Task<IActionResult> GetAllAlumni()
    {
        var alumnies = await _context.Alumni.Where(a => a.AlumniPrivacySettingId == 1).Include(a => a.Term)
            .ToListAsync();
        return Ok(alumnies);
    }
}
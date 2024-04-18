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
        // do not return password, registration and passwords
        var newAlumniList = alumnies.Select(a => new AlumniViewModel
        {
            AlumniStudentNo = a.AlumniStudentNo,
            FirstName = a.FirstName,
            LastName = a.LastName,
            EmailAddress = a.EmailAddress,
            DepartmentId = a.DepartmentId,
            TermId = a.TermId,
            ProfileDescription = a.ProfileDescription,
            Sector = a.Sector,
            Company = a.Company,
            JobTitle = a.JobTitle,
            AlumniPrivacySettingId = a.AlumniPrivacySettingId,
            BirthDate = a.BirthDate,
            LinkedInUrl = a.LinkedInUrl,
            Term = a.Term
        }).ToList();

        return Ok(newAlumniList);
    }

    // get all alumnies public
    [HttpGet("getAllAlumni")]
    public async Task<IActionResult> GetAllAlumni()
    {
        var alumnies = await _context.Alumni.Where(a => a.AlumniPrivacySettingId == 1).Include(a => a.Term)
            .ToListAsync();
        // do not return password, registration and isVerified
        var newAlumniList = alumnies.Select(a => new AlumniViewModel
        {
            AlumniStudentNo = a.AlumniStudentNo,
            FirstName = a.FirstName,
            LastName = a.LastName,
            EmailAddress = a.EmailAddress,
            DepartmentId = a.DepartmentId,
            TermId = a.TermId,
            ProfileDescription = a.ProfileDescription,
            Sector = a.Sector,
            Company = a.Company,
            JobTitle = a.JobTitle,
            AlumniPrivacySettingId = a.AlumniPrivacySettingId,
            BirthDate = a.BirthDate,
            LinkedInUrl = a.LinkedInUrl,
            Term = a.Term
        }).ToList();

        return Ok(alumnies);
    }

    [HttpGet("getAlumniByAlumniStudentNo/{alumniStudentNo}")]
    [Authorize]
    public async Task<IActionResult> GetAlumniByAlumniStudentNo(int alumniStudentNo)
    {
        var alumni = await _context.Alumni
            .Where(a => a.AlumniStudentNo == alumniStudentNo && a.AlumniPrivacySettingId == 1)
            .Include(a => a.Term)
            .FirstOrDefaultAsync();
        if (alumni == null)
        {
            return NotFound();
        }

        // do not return password, registration and isVerified
        var newAlumni = new AlumniViewModel
        {
            AlumniStudentNo = alumni.AlumniStudentNo,
            FirstName = alumni.FirstName,
            LastName = alumni.LastName,
            EmailAddress = alumni.EmailAddress,
            DepartmentId = alumni.DepartmentId,
            TermId = alumni.TermId,
            ProfileDescription = alumni.ProfileDescription,
            Sector = alumni.Sector,
            Company = alumni.Company,
            JobTitle = alumni.JobTitle,
            AlumniPrivacySettingId = alumni.AlumniPrivacySettingId,
            BirthDate = alumni.BirthDate,
            LinkedInUrl = alumni.LinkedInUrl,
            Term = alumni.Term
        };

        return Ok(newAlumni);
    }
}
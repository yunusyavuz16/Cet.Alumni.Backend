using CetGraduateApp.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CetGraduateApp.Controllers;

[EnableCors("AllowAnyOrigin")]
[ApiController]
[Route("api/term")]
public class TermController : ControllerBase
{
    private readonly AlumniDbContext _context;

    public TermController(AlumniDbContext context)
    {
        _context = context;
    }

    // get all terms
    [HttpGet("getAllTerms")]
    public async Task<IActionResult> GetAllTerms()
    {
        // sort by term year
        var terms = await _context.Terms.OrderBy(t => t.TermYear).ToListAsync();
        return Ok(terms);
    }
}
using System.Security.Claims;
using CetGraduateApp.Context;
using CetGraduateApp.Entities;
using CetGraduateApp.Models.JobPostingModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CetGraduateApp.Controllers;

[ApiController]
[Route("api")]
[EnableCors("AllowSpecificOrigin")]
public class JobPostingController : ControllerBase
{
    private readonly AlumniDbContext _context;

    public JobPostingController(AlumniDbContext context)
    {
        _context = context;
    }

    // crud operations for job posting

    // get all job postings
    [HttpGet("getAllJobPostings")]
    public async Task<IActionResult> GetAllJobPostings()
    {
        var jobPostings = await _context.JobPosts.Include(el => el.PublisherStudentNoNavigation).ToListAsync();

        // just include required alumni student data in response
        jobPostings.ForEach(el =>
        {
            el.PublisherStudentNoNavigation.IsVerified = null;
            el.PublisherStudentNoNavigation.LastSignedInDateTime = null;
            el.PublisherStudentNoNavigation.AlumniRegistrations = null;
            el.PublisherStudentNoNavigation.Password = null;
            el.PublisherStudentNoNavigation.JobPosts = null;
            el.PublisherStudentNoNavigation.Term = null;
        });
        return Ok(jobPostings);
    }

    // get job posting by id
    [HttpGet("getJobPostingById/{id}")]
    public async Task<IActionResult> GetJobPostingById(int id)
    {
        var jobPosting = await _context.JobPosts.FindAsync(id);
        if (jobPosting == null)
        {
            return NotFound();
        }

        return Ok(jobPosting);
    }

    // create job posting
    [Authorize]
    [HttpPost("createJobPosting")]
    public async Task<IActionResult> CreateJobPosting(JobPostingCreateModel jobPosting)
    {
        // get user from token
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _context.Alumni.FindAsync(userId);
        if (user == null)
        {
            return Unauthorized();
        }
        JobPost newPost = new JobPost()
        {
            Title = jobPosting.Title,
            Description = jobPosting.Description,
            Deadline = jobPosting.Deadline,
            DatePosted = jobPosting.DatePosted,
            Location = jobPosting.Location,
            PublisherStudentNo = user.AlumniStudentNo,
            Requirements = jobPosting.Requirements,
            Responsibilities = jobPosting.Responsibilities,
            CompanyName = jobPosting.CompanyName,
            ContactInfo = jobPosting.ContactInfo,
            ContactFullName = jobPosting.ContactFullName
        };
        _context.JobPosts.Add(newPost);
        await _context.SaveChangesAsync();
        return Ok(newPost);
    }

    // update job posting
    [Authorize]
    [HttpPut("updateJobPosting")]
    public async Task<IActionResult> UpdateJobPosting(JobPost jobPosting)
    {
        _context.JobPosts.Update(jobPosting);
        await _context.SaveChangesAsync();
        return Ok(jobPosting);
    }

    // delete job posting
    [Authorize]
    [HttpDelete("deleteJobPosting/{id}")]
    public async Task<IActionResult> DeleteJobPosting(int id)
    {
        var jobPosting = await _context.JobPosts.FindAsync(id);
        if (jobPosting == null)
        {
            return NotFound();
        }

        _context.JobPosts.Remove(jobPosting);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
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

    // get job postings by publisher student no
    [HttpGet("getJobPostingsByPublisherStudentNo/{studentNo}")]
    public async Task<IActionResult> GetJobPostingsByPublisherStudentNo(int studentNo)
    {
        var jobPostings = await _context.JobPosts.Where(el => el.PublisherStudentNo == studentNo).ToListAsync();
        return Ok(jobPostings);
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
        // Check if the user is authenticated
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _context.Alumni.FindAsync(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        // Find the existing job post
        var existingJobPost = await _context.JobPosts.FindAsync(jobPosting.JobPostId);
        if (existingJobPost == null)
        {
            return NotFound(); // Job post not found
        }

        // Check if the user is authorized to update this job post
        if (existingJobPost.PublisherStudentNo != user.AlumniStudentNo)
        {
            return Unauthorized();
        }

        // Update the properties of the existing job post
        existingJobPost.Title = jobPosting.Title;
        existingJobPost.Description = jobPosting.Description;
        existingJobPost.Deadline = jobPosting.Deadline;
        existingJobPost.DatePosted = jobPosting.DatePosted;
        existingJobPost.Location = jobPosting.Location;
        existingJobPost.Requirements = jobPosting.Requirements;
        existingJobPost.Responsibilities = jobPosting.Responsibilities;
        existingJobPost.CompanyName = jobPosting.CompanyName;
        existingJobPost.ContactInfo = jobPosting.ContactInfo;
        existingJobPost.ContactFullName = jobPosting.ContactFullName;

        try
        {
            await _context.SaveChangesAsync();
            return Ok(existingJobPost); // Return the updated job post
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.JobPosts.Any(e => e.JobPostId == jobPosting.JobPostId))
            {
                return NotFound(); // Job post not found
            }
            else
            {
                throw; // Rethrow exception
            }
        }
    }



    // delete job posting
    [Authorize]
    [HttpDelete("deleteJobPosting/{id}")]
    public async Task<IActionResult> DeleteJobPosting(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _context.Alumni.FindAsync(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        var jobPosting = await _context.JobPosts.FindAsync(id);

        if (jobPosting.PublisherStudentNo != user.AlumniStudentNo)
        {
            return Unauthorized();
        }

        if (jobPosting == null)
        {
            return NotFound();
        }

        _context.JobPosts.Remove(jobPosting);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
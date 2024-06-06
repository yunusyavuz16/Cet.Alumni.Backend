namespace CetGraduateApp.Models.JobPostingModels;

public class JobPostingCreateModel
{
    // required fields and not include like jobPostingId and not include a field is not in JobPost model
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? CompanyName { get; set; }

    public string? Location { get; set; }
    
    public string ContactFullName { get; set; }

    
    public DateTime? DatePosted { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Requirements { get; set; }

    public string? Responsibilities { get; set; }

    public string? ContactInfo { get; set; }
    
}
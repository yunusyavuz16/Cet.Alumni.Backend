using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class JobPost
{
    public int JobPostId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? CompanyName { get; set; }

    public string? Location { get; set; }

    public int? PublisherStudentNo { get; set; }

    public int? JobPostTypeId { get; set; }

    public DateTime? DatePosted { get; set; }

    public DateTime? Deadline { get; set; }

    public string? Requirements { get; set; }

    public string? Responsibilities { get; set; }

    public string? ContactInfo { get; set; }

    public string? ContactFullName { get; set; }

    public virtual JobPostType? JobPostType { get; set; }

    public virtual Alumni? PublisherStudentNoNavigation { get; set; }
}

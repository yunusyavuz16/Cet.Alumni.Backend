using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class JobPost
{
    public int JobPostId { get; set; }

    public string Title { get; set; } = null!;

    public string? InformationDetail { get; set; }

    public int UserId { get; set; }

    public int JobPostTypeId { get; set; }

    public DateTime CreateDateTime { get; set; }

    public virtual JobPostType JobPostType { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}

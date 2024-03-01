using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class JobPostType
{
    public int JobPostTypeId { get; set; }

    public string DisplayName { get; set; } = null!;

    public string TypeCode { get; set; } = null!;

    public DateTime CreateDateTime { get; set; }

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
}

using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class Announcement
{
    public int AnnouncementId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public DateTime? AnouncementDateTime { get; set; }
}

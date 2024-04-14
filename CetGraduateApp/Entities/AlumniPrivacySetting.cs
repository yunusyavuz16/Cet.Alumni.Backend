using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class AlumniPrivacySetting
{
    public int AlumniPrivacySettingId { get; set; }

    public string? DisplayName { get; set; }

    public string? SettingCode { get; set; }

    public virtual ICollection<Alumni> Alumni { get; set; } = new List<Alumni>();
}

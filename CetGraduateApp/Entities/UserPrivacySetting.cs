using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class UserPrivacySetting
{
    public int UserPrivacySettingId { get; set; }

    public string? DisplayName { get; set; }

    public string? SettingCode { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

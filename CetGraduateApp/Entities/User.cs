using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? IsVerified { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public DateTime? LastSignedInDateTime { get; set; }

    public int? DepartmentId { get; set; }

    public string? EntranceYear { get; set; }

    public string? GraduateYear { get; set; }

    public string? InformationDetail { get; set; }

    public int UserPrivacySettingId { get; set; }

    public virtual ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();

    public virtual UserPrivacySetting UserPrivacySetting { get; set; } = null!;

    public virtual ICollection<UserRegistration> UserRegistrations { get; set; } = new List<UserRegistration>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

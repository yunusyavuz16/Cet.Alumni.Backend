using System;
using System.Collections.Generic;

namespace CetGraduateApp.Entities;

public partial class Alumni
{
    public int AlumniStudentNo { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }

    public string? Password { get; set; }

    public bool? IsVerified { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? LastSignedInDateTime { get; set; }

    public int? DepartmentId { get; set; }

    public int? TermId { get; set; }

    public string? ProfileDescription { get; set; }

    public string? Sector { get; set; }

    public string? Company { get; set; }

    public string? JobTitle { get; set; }

    public int AlumniPrivacySettingId { get; set; }

    public virtual AlumniPrivacySetting AlumniPrivacySetting { get; set; } = null!;

    public virtual ICollection<AlumniRegistration> AlumniRegistrations { get; set; } = new List<AlumniRegistration>();

    public virtual ICollection<AlumniRole> AlumniRoles { get; set; } = new List<AlumniRole>();

    public virtual Term? Term { get; set; }
}

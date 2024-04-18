using CetGraduateApp.Entities;

public class AlumniViewModel
{
    public int AlumniStudentNo { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailAddress { get; set; }


    public int? DepartmentId { get; set; }

    public int? TermId { get; set; }

    public string? ProfileDescription { get; set; }

    public string? Sector { get; set; }

    public string? Company { get; set; }

    public string? JobTitle { get; set; }

    public int AlumniPrivacySettingId { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? LinkedInUrl { get; set; }

    public virtual AlumniPrivacySetting AlumniPrivacySetting { get; set; } = null!;

    public virtual Term? Term { get; set; }
}
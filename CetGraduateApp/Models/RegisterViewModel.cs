using System.ComponentModel.DataAnnotations;

namespace CetGraduateApp.Models;

public class RegisterViewModel
{
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public int AlumniStudentNo { get; set; }

    [Required] [StringLength(50)] public string FirstName { get; set; }

    [Required] [StringLength(50)] public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(16, MinimumLength = 6)]
    public string Password { get; set; }


    [StringLength(100)] public string Company { get; set; }

    [Required] public int AlumniPrivacySettingId { get; set; }

    [Required] public int TermId { get; set; }

    [Required] [StringLength(100)] public string JobTitle { get; set; }

    [StringLength(100)] public string Sector { get; set; }

    [StringLength(100)] public string AlumniProfileDescription { get; set; }
}
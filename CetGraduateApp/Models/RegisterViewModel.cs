using System.ComponentModel.DataAnnotations;

namespace CetGraduateApp.Models;

public class RegisterViewModel
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; }
    
}
using System.ComponentModel.DataAnnotations;

namespace CetGraduateApp.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; }

}
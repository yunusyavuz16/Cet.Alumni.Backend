using CetGraduateApp.Entities;
using CetGraduateApp.Helpers;
using CetGraduateApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CetGraduateApp.Controllers;

[EnableCors("AllowAnyOrigin")]
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JWTSettings _jwtSettings;

    private readonly UserManager _userManager;


    public AuthController(IOptions<JWTSettings> jwtSettings, UserManager userManager)
    {
        _userManager = userManager;

        _jwtSettings = jwtSettings.Value;
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        // Validate model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        // Find the user by email
        var user = await _userManager.FindUserByEmailAsync(model.Email);


        var _authManager = new AuthManager(_jwtSettings);
        // Check if the user exists and the provided password is correct
        if (user == null || !_authManager.VerifyPassword(model.Password, user.Password))
        {
            return Unauthorized("Invalid email or password");
        }


        user.LastSignedInDateTime = DateTime.UtcNow;

        await _userManager.UpdateUserAsync(user);

        // Generate a JWT token
        var token = _authManager.GenerateJwtToken(user);

        // Return the token (and optionally additional user details)
        return Ok(
            new { Token = token, UserId = user.UserId, FirstName = user.FirstName, LastName = user.LastName });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
    {
        // Validate model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the email is already registered
        var existingUser = await _userManager.FindUserByEmailAsync(model.Email);
        if (existingUser != null)
        {
            return BadRequest(new { Message = "Email is already registered", Code = 400 });
        }

        var _authManager = new AuthManager(_jwtSettings);

        // Create a new user
        var newUser = new User()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            EmailAddress = model.Email,
            Password = _authManager.HashPassword(model.Password),
            CreatedDateTime = DateTime.UtcNow,
            IsVerified = false,
            UserPrivacySettingId = 1
        };

        await _userManager.CreateUserAsync(newUser);

        //var dbUser = await _userManager.FindUserByEmailAsync(model.Email);
        // var verificationLink = _authManager.GenerateVerificationLink(dbUser);  
        // var emailBody = $"Click the following link to verify your email: {verificationLink}";
        // await _emailService.SendEmailAsync(newUser.EmailAddress, "Email Verification", emailBody);

        // Generate a JWT token and put to return object


        return Ok(new { Message = "Registration successful", Code = 200 });
    }
}
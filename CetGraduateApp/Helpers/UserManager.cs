using CetGraduateApp.Context;
using CetGraduateApp.Entities;

namespace CetGraduateApp.Helpers;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UserManager
{
    private readonly GraduateDbContext _context;


    public UserManager(IConfiguration configuration, GraduateDbContext context)
    {
        _context = context;
    }

    // User-related operations

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        return await _context.Users.FindAsync(userId);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<User?> FindUserByEmailAsync(string email)
    {
        // Perform a case-insensitive search for the user with the specified email
        return await _context.Users
            .FirstOrDefaultAsync(u => u.EmailAddress.Equals(email));
    }

    public async Task AddUserRegistrationAsync(UserRegistration userRegistration)
    {
        // Perform a case-insensitive search for the user with the specified email

        _context.UserRegistrations.Add(userRegistration);
        await _context.SaveChangesAsync();
    }

    public async Task<UserRegistration?> GetLastVerificationCodeAsync(int userId)
    {
        return await _context.UserRegistrations
            .Where(ur => ur.UserId == userId)
            .OrderByDescending(ur => ur.CreateDateTime)
            .FirstOrDefaultAsync();
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
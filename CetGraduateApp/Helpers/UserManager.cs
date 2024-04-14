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
    private readonly AlumniDbContext _context;


    public UserManager(IConfiguration configuration, AlumniDbContext context)
    {
        _context = context;
    }

    // User-related operations

    public async Task<Alumni?> GetUserByIdAsync(int alumniStudentNo)
    {
        return await _context.Alumni.FindAsync(alumniStudentNo);
    }


    public async Task<List<Alumni>> GetAllUsersAsync()
    {
        return await _context.Alumni.ToListAsync();
    }

    public async Task CreateUserAsync(Alumni alumni)
    {
        _context.Alumni.Add(alumni);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(Alumni user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int alumniStudentNo)
    {
        var user = await _context.Alumni.FindAsync(alumniStudentNo);
        if (user != null)
        {
            _context.Alumni.Remove(user);
            await _context.SaveChangesAsync();
        }
    }


    public async Task<Alumni?> FindUserByEmailAsync(string email)
    {
        // Perform a case-insensitive search for the user with the specified email
        return await _context.Alumni
            .FirstOrDefaultAsync(u => u.EmailAddress.Equals(email));
    }

    public async Task AddUserRegistrationAsync(AlumniRegistration userRegistration)
    {
        // Perform a case-insensitive search for the user with the specified email

        _context.AlumniRegistrations.Add(userRegistration);
        await _context.SaveChangesAsync();
    }

    public async Task<AlumniRegistration?> GetLastVerificationCodeAsync(int userId)
    {
        return await _context.AlumniRegistrations
            .Where(ur => ur.AlumniStudentNo == userId)
            .OrderByDescending(ur => ur.CreateDateTime)
            .FirstOrDefaultAsync();
    }


    public void Dispose()
    {
        _context.Dispose();
    }
}
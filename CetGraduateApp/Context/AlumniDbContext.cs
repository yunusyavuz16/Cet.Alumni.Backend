using System;
using System.Collections.Generic;
using CetGraduateApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CetGraduateApp.Context;

public partial class AlumniDbContext : DbContext
{
    public AlumniDbContext()
    {
    }

    public AlumniDbContext(DbContextOptions<AlumniDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<JobPost> JobPosts { get; set; }

    public virtual DbSet<JobPostType> JobPostTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPrivacySetting> UserPrivacySettings { get; set; }

    public virtual DbSet<UserRegistration> UserRegistrations { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=awseb-e-qmfumqmr8e-stack-awsebrdsdatabase-nqhgplsmjxpx.chbgdpvcdxf8.eu-north-1.rds.amazonaws.com,1433;Integrated Security=false;User ID=db_alumni_user;Password=alumniStrongPassword@1234!;Database=AlumniRdsDb;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Announce__9C77CBC6627E705D");

            entity.ToTable("Announcement");

            entity.Property(e => e.AnnouncementId).HasColumnName("announcementId");
            entity.Property(e => e.AnouncementDateTime).HasColumnName("anouncementDateTime");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .HasColumnName("author");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedDateTime).HasColumnName("createdDateTime");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedDateTime).HasColumnName("updatedDateTime");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__F9B8346D011E4A5C");

            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentCode, "UQ__Departme__7BF423ADAC39790D").IsUnique();

            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.CreateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createDateTime");
            entity.Property(e => e.DepartmentCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("departmentCode");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("displayName");
        });

        modelBuilder.Entity<JobPost>(entity =>
        {
            entity.HasKey(e => e.JobPostId).HasName("PK__JobPost__E4C3B7C78E65D832");

            entity.ToTable("JobPost");

            entity.Property(e => e.JobPostId).HasColumnName("jobPostId");
            entity.Property(e => e.CreateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createDateTime");
            entity.Property(e => e.InformationDetail).HasColumnName("informationDetail");
            entity.Property(e => e.JobPostTypeId).HasColumnName("jobPostTypeId");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.JobPostType).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.JobPostTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobPost__jobPost__47DBAE45");

            entity.HasOne(d => d.User).WithMany(p => p.JobPosts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__JobPost__userId__46E78A0C");
        });

        modelBuilder.Entity<JobPostType>(entity =>
        {
            entity.HasKey(e => e.JobPostTypeId).HasName("PK__JobPostT__71FA7F56221EFAC4");

            entity.ToTable("JobPostType");

            entity.HasIndex(e => e.TypeCode, "UQ__JobPostT__E9DAA9C7ECC33F6F").IsUnique();

            entity.Property(e => e.JobPostTypeId).HasColumnName("jobPostTypeId");
            entity.Property(e => e.CreateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createDateTime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("displayName");
            entity.Property(e => e.TypeCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("typeCode");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98462A8123A52B");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.CreateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createDateTime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("displayName");
            entity.Property(e => e.RoleCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleCode");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFFD767534D");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createdDateTime");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("emailAddress");
            entity.Property(e => e.EntranceYear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("entranceYear");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.GraduateYear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("graduateYear");
            entity.Property(e => e.InformationDetail).HasColumnName("informationDetail");
            entity.Property(e => e.IsVerified).HasColumnName("isVerified");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.LastSignedInDateTime)
                .HasColumnType("datetime")
                .HasColumnName("lastSignedInDateTime");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.UserPrivacySettingId).HasColumnName("userPrivacySettingId");

            entity.HasOne(d => d.UserPrivacySetting).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserPrivacySettingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__userPrivac__3B75D760");
        });

        modelBuilder.Entity<UserPrivacySetting>(entity =>
        {
            entity.HasKey(e => e.UserPrivacySettingId).HasName("PK__UserPriv__E59D4DE83AECC38D");

            entity.ToTable("UserPrivacySetting");

            entity.Property(e => e.UserPrivacySettingId).HasColumnName("userPrivacySettingId");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createdDateTime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("displayName");
            entity.Property(e => e.SettingCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("settingCode");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasKey(e => e.UserRegistrationId).HasName("PK__UserRegi__2A3E89D8907803A4");

            entity.ToTable("UserRegistration");

            entity.Property(e => e.UserRegistrationId).HasColumnName("userRegistrationId");
            entity.Property(e => e.CreateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createDateTime");
            entity.Property(e => e.RegistrationCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("registrationCode");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.UserRegistrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRegis__userI__3F466844");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__CD3149CC90EB2516");

            entity.ToTable("UserRole");

            entity.Property(e => e.UserRoleId).HasColumnName("userRoleId");
            entity.Property(e => e.CreateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createDateTime");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__roleId__534D60F1");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRole__userId__52593CB8");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

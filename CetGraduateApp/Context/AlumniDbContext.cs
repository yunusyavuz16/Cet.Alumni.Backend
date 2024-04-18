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

    public virtual DbSet<Alumni> Alumni { get; set; }

    public virtual DbSet<AlumniPrivacySetting> AlumniPrivacySettings { get; set; }

    public virtual DbSet<AlumniRegistration> AlumniRegistrations { get; set; }

    public virtual DbSet<AlumniRole> AlumniRoles { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<JobPostType> JobPostTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Term> Terms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=awseb-e-qmfumqmr8e-stack-awsebrdsdatabase-nqhgplsmjxpx.chbgdpvcdxf8.eu-north-1.rds.amazonaws.com,1433;Integrated Security=false;User ID=db_alumni_user;Password=alumniStrongPassword@1234!;Database=AlumniRdsDb;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumni>(entity =>
        {
            entity.HasKey(e => e.AlumniStudentNo).HasName("PK__Alumni__7E0B5690EBC12108");

            entity.Property(e => e.AlumniStudentNo)
                .ValueGeneratedNever()
                .HasColumnName("alumniStudentNo");
            entity.Property(e => e.AlumniPrivacySettingId).HasColumnName("alumniPrivacySettingId");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birthDate");
            entity.Property(e => e.Company)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("company");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createdDateTime");
            entity.Property(e => e.DepartmentId).HasColumnName("departmentId");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emailAddress");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.IsVerified).HasColumnName("isVerified");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("jobTitle");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.LastSignedInDateTime)
                .HasColumnType("datetime")
                .HasColumnName("lastSignedInDateTime");
            entity.Property(e => e.LinkedInUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("linkedInUrl");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfileDescription)
                .HasColumnType("text")
                .HasColumnName("profileDescription");
            entity.Property(e => e.Sector)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sector");
            entity.Property(e => e.TermId).HasColumnName("termId");

            entity.HasOne(d => d.AlumniPrivacySetting).WithMany(p => p.Alumni)
                .HasForeignKey(d => d.AlumniPrivacySettingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("alumniPrivacySettingId_constraint");

            entity.HasOne(d => d.Term).WithMany(p => p.Alumni)
                .HasForeignKey(d => d.TermId)
                .HasConstraintName("FK_term");
        });

        modelBuilder.Entity<AlumniPrivacySetting>(entity =>
        {
            entity.HasKey(e => e.AlumniPrivacySettingId).HasName("PK__AlumniPr__15D64E291688773D");

            entity.ToTable("AlumniPrivacySetting");

            entity.Property(e => e.AlumniPrivacySettingId).HasColumnName("alumniPrivacySettingId");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("displayName");
            entity.Property(e => e.SettingCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("settingCode");
        });

        modelBuilder.Entity<AlumniRegistration>(entity =>
        {
            entity.HasKey(e => e.UserRegistrationId).HasName("PK__AlumniRe__2A3E89D8F5BC3292");

            entity.ToTable("AlumniRegistration");

            entity.Property(e => e.UserRegistrationId).HasColumnName("userRegistrationId");
            entity.Property(e => e.AlumniStudentNo).HasColumnName("alumniStudentNo");
            entity.Property(e => e.CreateDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createDateTime");
            entity.Property(e => e.RegistrationCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("registrationCode");

            entity.HasOne(d => d.AlumniStudentNoNavigation).WithMany(p => p.AlumniRegistrations)
                .HasForeignKey(d => d.AlumniStudentNo)
                .HasConstraintName("FK__AlumniReg__alumn__797309D9");
        });

        modelBuilder.Entity<AlumniRole>(entity =>
        {
            entity.HasKey(e => e.AlumniRoleId).HasName("PK__AlumniRo__E767B6D2E28D1B0C");

            entity.ToTable("AlumniRole");

            entity.Property(e => e.AlumniRoleId).HasColumnName("alumniRoleId");
            entity.Property(e => e.AlumniStudentNo).HasColumnName("alumniStudentNo");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.AlumniStudentNoNavigation).WithMany(p => p.AlumniRoles)
                .HasForeignKey(d => d.AlumniStudentNo)
                .HasConstraintName("FK__AlumniRol__alumn__75A278F5");

            entity.HasOne(d => d.Role).WithMany(p => p.AlumniRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__AlumniRol__roleI__76969D2E");
        });

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
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98462A80AC707D");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("roleId");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("displayName");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasKey(e => e.TermId).HasName("PK__Term__90C2BD1EA4BD5776");

            entity.ToTable("Term");

            entity.HasIndex(e => e.TermYear, "UC_ColumnName").IsUnique();

            entity.Property(e => e.TermId).HasColumnName("termId");
            entity.Property(e => e.TermYear).HasColumnName("termYear");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

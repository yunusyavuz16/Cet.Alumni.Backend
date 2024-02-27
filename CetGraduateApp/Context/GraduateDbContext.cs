using System;
using System.Collections.Generic;
using CetGraduateApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace CetGraduateApp.Context;

public partial class GraduateDbContext : DbContext
{
    public GraduateDbContext()
    {
    }

    public GraduateDbContext(DbContextOptions<GraduateDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRegistration> UserRegistrations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=awseb-e-qmfumqmr8e-stack-awsebrdsdatabase-nqhgplsmjxpx.chbgdpvcdxf8.eu-north-1.rds.amazonaws.com,1433;Integrated Security=false;User ID=yunusyavuzadmin;Password=2c(cc]Yi%lsX;Database=GraduateRdsDb;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFF083D3C93");

            entity.ToTable("User");

            entity.HasIndex(e => e.EmailAddress, "UQ__User__347C30270B8E9AE0").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createdDateTime");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("emailAddress");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.IsVerified).HasColumnName("isVerified");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.LastSignedInDateTime)
                .HasColumnType("datetime")
                .HasColumnName("lastSignedInDateTime");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasKey(e => e.UserRegistrationId).HasName("PK__UserRegi__2A3E89D86AB92182");

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
                .HasConstraintName("FK__UserRegis__userI__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

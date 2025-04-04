using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudentManagmentSystem.WebApp.Models;

public partial class StudentManagmentContext : DbContext
{
    public StudentManagmentContext()
    {
    }

    public StudentManagmentContext(DbContextOptions<StudentManagmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

    public virtual DbSet<ApplicationUserProfile> ApplicationUserProfiles { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HDUBVFO\\SQLEXPRESS;Database=StudentManagment;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC07D81B7E25");

            entity.ToTable("ApplicationRole");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ApplicationUserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Applicat__3214EC0730357FC1");

            entity.ToTable("ApplicationUserProfile");

            entity.Property(e => e.Address1)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_Deleted");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Department).WithMany(p => p.ApplicationUserProfiles)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationUser_DepartmentId_Department_Id");

            entity.HasOne(d => d.Role).WithMany(p => p.ApplicationUserProfiles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationUserProfile_Role");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC076DFAC773");

            entity.ToTable("Department");

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_Deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserAcco__3214EC0747605F58");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.UserAccount)
                .HasForeignKey<UserAccount>(d => d.Id)
                .HasConstraintName("FK_ApplicationUserProfile_UserAccount");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

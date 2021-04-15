﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TimesheetBackend.Models
{
    public partial class tuntidbContext : DbContext
    {
        public tuntidbContext()
        {
        }

        public tuntidbContext(DbContextOptions<tuntidbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Timesheet> Timesheets { get; set; }
        public virtual DbSet<WorkAssignment> WorkAssignments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-J757MJTR;Database=tuntidb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Finnish_Swedish_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer);

                entity.Property(e => e.IdCustomer).HasColumnName("Id_Customer");

                entity.Property(e => e.ContactName).HasMaxLength(50);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.DeleteAt).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.LastModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee);

                entity.Property(e => e.IdEmployee).HasColumnName("Id_Employee");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.EmailAddress).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.IdContractor).HasColumnName("Id_Contractor");

                entity.Property(e => e.LastModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<Timesheet>(entity =>
            {
                entity.HasKey(e => e.IdTimesheet);

                entity.ToTable("Timesheet");

                entity.Property(e => e.IdTimesheet).HasColumnName("Id_Timesheet");

                entity.Property(e => e.Comments)
                    .HasMaxLength(1000)
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.IdCustomer).HasColumnName("Id_Customer");

                entity.Property(e => e.IdEmployee).HasColumnName("Id_Employee");

                entity.Property(e => e.IdWorkAssignment).HasColumnName("Id_WorkAssignment");

                entity.Property(e => e.LastModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.Latitude).HasMaxLength(50);

                entity.Property(e => e.Longitude).HasMaxLength(50);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.StopTime).HasColumnType("datetime");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK_Timesheet_Customers");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.IdEmployee)
                    .HasConstraintName("FK_Timesheet_Employee");

                entity.HasOne(d => d.IdWorkAssignmentNavigation)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.IdWorkAssignment)
                    .HasConstraintName("FK_Timesheet_WorkAssignment");
            });

            modelBuilder.Entity<WorkAssignment>(entity =>
            {
                entity.HasKey(e => e.IdWorkAssingment);

                entity.Property(e => e.IdWorkAssingment)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id_WorkAssingment");

                entity.Property(e => e.CompletedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeadLine).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.IdCustomer).HasColumnName("Id_Customer");

                entity.Property(e => e.LastModifiedAt).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.WorkStartedAt).HasColumnType("datetime");

                entity.HasOne(d => d.IdWorkAssingmentNavigation)
                    .WithOne(p => p.WorkAssignment)
                    .HasForeignKey<WorkAssignment>(d => d.IdWorkAssingment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkAssignments_Customers");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

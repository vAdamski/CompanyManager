﻿// <auto-generated />
using System;
using CompanyManager.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyManager.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241117105421_IgnoreEntityEventClass")]
    partial class IgnoreEntityEventClass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyManager.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Inactivated")
                        .HasColumnType("datetime2");

                    b.Property<string>("InactivatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime?>("Inactivated")
                        .HasColumnType("datetime2");

                    b.Property<string>("InactivatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.EmployeeSupervisor", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SupervisorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeeId", "SupervisorId");

                    b.HasIndex("SupervisorId");

                    b.ToTable("EmployeeSupervisors");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.LeaveApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("FreeFrom")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FreeTo")
                        .HasColumnType("date");

                    b.Property<DateTime?>("Inactivated")
                        .HasColumnType("datetime2");

                    b.Property<string>("InactivatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LeaveApplicationStatus")
                        .HasColumnType("int");

                    b.Property<int>("LeaveApplicationType")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("WorkDaysCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("LeaveApplications");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.LeaveApplicationComment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Inactivated")
                        .HasColumnType("datetime2");

                    b.Property<string>("InactivatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("LeaveApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeaveApplicationId");

                    b.ToTable("LeaveApplicationComments");
                });

            modelBuilder.Entity("CompanyManager.Domain.Primitives.DomainEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LeaveApplicationCommentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LeaveApplicationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("LeaveApplicationCommentId");

                    b.HasIndex("LeaveApplicationId");

                    b.ToTable("DomainEvent");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.Employee", b =>
                {
                    b.HasOne("CompanyManager.Domain.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.EmployeeSupervisor", b =>
                {
                    b.HasOne("CompanyManager.Domain.Entities.Employee", "Employee")
                        .WithMany("Supervisors")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CompanyManager.Domain.Entities.Employee", "Supervisor")
                        .WithMany("Subordinates")
                        .HasForeignKey("SupervisorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.LeaveApplication", b =>
                {
                    b.HasOne("CompanyManager.Domain.Entities.Company", "Company")
                        .WithMany("LeaveApplications")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.LeaveApplicationComment", b =>
                {
                    b.HasOne("CompanyManager.Domain.Entities.LeaveApplication", "LeaveApplication")
                        .WithMany("Comments")
                        .HasForeignKey("LeaveApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaveApplication");
                });

            modelBuilder.Entity("CompanyManager.Domain.Primitives.DomainEvent", b =>
                {
                    b.HasOne("CompanyManager.Domain.Entities.Company", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("CompanyId");

                    b.HasOne("CompanyManager.Domain.Entities.Employee", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("CompanyManager.Domain.Entities.LeaveApplicationComment", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("LeaveApplicationCommentId");

                    b.HasOne("CompanyManager.Domain.Entities.LeaveApplication", null)
                        .WithMany("DomainEvents")
                        .HasForeignKey("LeaveApplicationId");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.Company", b =>
                {
                    b.Navigation("DomainEvents");

                    b.Navigation("Employees");

                    b.Navigation("LeaveApplications");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.Employee", b =>
                {
                    b.Navigation("DomainEvents");

                    b.Navigation("Subordinates");

                    b.Navigation("Supervisors");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.LeaveApplication", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DomainEvents");
                });

            modelBuilder.Entity("CompanyManager.Domain.Entities.LeaveApplicationComment", b =>
                {
                    b.Navigation("DomainEvents");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using AssetManagement.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AssetManagement.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240702060453_removeUnnecessaryField")]
    partial class removeUnnecessaryField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AssetManagement.Domain.Entites.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssetCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AssetLocation")
                        .HasColumnType("int");

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("InstalledDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Specification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssetId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("AssignedIdBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignedIdTo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("Location")
                        .HasColumnType("int");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ReturnRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("AssignedIdBy");

                    b.HasIndex("AssignedIdTo");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryName")
                        .IsUnique();

                    b.HasIndex("Prefix")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6a70be78-84f7-4a6f-ab9b-320be20abe90"),
                            CategoryName = "Laptop",
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Prefix = "LA"
                        },
                        new
                        {
                            Id = new Guid("78a990fa-3e7f-4a3e-836f-6b6b79694955"),
                            CategoryName = "Monitor",
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Prefix = "MO"
                        },
                        new
                        {
                            Id = new Guid("fc9f0850-242a-4043-acd0-6eade6a067cb"),
                            CategoryName = "Desk",
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Prefix = "DE"
                        });
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.ReturnRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AcceptedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssignmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<Guid>("RequestedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ReturnState")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReturnedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AcceptedBy");

                    b.HasIndex("AssignmentId")
                        .IsUnique();

                    b.HasIndex("RequestedBy");

                    b.ToTable("ReturnRequests");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFirstTimeLogin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoinedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModifiedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("CONCAT('SD', RIGHT('0000' + CAST(StaffCodeId AS VARCHAR(4)), 4))");

                    b.Property<int>("StaffCodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffCodeId"));

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a4065ce5-58da-44b5-b3d7-47a89aa02474"),
                            CreatedBy = "System",
                            CreatedOn = new DateTimeOffset(new DateTime(2024, 7, 2, 13, 4, 51, 137, DateTimeKind.Unspecified).AddTicks(1250), new TimeSpan(0, 7, 0, 0, 0)),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Admin",
                            Gender = 0,
                            IsDeleted = false,
                            IsFirstTimeLogin = false,
                            JoinedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Ha Noi",
                            Location = 1,
                            PasswordHash = "AQAAAAIAAYagAAAAEG3+fwgMYO7TCEIEB5PPfyCDCkSHmrQRP/D4SELA8WH60u38NIAyISL4Sp30sARq7A==",
                            Role = 1,
                            StaffCode = "",
                            StaffCodeId = 0,
                            Username = "adminHN"
                        },
                        new
                        {
                            Id = new Guid("cbc111f4-fccb-497b-a072-bf2bee21121f"),
                            CreatedBy = "System",
                            CreatedOn = new DateTimeOffset(new DateTime(2024, 7, 2, 13, 4, 51, 390, DateTimeKind.Unspecified).AddTicks(3665), new TimeSpan(0, 7, 0, 0, 0)),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Admin",
                            Gender = 0,
                            IsDeleted = false,
                            IsFirstTimeLogin = false,
                            JoinedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Ho Chi Minh",
                            Location = 3,
                            PasswordHash = "AQAAAAIAAYagAAAAEOLY5SyFu3CK+2o0M1byQgUbPS1r8GQG7YzsGoBm5wxvuHDPh+OIBosYphJNUgaQGw==",
                            Role = 1,
                            StaffCode = "",
                            StaffCodeId = 0,
                            Username = "adminHCM"
                        },
                        new
                        {
                            Id = new Guid("87996093-7586-45b5-bd44-67ed9dde208e"),
                            CreatedBy = "System",
                            CreatedOn = new DateTimeOffset(new DateTime(2024, 7, 2, 13, 4, 51, 647, DateTimeKind.Unspecified).AddTicks(3252), new TimeSpan(0, 7, 0, 0, 0)),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Admin",
                            Gender = 0,
                            IsDeleted = false,
                            IsFirstTimeLogin = false,
                            JoinedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Da Nang",
                            Location = 2,
                            PasswordHash = "AQAAAAIAAYagAAAAEOagerVzBj2tWN1TmR7iFktbRSVj1A2NvOnMX+9axCWyfHdWr4uAGcBsCYpx5/W0Ug==",
                            Role = 1,
                            StaffCode = "",
                            StaffCodeId = 0,
                            Username = "adminDN"
                        });
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.Asset", b =>
                {
                    b.HasOne("AssetManagement.Domain.Entites.Category", "Category")
                        .WithMany("Assets")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.Assignment", b =>
                {
                    b.HasOne("AssetManagement.Domain.Entites.Asset", "Asset")
                        .WithMany("Assignments")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AssetManagement.Domain.Entites.User", "AssignedBy")
                        .WithMany("AssignmentsCreated")
                        .HasForeignKey("AssignedIdBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AssetManagement.Domain.Entites.User", "AssignedTo")
                        .WithMany("AssignmentsReceived")
                        .HasForeignKey("AssignedIdTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("AssignedBy");

                    b.Navigation("AssignedTo");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.ReturnRequest", b =>
                {
                    b.HasOne("AssetManagement.Domain.Entites.User", "AcceptedUser")
                        .WithMany("ReturnRequestsAccepted")
                        .HasForeignKey("AcceptedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("AssetManagement.Domain.Entites.Assignment", "Assignment")
                        .WithOne("ReturnRequest")
                        .HasForeignKey("AssetManagement.Domain.Entites.ReturnRequest", "AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AssetManagement.Domain.Entites.User", "RequestedUser")
                        .WithMany("ReturnRequestsRequested")
                        .HasForeignKey("RequestedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AcceptedUser");

                    b.Navigation("Assignment");

                    b.Navigation("RequestedUser");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.Asset", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.Assignment", b =>
                {
                    b.Navigation("ReturnRequest");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.Category", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("AssetManagement.Domain.Entites.User", b =>
                {
                    b.Navigation("AssignmentsCreated");

                    b.Navigation("AssignmentsReceived");

                    b.Navigation("ReturnRequestsAccepted");

                    b.Navigation("ReturnRequestsRequested");
                });
#pragma warning restore 612, 618
        }
    }
}

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
    [Migration("20240620054713_remove disable in base entity")]
    partial class removedisableinbaseentity
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

            modelBuilder.Entity("AssetManagement.Domain.Entites.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c5e91723-f161-4fa2-8ef2-c44de4c79845"),
                            CategoryName = "Laptop",
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Prefix = "LA"
                        },
                        new
                        {
                            Id = new Guid("12e152f1-0466-44ad-9105-e6cc907ac3bc"),
                            CategoryName = "Monitor",
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Prefix = "MO"
                        },
                        new
                        {
                            Id = new Guid("c7f20851-1605-407d-b765-3afa0106d6e1"),
                            CategoryName = "Desk",
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Prefix = "DE"
                        });
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
                            Id = new Guid("1f74aee7-ee35-4596-a402-7dbfc301c114"),
                            CreatedBy = "System",
                            CreatedOn = new DateTimeOffset(new DateTime(2024, 6, 20, 12, 47, 11, 970, DateTimeKind.Unspecified).AddTicks(399), new TimeSpan(0, 7, 0, 0, 0)),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Admin",
                            Gender = 0,
                            IsDeleted = false,
                            IsFirstTimeLogin = false,
                            JoinedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Ha Noi",
                            Location = 1,
                            PasswordHash = "AQAAAAIAAYagAAAAELfggdDzt5afmtL+aX5TyztYlQMSekJC85tHc2kn3XSSQwNUMOYuy97HXOpm69aEQQ==",
                            Role = 1,
                            StaffCode = "",
                            StaffCodeId = 0,
                            Username = "adminHN"
                        },
                        new
                        {
                            Id = new Guid("8e42a68f-abce-4fa7-8d63-8aa880a35f96"),
                            CreatedBy = "System",
                            CreatedOn = new DateTimeOffset(new DateTime(2024, 6, 20, 12, 47, 12, 58, DateTimeKind.Unspecified).AddTicks(7248), new TimeSpan(0, 7, 0, 0, 0)),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Admin",
                            Gender = 0,
                            IsDeleted = false,
                            IsFirstTimeLogin = false,
                            JoinedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Ho Chi Minh",
                            Location = 3,
                            PasswordHash = "AQAAAAIAAYagAAAAENSx9wIGsXujDyDDvGK9WCF6CkEp2SVzh1IXEQJ6O1E9ubqqHfN34U1ZDnEXqK3/MA==",
                            Role = 1,
                            StaffCode = "",
                            StaffCodeId = 0,
                            Username = "adminHCM"
                        },
                        new
                        {
                            Id = new Guid("6e802fb3-1730-4035-828d-be57b5ebc5d8"),
                            CreatedOn = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FirstName = "Admin",
                            Gender = 0,
                            IsDeleted = false,
                            IsFirstTimeLogin = false,
                            JoinedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Da Nang",
                            Location = 2,
                            PasswordHash = "",
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

            modelBuilder.Entity("AssetManagement.Domain.Entites.Category", b =>
                {
                    b.Navigation("Assets");
                });
#pragma warning restore 612, 618
        }
    }
}

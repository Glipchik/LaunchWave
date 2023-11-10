﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectService.DAL.Contexts;

#nullable disable

namespace ProjectService.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectService.DAL.Entities.ChangeLogEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangeAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EntityName")
                        .HasColumnType("text");

                    b.Property<string>("NewValue")
                        .HasColumnType("text");

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<string>("OldValue")
                        .HasColumnType("text");

                    b.Property<string>("PrimaryKeyValue")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("PropertyName")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChangeLogs");
                });

            modelBuilder.Entity("ProjectService.DAL.Entities.CrowdFundRequestEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("CrowdFundingAmount")
                        .HasColumnType("numeric");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("RequestDate")
                        .HasColumnType("date");

                    b.Property<string>("RequestedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("CrowdFundRequests");
                });

            modelBuilder.Entity("ProjectService.DAL.Entities.DbAuditEntry", b =>
                {
                    b.Property<int>("AuditEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AuditEntryId"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("EntitySetName")
                        .HasColumnType("text");

                    b.Property<string>("EntityTypeName")
                        .HasColumnType("text");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<string>("StateName")
                        .HasColumnType("text");

                    b.HasKey("AuditEntryId");

                    b.ToTable("AuditEntries");
                });

            modelBuilder.Entity("ProjectService.DAL.Entities.DbAuditEntryProperty", b =>
                {
                    b.Property<int>("AuditEntryPropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AuditEntryPropertyId"));

                    b.Property<int?>("DbAuditEntryAuditEntryId")
                        .HasColumnType("integer");

                    b.Property<string>("NewValue")
                        .HasColumnType("text");

                    b.Property<string>("OldValue")
                        .HasColumnType("text");

                    b.Property<string>("PropertyName")
                        .HasColumnType("text");

                    b.Property<string>("RelationName")
                        .HasColumnType("text");

                    b.HasKey("AuditEntryPropertyId");

                    b.HasIndex("DbAuditEntryAuditEntryId");

                    b.ToTable("AuditEntryProperties");
                });

            modelBuilder.Entity("ProjectService.DAL.Entities.ProjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("CollectedAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("CrowdFundRequestId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("CrowdFundingAmount")
                        .HasColumnType("numeric");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("RequestedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CrowdFundRequestId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectService.DAL.Entities.DbAuditEntryProperty", b =>
                {
                    b.HasOne("ProjectService.DAL.Entities.DbAuditEntry", null)
                        .WithMany("Properties")
                        .HasForeignKey("DbAuditEntryAuditEntryId");
                });

            modelBuilder.Entity("ProjectService.DAL.Entities.ProjectEntity", b =>
                {
                    b.HasOne("ProjectService.DAL.Entities.CrowdFundRequestEntity", "CrowdFundRequest")
                        .WithMany()
                        .HasForeignKey("CrowdFundRequestId");

                    b.Navigation("CrowdFundRequest");
                });

            modelBuilder.Entity("ProjectService.DAL.Entities.DbAuditEntry", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}

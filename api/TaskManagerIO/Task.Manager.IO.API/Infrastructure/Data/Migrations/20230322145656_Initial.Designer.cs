﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManagerIO.API.Infrastructure.Data;

#nullable disable

namespace TaskManagerIO.API.Infrastructure.Data.Migrations
{
    [DbContext(typeof(TaskManagerContext))]
    [Migration("20230322145656_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskManagerIO.API.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TaskManagerIO.API.Entities.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ApprovedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ApprovedById")
                        .HasColumnType("integer");

                    b.Property<int?>("AssignedToId")
                        .HasColumnType("integer");

                    b.Property<int?>("ClosedById")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer");

                    b.Property<int>("DepartmnetId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("ClosedById");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DepartmnetId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("TaskManagerIO.API.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenEndsAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Salt")
                        .HasColumnType("text");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskManagerIO.API.Entities.Job", b =>
                {
                    b.HasOne("TaskManagerIO.API.Entities.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("TaskManagerIO.API.Entities.User", "AssignedTo")
                        .WithMany("Jobs")
                        .HasForeignKey("AssignedToId");

                    b.HasOne("TaskManagerIO.API.Entities.User", "ClosedBy")
                        .WithMany()
                        .HasForeignKey("ClosedById");

                    b.HasOne("TaskManagerIO.API.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManagerIO.API.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmnetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("AssignedTo");

                    b.Navigation("ClosedBy");

                    b.Navigation("CreatedBy");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("TaskManagerIO.API.Entities.User", b =>
                {
                    b.HasOne("TaskManagerIO.API.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("TaskManagerIO.API.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TaskManagerIO.API.Entities.User", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
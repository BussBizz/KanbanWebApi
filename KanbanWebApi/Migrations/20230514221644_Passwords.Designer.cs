﻿// <auto-generated />
using System;
using KanbanWebApi.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KanbanWebApi.Migrations
{
    [DbContext(typeof(KanbanDBContext))]
    [Migration("20230514221644_Passwords")]
    partial class Passwords
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KanbanWebApi.Models.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeadLine")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("KanbanTaskId")
                        .HasColumnType("int");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KanbanTaskId");

                    b.HasIndex("MemberId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("KanbanWebApi.Models.KanbanTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AssingedId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CatergoryId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AssingedId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.ToTable("KanbanTasks");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.Property<bool>("CanAssign")
                        .HasColumnType("bit");

                    b.Property<bool>("CanComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("CanCreate")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("UserId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Password", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("KanbanWebApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAnon")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PasswordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Board", b =>
                {
                    b.HasOne("KanbanWebApi.Models.User", "Owner")
                        .WithMany("Boards")
                        .HasForeignKey("OwnerId");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Category", b =>
                {
                    b.HasOne("KanbanWebApi.Models.Board", "Board")
                        .WithMany("Categories")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KanbanWebApi.Models.Member", "Creator")
                        .WithMany("Categories")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Board");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Comment", b =>
                {
                    b.HasOne("KanbanWebApi.Models.KanbanTask", "KanbanTask")
                        .WithMany("Comments")
                        .HasForeignKey("KanbanTaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KanbanWebApi.Models.Member", "Member")
                        .WithMany("Comments")
                        .HasForeignKey("MemberId");

                    b.Navigation("KanbanTask");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("KanbanWebApi.Models.KanbanTask", b =>
                {
                    b.HasOne("KanbanWebApi.Models.Member", "Assigned")
                        .WithMany("TasksAssigned")
                        .HasForeignKey("AssingedId");

                    b.HasOne("KanbanWebApi.Models.Category", "Category")
                        .WithMany("KanbanTasks")
                        .HasForeignKey("CategoryId");

                    b.HasOne("KanbanWebApi.Models.Member", "Creator")
                        .WithMany("TasksCreated")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Assigned");

                    b.Navigation("Category");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Member", b =>
                {
                    b.HasOne("KanbanWebApi.Models.Board", "Board")
                        .WithMany("Members")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KanbanWebApi.Models.User", "User")
                        .WithMany("Memberships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Password", b =>
                {
                    b.HasOne("KanbanWebApi.Models.User", "User")
                        .WithOne("Password")
                        .HasForeignKey("KanbanWebApi.Models.Password", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Board", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Category", b =>
                {
                    b.Navigation("KanbanTasks");
                });

            modelBuilder.Entity("KanbanWebApi.Models.KanbanTask", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("KanbanWebApi.Models.Member", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Comments");

                    b.Navigation("TasksAssigned");

                    b.Navigation("TasksCreated");
                });

            modelBuilder.Entity("KanbanWebApi.Models.User", b =>
                {
                    b.Navigation("Boards");

                    b.Navigation("Memberships");

                    b.Navigation("Password")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

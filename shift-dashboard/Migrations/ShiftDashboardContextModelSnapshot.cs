﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shift_dashboard.Data;

namespace shift_dashboard.Migrations
{
    [DbContext(typeof(ShiftDashboardContext))]
    partial class ShiftDashboardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("DelegateDBVoterDB", b =>
                {
                    b.Property<int>("DelegatesVoteId")
                        .HasColumnType("int");

                    b.Property<int>("VotersId")
                        .HasColumnType("int");

                    b.HasKey("DelegatesVoteId", "VotersId");

                    b.HasIndex("VotersId");

                    b.ToTable("DelegateDBVoterDB");
                });

            modelBuilder.Entity("shift_dashboard.Model.DelegateDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<double>("Approval")
                        .HasColumnType("float");

                    b.Property<int>("Missedblocks")
                        .HasColumnType("int");

                    b.Property<int>("Producedblocks")
                        .HasColumnType("int");

                    b.Property<int>("Productivity")
                        .HasColumnType("int");

                    b.Property<string>("PublicKey")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("Rate")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Vote")
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Address" }, "Index_Address")
                        .IsUnique();

                    b.ToTable("DelegatesDB");
                });

            modelBuilder.Entity("shift_dashboard.Model.VoterDB", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Balance")
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("PublicKey")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Address" }, "Index_Address")
                        .IsUnique()
                        .HasDatabaseName("Index_Address1");

                    b.ToTable("VotersDB");
                });

            modelBuilder.Entity("DelegateDBVoterDB", b =>
                {
                    b.HasOne("shift_dashboard.Model.DelegateDB", null)
                        .WithMany()
                        .HasForeignKey("DelegatesVoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shift_dashboard.Model.VoterDB", null)
                        .WithMany()
                        .HasForeignKey("VotersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
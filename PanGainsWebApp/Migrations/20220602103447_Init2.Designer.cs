﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PanGainsWebApp.Data;

#nullable disable

namespace PanGainsWebApp.Migrations
{
    [DbContext(typeof(PanGainsWebAppContext))]
    [Migration("20220602103447_Init2")]
    partial class Init2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PanGainsWebApp.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AverageChallengePos")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Notifications")
                        .HasColumnType("tinyint(1)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<bool>("Private")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("AccountID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.ChallengeStats", b =>
                {
                    b.Property<int>("ChallengeStatsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("ChallengeTotalReps")
                        .HasColumnType("int");

                    b.Property<int>("LeaderboardID")
                        .HasColumnType("int");

                    b.HasKey("ChallengeStatsID");

                    b.ToTable("ChallengeStats");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.CompletedWorkout", b =>
                {
                    b.Property<int>("CompletedWorkoutID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCompleted")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("RoutineID")
                        .HasColumnType("int");

                    b.Property<double>("TotalWeightLifted")
                        .HasColumnType("double");

                    b.HasKey("CompletedWorkoutID");

                    b.ToTable("CompletedWorkout");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.DaysWorkedOut", b =>
                {
                    b.Property<int>("DaysWorkedOutID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.HasKey("DaysWorkedOutID");

                    b.ToTable("DaysWorkedOut");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.Exercise", b =>
                {
                    b.Property<int>("ExerciseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ExerciseName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ExerciseID");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.Folder", b =>
                {
                    b.Property<int>("FolderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("FolderLikes")
                        .HasColumnType("int");

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("FolderID");

                    b.ToTable("Folder");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.Leaderboard", b =>
                {
                    b.Property<int>("LeaderboardID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LeaderboardDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TotalParticipants")
                        .HasColumnType("int");

                    b.HasKey("LeaderboardID");

                    b.ToTable("Leaderboard");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.Routine", b =>
                {
                    b.Property<int>("RoutineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FolderID")
                        .HasColumnType("int");

                    b.Property<string>("RoutineName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("RoutineID");

                    b.ToTable("Routine");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.Set", b =>
                {
                    b.Property<int>("SetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Kg")
                        .HasColumnType("int");

                    b.Property<string>("Previous")
                        .HasColumnType("longtext");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("SetRow")
                        .HasColumnType("int");

                    b.Property<string>("SetType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("YourExerciseID")
                        .HasColumnType("int");

                    b.HasKey("SetID");

                    b.ToTable("Set");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.Social", b =>
                {
                    b.Property<int>("SocialID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("FollowingID")
                        .HasColumnType("int");

                    b.HasKey("SocialID");

                    b.ToTable("Social");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.Statistics", b =>
                {
                    b.Property<int>("StatisticsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<int>("AvgReps")
                        .HasColumnType("int");

                    b.Property<int>("AvgSets")
                        .HasColumnType("int");

                    b.Property<int>("AvgWorkoutTime")
                        .HasColumnType("int");

                    b.Property<double>("TotalLifted")
                        .HasColumnType("double");

                    b.Property<int>("TotalWorkouts")
                        .HasColumnType("int");

                    b.HasKey("StatisticsID");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("PanGainsWebApp.Models.YourExercise", b =>
                {
                    b.Property<int>("YourExerciseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ExerciseID")
                        .HasColumnType("int");

                    b.Property<int>("RoutineID")
                        .HasColumnType("int");

                    b.HasKey("YourExerciseID");

                    b.ToTable("YourExercise");
                });
#pragma warning restore 612, 618
        }
    }
}

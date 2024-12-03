﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TFTStats.Matches.Persistence;

#nullable disable

namespace TFTStats.Matches.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241130130433_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TFTStats.Matches.Entities.Match", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("GameType")
                        .HasColumnType("text");

                    b.Property<string>("GameVersion")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("TFTStats.Matches.Entities.Match", b =>
                {
                    b.OwnsMany("TFTStats.Matches.Entities.Participant", "Participants", b1 =>
                        {
                            b1.Property<string>("Puuid")
                                .HasColumnType("text");

                            b1.Property<string>("MatchId")
                                .HasColumnType("text");

                            b1.Property<int?>("Level")
                                .HasColumnType("integer");

                            b1.Property<int?>("Placement")
                                .HasColumnType("integer");

                            b1.Property<bool?>("Win")
                                .HasColumnType("boolean");

                            b1.HasKey("Puuid", "MatchId");

                            b1.HasIndex("MatchId");

                            b1.ToTable("Participant");

                            b1.WithOwner()
                                .HasForeignKey("MatchId");

                            b1.OwnsMany("TFTStats.Matches.Entities.Trait", "Traits", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.Property<int?>("CurrentTier")
                                        .HasColumnType("integer");

                                    b2.Property<string>("MatchId")
                                        .HasColumnType("text");

                                    b2.Property<string>("Name")
                                        .HasColumnType("text");

                                    b2.Property<int?>("NumberOfUnits")
                                        .HasColumnType("integer");

                                    b2.Property<string>("ParticipantId")
                                        .HasColumnType("text");

                                    b2.Property<int?>("TotalTier")
                                        .HasColumnType("integer");

                                    b2.HasKey("Id");

                                    b2.HasIndex("ParticipantId", "MatchId");

                                    b2.ToTable("Trait");

                                    b2.WithOwner()
                                        .HasForeignKey("ParticipantId", "MatchId");
                                });

                            b1.OwnsMany("TFTStats.Matches.Entities.Unit", "Units", b2 =>
                                {
                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.Property<string[]>("ItemNames")
                                        .IsRequired()
                                        .HasColumnType("text[]");

                                    b2.Property<string>("MatchId")
                                        .HasColumnType("text");

                                    b2.Property<string>("Name")
                                        .HasColumnType("text");

                                    b2.Property<string>("ParticipantId")
                                        .HasColumnType("text");

                                    b2.Property<int?>("Tier")
                                        .HasColumnType("integer");

                                    b2.HasKey("Id");

                                    b2.HasIndex("ParticipantId", "MatchId");

                                    b2.ToTable("Unit");

                                    b2.WithOwner()
                                        .HasForeignKey("ParticipantId", "MatchId");
                                });

                            b1.Navigation("Traits");

                            b1.Navigation("Units");
                        });

                    b.Navigation("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}
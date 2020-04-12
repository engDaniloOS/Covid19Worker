﻿// <auto-generated />
using System;
using Covid19Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Covid19Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Covid19Data.Domain.Entities.DayData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Deceased")
                        .HasColumnType("int");

                    b.Property<int>("Infected")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedAtSource")
                        .HasColumnType("datetime2");

                    b.Property<string>("SourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalNotInfected")
                        .HasColumnType("int");

                    b.Property<int>("TotalTested")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DayDatas");
                });

            modelBuilder.Entity("Covid19Data.Domain.Entities.DestinationEmail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DestinationEmails");
                });

            modelBuilder.Entity("Covid19Data.Domain.Entities.StateInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<long?>("DayDataId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DayDataId1")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DayDataId");

                    b.HasIndex("DayDataId1");

                    b.ToTable("StateInfos");
                });

            modelBuilder.Entity("Covid19Data.Domain.Entities.StateInfo", b =>
                {
                    b.HasOne("Covid19Data.Domain.Entities.DayData", null)
                        .WithMany("DeceasedByRegion")
                        .HasForeignKey("DayDataId");

                    b.HasOne("Covid19Data.Domain.Entities.DayData", null)
                        .WithMany("InfectedByRegion")
                        .HasForeignKey("DayDataId1");
                });
#pragma warning restore 612, 618
        }
    }
}
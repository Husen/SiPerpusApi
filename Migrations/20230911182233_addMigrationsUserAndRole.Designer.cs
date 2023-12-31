﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SiPerpusApi.Repositories;

#nullable disable

namespace SiPerpusApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230911182233_addMigrationsUserAndRole")]
    partial class addMigrationsUserAndRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SiPerpusApi.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("CATEGORY_ID");

                    b.Property<string>("CodeBook")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("CODE_BOOK");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("ISBN")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("ISBN");

                    b.Property<string>("NameBook")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("NAME_BOOK");

                    b.Property<int>("PageBook")
                        .HasColumnType("integer")
                        .HasColumnName("PAGE_BOOK");

                    b.Property<string>("Pengarang")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("PENGARANG");

                    b.Property<int?>("PublisherId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("PUBLISHER_ID");

                    b.Property<int?>("RackId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("RACK_ID");

                    b.Property<int>("Stock")
                        .HasColumnType("integer")
                        .HasColumnName("STOCK");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.Property<string>("YearBook")
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)")
                        .HasColumnName("YEAR_BOOK");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PublisherId");

                    b.HasIndex("RackId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("SiPerpusApi.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("NameCategory")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("NAME_CATEGORY");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SiPerpusApi.Models.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Amercement")
                        .HasColumnType("integer")
                        .HasColumnName("AMERCEMENT");

                    b.Property<string>("CodeLoan")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("CODE_LOAN");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<int>("Duration")
                        .HasColumnType("integer")
                        .HasColumnName("DURATION");

                    b.Property<DateTime>("EndDateLoan")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("END_DATE_LOAN");

                    b.Property<int>("MemberId")
                        .HasColumnType("integer")
                        .HasColumnName("MEMBER_ID");

                    b.Property<DateTime>("StartDateLoan")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("START_DATE_LOAN");

                    b.Property<int>("StatusLoan")
                        .HasColumnType("integer")
                        .HasColumnName("STATUS_LOAN");

                    b.Property<int?>("TotalDailyFines")
                        .HasColumnType("integer")
                        .HasColumnName("TOTAL_DAILY_FINES");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("SiPerpusApi.Models.LoanDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("integer")
                        .HasColumnName("BOOK_ID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("LoanCode")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("LOAN_CODE");

                    b.Property<int?>("LoanId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("LOAN_ID");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.Property<int>("qty")
                        .HasColumnType("integer")
                        .HasColumnName("QTY");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("LoanId");

                    b.ToTable("LoanDetails");
                });

            modelBuilder.Entity("SiPerpusApi.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("FullName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("FULL_NAME");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("SiPerpusApi.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("NamePublisher")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("NAME_PUBLISHER");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("SiPerpusApi.Models.Rack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CodeRack")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("CODE_RACK");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.ToTable("Racks");
                });

            modelBuilder.Entity("SiPerpusApi.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleName")
                        .HasColumnType("integer")
                        .HasColumnName("RoleName");

                    b.HasKey("Id");

                    b.ToTable("ROLES");
                });

            modelBuilder.Entity("SiPerpusApi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CREATED_AT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PASSWORD");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("ROLE_ID");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UPDATED_AT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("M_USERS");
                });

            modelBuilder.Entity("SiPerpusApi.Models.Book", b =>
                {
                    b.HasOne("SiPerpusApi.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiPerpusApi.Models.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiPerpusApi.Models.Rack", "Rack")
                        .WithMany()
                        .HasForeignKey("RackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Publisher");

                    b.Navigation("Rack");
                });

            modelBuilder.Entity("SiPerpusApi.Models.Loan", b =>
                {
                    b.HasOne("SiPerpusApi.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("SiPerpusApi.Models.LoanDetail", b =>
                {
                    b.HasOne("SiPerpusApi.Models.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SiPerpusApi.Models.Loan", "Loan")
                        .WithMany()
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("SiPerpusApi.Models.User", b =>
                {
                    b.HasOne("SiPerpusApi.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}

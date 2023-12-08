﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NEO_SYSTEM_TECHNOLOGY.Data;

#nullable disable

namespace NEO_SYSTEM_TECHNOLOGY.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231208085730_AddContentToReceiptClass")]
    partial class AddContentToReceiptClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Dogovor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<decimal>("DogovorSum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsVatIncluded")
                        .HasColumnType("bit");

                    b.Property<string>("OrderHeader")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrganizationID")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("ID");

                    b.HasIndex("OrganizationID");

                    b.ToTable("Dogovors");
                });

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Organization", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationID")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("OrganizationID");

                    b.ToTable("People");
                });

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Receipt", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<int>("AmountFaceValue")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFaceValue")
                        .HasColumnType("datetime2");

                    b.Property<int>("DogovorId")
                        .HasColumnType("int");

                    b.Property<bool>("IsVatIncluded")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentSum")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DogovorId")
                        .IsUnique();

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Dogovor", b =>
                {
                    b.HasOne("NEO_SYSTEM_TECHNOLOGY.Entity.Organization", "Organization")
                        .WithMany("Dogovors")
                        .HasForeignKey("OrganizationID");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Person", b =>
                {
                    b.HasOne("NEO_SYSTEM_TECHNOLOGY.Entity.Organization", "Organization")
                        .WithMany("Person")
                        .HasForeignKey("OrganizationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Receipt", b =>
                {
                    b.HasOne("NEO_SYSTEM_TECHNOLOGY.Entity.Dogovor", "Dogovor")
                        .WithOne("Receipt")
                        .HasForeignKey("NEO_SYSTEM_TECHNOLOGY.Entity.Receipt", "DogovorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dogovor");
                });

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Dogovor", b =>
                {
                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("NEO_SYSTEM_TECHNOLOGY.Entity.Organization", b =>
                {
                    b.Navigation("Dogovors");

                    b.Navigation("Person");
                });
#pragma warning restore 612, 618
        }
    }
}
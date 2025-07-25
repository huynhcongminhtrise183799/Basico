﻿// <auto-generated />
using System;
using FormService.Infrastructure.Write;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FormService.Infrastructure.Write.Migrations
{
    [DbContext(typeof(FormDbContextWrite))]
    [Migration("20250619064906_UpdateTableForm")]
    partial class UpdateTableForm
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FormService.Domain.Entities.CustomerForm", b =>
                {
                    b.Property<Guid>("CustomerFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerFormData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("FormTemplateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("CustomerFormId");

                    b.HasIndex("FormTemplateId");

                    b.ToTable("CustomerForm", (string)null);
                });

            modelBuilder.Entity("FormService.Domain.Entities.FormTemplate", b =>
                {
                    b.Property<Guid>("FormTemplateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FormTemplateData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormTemplateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("FormTemplateId");

                    b.ToTable("FormTemplate", (string)null);
                });

            modelBuilder.Entity("FormService.Domain.Entities.CustomerForm", b =>
                {
                    b.HasOne("FormService.Domain.Entities.FormTemplate", "FormTemplate")
                        .WithMany()
                        .HasForeignKey("FormTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_CustomerForm_FormTemplate");

                    b.Navigation("FormTemplate");
                });
#pragma warning restore 612, 618
        }
    }
}

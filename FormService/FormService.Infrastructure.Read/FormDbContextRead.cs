using FormService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Infrastructure.Read
{
    public class FormDbContextRead : DbContext
    {
        public FormDbContextRead(DbContextOptions<FormDbContextRead> options) : base(options)
        {
        }

        public DbSet<FormTemplate> FormTemplates { get; set; }

        public DbSet<CustomerForm> CustomerForms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the FormTemplate entity
            modelBuilder.Entity<FormTemplate>(entity =>
            {
                entity.ToTable("FormTemplate");
                entity.HasKey(e => e.FormTemplateId);
                entity.Property(e => e.FormTemplateId).ValueGeneratedOnAdd();
                entity.Property(e => e.FormTemplateName).IsRequired();
                entity.Property(e => e.FormTemplateData).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(15);
            });
            // Configure the CustomerForm entity
            modelBuilder.Entity<CustomerForm>(entity =>
            {
                entity.ToTable("CustomerForm");
                entity.HasKey(e => e.CustomerFormId);
                entity.Property(e => e.CustomerFormId).ValueGeneratedOnAdd();
                entity.Property(e => e.CustomerFormData).IsRequired();
                entity.Property(e => e.Status).IsRequired().HasMaxLength(15);

                // Configure the relationship with FormTemplate
                entity.HasOne(e => e.FormTemplate)
                      .WithMany()
                      .HasForeignKey(e => e.FormTemplateId)
                      .HasConstraintName("FK_CustomerForm_FormTemplate");
            });
        }
    }
}

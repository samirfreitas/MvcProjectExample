using Example.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data.Mappings
{
    public class VendorMapping : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(v => v.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : 1

            builder.HasOne(v => v.Address)
                .WithOne(a => a.Vendor);

            // 1 : N 
            builder.HasMany(v => v.Products)
                .WithOne(p => p.Vendor)
                .HasForeignKey(p => p.VendorId);

            builder.ToTable("Vendors");

        }
    }
}

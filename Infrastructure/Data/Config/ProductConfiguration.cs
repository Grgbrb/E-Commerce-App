using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    //     Allows configuration for an entity type to be factored into a separate class,
    //     rather than in-line in Microsoft.EntityFrameworkCore.DbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder).
    //     Implement this interface, applying configuration for the entity in the Microsoft.EntityFrameworkCore.IEntityTypeConfiguration`1.Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder{`0})
    //     method, and then apply the configuration to the model using Microsoft.EntityFrameworkCore.ModelBuilder.ApplyConfiguration``1(Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{``0})
    //     in Microsoft.EntityFrameworkCore.DbContext.OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder)
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(t => t.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
            
        }
    }
}
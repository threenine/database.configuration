using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Threenine.Models;

namespace Threenine.Configurations;

public abstract class ValueListTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : ValueListEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(e => e.Id)
            .HasColumnName(nameof(BaseEntity.Id).ToLower())
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("newid()");

        builder.Property(x => x.Name)
            .HasColumnName(nameof(ValueListEntity.Name).ToLower())
            .IsRequired()
            .HasMaxLength(150);
        

        builder.Property(e => e.Created)
            .HasColumnType("datetime")
            .HasColumnName(nameof(BaseEntity.Created).ToLower())
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("getdate()");
            
        builder.Property(e => e.Modified)
            .HasColumnType("datetime")
            .HasColumnName(nameof(BaseEntity.Modified).ToLower())
            .ValueGeneratedOnUpdate()
            .HasDefaultValueSql("getdate()");
        
        builder.Property(e => e.Active)
            .HasColumnName(nameof(BaseEntity.Active).ToLower())
            .ValueGeneratedOnAdd()
            .HasColumnType("bit")
            .HasDefaultValueSql("((1))");
    }
}
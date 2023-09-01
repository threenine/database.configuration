using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Threenine.Models;

namespace Threenine.Configurations.PostgreSql;

public abstract class ValueListTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : ValueListEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(e => e.Id)
            .HasColumnName(nameof(ValueListEntity.Id).ToLower())
             .HasColumnType(ColumnTypes.Integer)
             .IsRequired()
            .ValueGeneratedOnAdd();
      
        builder.Property(x => x.Name)
            .HasColumnName(nameof(ValueListEntity.Name).ToLower())
            .HasColumnType(ColumnTypes.Varchar)
            .IsRequired()
            .HasMaxLength(75);
        
        builder.Property(x => x.Description)
            .HasColumnName(nameof(ValueListEntity.Description).ToLower())
            .HasColumnType(ColumnTypes.Varchar)
            .HasMaxLength(150);
        
        builder.HasIndex(x => x.Name).IsUnique();
        
        builder.Property(e => e.Active)
            .HasDefaultValue(true)
            .HasColumnName(nameof(ValueListEntity.Active).ToLower())
            .ValueGeneratedOnAdd();
       
    }
}
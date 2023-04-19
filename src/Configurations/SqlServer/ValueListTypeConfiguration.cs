using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Threenine.Models;

namespace Threenine.Configurations.SqlServer;

public abstract class ValueListTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : ValueListEntity
{
    public void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(e => e.Id)
            .HasColumnName(nameof(ValueListEntity.Id).ToLower())
            .HasColumnType(ColumnTypes.Integer)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnType(ColumnTypes.NVarchar)
            .HasColumnName(nameof(ValueListEntity.Name).ToLower())
            .IsRequired()
            .HasMaxLength(150);
        
        builder.HasIndex(x => x.Name).IsUnique();
        
        builder.Property(x => x.Description)
            .HasColumnType(ColumnTypes.NVarchar)
            .HasColumnName(nameof(ValueListEntity.Description).ToLower())
            .HasMaxLength(150);
        
        builder.Property(e => e.Active)
            .HasColumnType(ColumnTypes.Boolean)
            .HasDefaultValue(true)
            .HasColumnName(nameof(ValueListEntity.Active).ToLower())
            .ValueGeneratedOnAdd();
    }
}
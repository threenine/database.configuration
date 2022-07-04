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
            .HasColumnName(nameof(BaseEntity.Id).ToLower())
            .HasColumnType(ColumnTypes.UniqueIdentifier)
            .HasDefaultValueSql(PostgreExtensions.UUIDAlgorithm)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Created)
            .HasColumnType(ColumnTypes.Timestamp)
            .HasColumnName(nameof(BaseEntity.Created).ToLower())
            .ValueGeneratedOnAdd();
            
        builder.Property(e => e.Modified)
            .HasColumnType(ColumnTypes.Timestamp)
            .HasColumnName(nameof(BaseEntity.Modified).ToLower())
            .ValueGeneratedOnUpdate();

        builder.Property(x => x.Name)
            .HasColumnName(nameof(ValueListEntity.Name).ToLower())
            .HasColumnType(ColumnTypes.Varchar)
            .IsRequired()
            .HasMaxLength(150);
        

       
    }
}
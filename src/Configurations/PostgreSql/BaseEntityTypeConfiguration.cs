using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Threenine.Configurations.PostgreSql;
using Threenine.Models;

namespace Threenine.Database.Configuration.PostgreSql;

public abstract class BaseEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name.ToSnakeCase());
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
        
        builder.Property(e => e.Active)
            .HasColumnName(nameof(BaseEntity.Active).ToLower())
            .HasDefaultValue(true)
            .ValueGeneratedOnAdd();
    }
}
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Threenine.Database.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureOwnedTypeColumnNames(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!entityType.IsOwned()) continue;
           
            var ownership = entityType.FindOwnership();

            if (ownership is null) continue;

            var properties = entityType.GetProperties().Where(x => !x.IsShadowProperty());
            
            foreach (var property in properties)
            {
                var tableName = entityType.GetTableName();
                entityType.SetTableName(tableName.ToSnakeCase());

                if (tableName is null) continue;

                var columnName = property.GetColumnName(StoreObjectIdentifier.Table(tableName, null));
                var columnNameDefault = property.GetDefaultColumnName(StoreObjectIdentifier.Table(tableName, null));

                if (columnName is null || columnNameDefault is null) continue;

                if (!columnName.Equals(columnNameDefault)) continue;
                var columnNameBase = property.GetColumnName();
                property.SetColumnName(columnNameBase.ToSnakeCase());
            }
        }
    }
}

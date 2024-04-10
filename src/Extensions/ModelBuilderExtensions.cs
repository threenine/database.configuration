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
            }
        }
    }
}

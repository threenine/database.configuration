using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Threenine.Database.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureOwnedTypeTableName(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!entityType.IsOwned()) continue;
            var tableName = entityType.GetTableName();
            entityType.SetTableName(tableName.ToSnakeCase());
          
        }
    }
}

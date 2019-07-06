using Model.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DapperContext
{
    public class TableInfoProvider<TEntity> where TEntity : BaseEntity
    {
        public static TableInfo GetTableInfo()
        {
            return new TableInfo();
        }

        private string GetKeyName(Type type)
        {
            return type
                .GetProperties()
                .First(x => x.GetCustomAttribute<KeyAttribute>() != null)
                .Name;
        }

        private string GetTableName(Type type)
        {
            return type.GetCustomAttribute<TableAttribute>().Name;
        }

        private string[] GetColumnNames(Type type)
        {
            return type
                .GetProperties()
                .Where(x => x.GetCustomAttribute(typeof(ColumnAttribute)) != null)
                .Select(x => x.Name)
                .ToArray();
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace DapperContext.Information
{
    /// <summary>
    /// Поставщик информации о таблице БД
    /// </summary>
    internal static class TableInfoProvider
    {
        /// <summary>
        /// Вернуть информацию о таблице на основе ассоциированного с ней типа
        /// </summary>
        public static TableInfo GetTableInfo(Type type)
        {
            return new TableInfo
            {
                Name = GetTableName(type),
                PK = GetPK(type),
                Columns = GetColumns(type)
            };
        }

        /// <summary>
        /// Получить имя таблицы
        /// </summary>
        private static string GetTableName(Type type)
        {
            return type.GetCustomAttribute<TableAttribute>().Name;
        }

        /// <summary>
        /// Получить информацию о первичном ключе
        /// </summary>
        private static KeyInfo GetPK(Type type)
        {
            var keyProp = type
                .GetProperties()
                .First(x => x.GetCustomAttribute<KeyAttribute>() != null);

            return new KeyInfo
            {
                Name = keyProp.Name,
                Property = keyProp.Name
            };
        }

        /// <summary>
        /// Получить информацию по колонкам таблицы
        /// </summary>
        private static ColumnInfo[] GetColumns(Type type)
        {
            return type
                .GetProperties()
                .Where(x => x.GetCustomAttribute(typeof(ColumnAttribute)) != null)
                .Select(x => new ColumnInfo
                {
                    Name = x.Name,
                    Property = x.Name
                })
                .ToArray();
        }
    }
}

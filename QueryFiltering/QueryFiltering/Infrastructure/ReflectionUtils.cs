using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace QueryFiltering.Infrastructure
{
    public static class ReflectionUtils
    {
        private static readonly ConcurrentDictionary<string, Type> DynamicTypes = new ConcurrentDictionary<string, Type>();

        private static readonly ModuleBuilder DynamicTypeBuilder = AssemblyBuilder
            .DefineDynamicAssembly(new AssemblyName { Name = "DynamicTypeAssembly" }, AssemblyBuilderAccess.Run)
            .DefineDynamicModule("DynamicTypeModule");

        public static Type CreateDynamicType(IDictionary<string, Type> properties)
        {
            if (properties.Count == 0)
            {
                throw new ArgumentException(nameof(properties));
            }

            return DynamicTypes.GetOrAdd(
                GetKey(properties), 
                key =>
                {
                    var typeBuilder = DynamicTypeBuilder.DefineType(
                        key,
                        TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);

                    foreach (var property in properties)
                    {
                        typeBuilder.DefineField(property.Key, property.Value, FieldAttributes.Public);
                    }

                    return typeBuilder.CreateType();
                });
        }

        private static string GetKey(IDictionary<string, Type> properties)
        {
            return string.Join(", ", properties
                    .OrderBy(x => x.Value)
                    .ThenBy(x => x.Key)
                    .Select(x => $"[{x.Key} : {x.Value}]"));
        }
    }
}

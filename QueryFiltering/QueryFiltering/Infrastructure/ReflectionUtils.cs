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
        private static readonly ConcurrentDictionary<string, Type> AnonymousTypes = new ConcurrentDictionary<string, Type>();

        private static readonly ModuleBuilder AnonymousTypeBuilder = AssemblyBuilder
            .DefineDynamicAssembly(new AssemblyName { Name = "AnonymousTypeAssembly" }, AssemblyBuilderAccess.Run)
            .DefineDynamicModule("AnonymousTypeModule");

        public static Type CreateAnonymousTypeFromSourceType(Type sourceType, ISet<string> propertyNames)
        {
            var existingProperties = sourceType
                .GetCashedProperties()
                .Where(x => propertyNames.Contains(x.Name))
                .ToArray();

            if (existingProperties.Length == 0)
            {
                throw new ArgumentException(nameof(propertyNames));
            }

            var key = $"{sourceType.FullName}:[{string.Join(", ", existingProperties.Select(x => $"{x.Name}: {x.PropertyType}"))}]";

            return AnonymousTypes.GetOrAdd(key, _ => CreateAnonymousType(key, existingProperties));
        }

        private static Type CreateAnonymousType(string name, PropertyInfo[] properties)
        {
            var typeBuilder = AnonymousTypeBuilder.DefineType(name,
                TypeAttributes.Public |
                TypeAttributes.AutoLayout |
                TypeAttributes.AnsiClass |
                TypeAttributes.Class |
                TypeAttributes.Sealed |
                TypeAttributes.BeforeFieldInit
            );

            var parameterNames = properties
                .Select(p => $"<{p.Name}>j__TPar")
                .ToArray();

            var typeParameters = typeBuilder.DefineGenericParameters(parameterNames);

            var fieldBuilders = new FieldBuilder[typeParameters.Length];

            for (int i = 0; i < typeParameters.Length; i++)
            {
                var fieldBuilder = typeBuilder.DefineField(
                    fieldName: $"<{properties[i].Name}>i__Field",
                    type: properties[i].PropertyType,
                    attributes: FieldAttributes.Private | FieldAttributes.InitOnly);

                var propertyBuilder = typeBuilder.DefineProperty(
                    name: properties[i].Name,
                    attributes: PropertyAttributes.None,
                    returnType: properties[i].PropertyType,
                    parameterTypes: Type.EmptyTypes);

                var methodBuilder = typeBuilder.DefineMethod(
                    name: $"get_{properties[i].Name}",
                    attributes: MethodAttributes.PrivateScope | MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName,
                    callingConvention: CallingConventions.Standard | CallingConventions.HasThis,
                    returnType: properties[i].PropertyType,
                    parameterTypes: Type.EmptyTypes
                );

                var methodGenerator = methodBuilder.GetILGenerator();

                methodGenerator.Emit(OpCodes.Ldarg_0);
                methodGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
                methodGenerator.Emit(OpCodes.Ret);

                propertyBuilder.SetGetMethod(methodBuilder);

                fieldBuilders[i] = fieldBuilder;
            }

            var constructorBuilder = typeBuilder.DefineConstructor(
                attributes: MethodAttributes.PrivateScope | MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                callingConvention: CallingConventions.Standard | CallingConventions.HasThis,
                parameterTypes: typeParameters
            );

            for (int i = 0; i < properties.Length; i++)
            {
                constructorBuilder.DefineParameter(
                    iSequence: i + 1,
                    attributes: ParameterAttributes.None,
                    strParamName: properties[i].Name);
            }


            var il = constructorBuilder.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, typeof(object).GetConstructors().Single());

            for (int i = 0; i < fieldBuilders.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_0);

                switch (i)
                {
                    case 0:
                        il.Emit(OpCodes.Ldarg_1);
                        break;
                    case 1:
                        il.Emit(OpCodes.Ldarg_2);
                        break;
                    case 2:
                        il.Emit(OpCodes.Ldarg_3);
                        break;
                    default:
                        il.Emit(OpCodes.Ldarg_S, i + 1);
                        break;
                }

                il.Emit(OpCodes.Stfld, fieldBuilders[i]);
            }

            il.Emit(OpCodes.Ret);

            var typeArguments = properties
                .Select(x => x.PropertyType)
                .ToArray();

            return typeBuilder.CreateType().MakeGenericType(typeArguments);
        }
    }
}

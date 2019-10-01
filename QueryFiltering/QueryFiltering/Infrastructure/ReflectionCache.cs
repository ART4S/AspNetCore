using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryFiltering.Infrastructure
{
    internal static class ReflectionCache
    {
        private static readonly ConcurrentDictionary<string, MethodInfo> Methods = new ConcurrentDictionary<string, MethodInfo>();

        public static MethodInfo Lambda => Methods.GetOrAdd("Lambda",
            k => typeof(Expression)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == "Lambda"));

        public static MethodInfo OrderBy => Methods.GetOrAdd("OrderBy",
            k => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == "OrderBy" && x.GetParameters().Length == 2));

        public static MethodInfo OrderByDescending => Methods.GetOrAdd("OrderByDescending",
            k => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == "OrderByDescending" && x.GetParameters().Length == 2));

        public static MethodInfo ThenBy => Methods.GetOrAdd("ThenBy",
            k => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == "ThenBy" && x.GetParameters().Length == 2));

        public static MethodInfo ThenByDescending => Methods.GetOrAdd("ThenByDescending",
            k => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == "ThenByDescending" && x.GetParameters().Length == 2));

        public static MethodInfo Skip => Methods.GetOrAdd("Skip",
            k => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == "Skip" && x.GetParameters().Length == 2));

        public static MethodInfo Take => Methods.GetOrAdd("Take",
            k => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == "Take" && x.GetParameters().Length == 2));

        public static MethodInfo Where => Methods.GetOrAdd("Where",
            k => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == "Where" && x.GetParameters().Length == 2));
    }
}

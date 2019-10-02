﻿using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace QueryFiltering.Infrastructure
{
    internal static class ReflectionCache
    {
        private static readonly ConcurrentDictionary<string, MethodInfo> Methods = new ConcurrentDictionary<string, MethodInfo>();

        public static MethodInfo Lambda => Methods.GetOrAdd("Lambda",
            n => typeof(Expression)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == n));

        public static MethodInfo OrderBy => Methods.GetOrAdd("OrderBy",
            n => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo OrderByDescending => Methods.GetOrAdd("OrderByDescending",
            n => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo ThenBy => Methods.GetOrAdd("ThenBy",
            n => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo ThenByDescending => Methods.GetOrAdd("ThenByDescending",
            n => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo Skip => Methods.GetOrAdd("Skip",
            n => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo Take => Methods.GetOrAdd("Take",
            n => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo Where => Methods.GetOrAdd("Where",
            n => typeof(Queryable)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(x => x.Name == n && x.GetParameters().Length == 2));

        public static MethodInfo ToUpper => Methods.GetOrAdd("Where",
            n => typeof(string)
                .GetMethods(BindingFlags.Public)
                .First(x => x.Name == n && x.GetParameters().Length == 0));
    }
}

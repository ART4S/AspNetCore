using System;
using System.Linq;
using System.Reflection;

namespace WebFeatures.Common.Extensions
{
    public static class ReflectionExtensions
    {
        public static Assembly GetAssembly(this AppDomain domian, string assemblyName)
        {
            var assembly = domian
                .GetAssemblies()
                .FirstOrDefault(x => x.FullName == assemblyName);

            return assembly;
        }
    }
}

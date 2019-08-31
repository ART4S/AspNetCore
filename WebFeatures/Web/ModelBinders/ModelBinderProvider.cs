//using System;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
//using Web.Infrastructure;

//namespace Web.ModelBinders
//{
//    public class ModelBinderProvider : IModelBinderProvider
//    {
//        public IModelBinder GetBinder(ModelBinderProviderContext context)
//        {
//            if (context == null) throw new ArgumentException(nameof(context));

//            var type = context.Metadata.ModelType;
//            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Id<,>)) return null;

//            var binderType = typeof(ModelBinder<,>).MakeGenericType(type.GetGenericArguments());
//            return (IModelBinder)context.Services.GetRequiredService(binderType);
//        }
//    }
//}
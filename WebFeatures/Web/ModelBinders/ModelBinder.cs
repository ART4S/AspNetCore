//using System;
//using System.ComponentModel;
//using System.Linq;
//using System.Threading.Tasks;
//using Entities.Abstractions;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Web.Infrastructure;

//namespace Web.ModelBinders
//{
//    public class ModelBinder<TEntity, TId> : IModelBinder
//        where TEntity : class, IEntity<TId>
//        where TId : struct
//    {
//        private readonly DbContext _dataContext;
//        private static readonly TypeConverter _valueConverter = TypeDescriptor.GetConverter(typeof(TId));

//        public ModelBinder(DbContext dataContext)
//        {
//            _dataContext = dataContext;
//        }

//        public Task BindModelAsync(ModelBindingContext bindingContext)
//        {
//            if (bindingContext == null) throw new ArgumentException(nameof(bindingContext));

//            var modelName = bindingContext.ModelName;
//            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

//            if (valueProviderResult == ValueProviderResult.None)
//                return Task.CompletedTask;

//            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

//            var value = valueProviderResult.FirstValue;
//            if (value == null)
//                return Task.CompletedTask;

//            if (!_valueConverter.IsValid(value))
//                return Task.CompletedTask;

//            var id = (TId) _valueConverter.ConvertFromString(value);
//            var entity = _dataContext.Set<TEntity>().FirstOrDefault(x => Equals(x.Id, id));

//            if (entity == null)
//            {
//                bindingContext.ModelState.TryAddModelError(modelName, "Запись отсутствует в БД");
//                return Task.CompletedTask;
//            }

//            bindingContext.Result = ModelBindingResult.Success(new Id<TEntity, TId>(entity));
//            return Task.CompletedTask;
//        }
//    }

//    public class IdModelBinder : IModelBinder
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public IdModelBinder(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        public Task BindModelAsync(ModelBindingContext bindingContext)
//        {
//            if (bindingContext == null) throw new ArgumentException(nameof(bindingContext));

//            var type = bindingContext.ModelType;
//            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Id<,>))
//            {
//                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Неверный тип");
//                return Task.CompletedTask;
//            }

//            var binderType = typeof(ModelBinder<,>).MakeGenericType(type.GetGenericArguments());
//            var binderImpl = (IModelBinder)_serviceProvider.GetRequiredService(binderType);

//            return binderImpl.BindModelAsync(bindingContext);
//        }
//    }
//}
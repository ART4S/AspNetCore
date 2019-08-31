//using Entities.Abstractions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json.Converters;
//using Newtonsoft.Json.Serialization;
//using Web.Extensions;

//namespace Web.Infrastructure
//{
//    [JsonConverter(typeof(IdConverter))]
//    public sealed class Id<TEntity, TId>
//        where TEntity : class, IEntity<TId> 
//        where TId : struct
//    {
//        private readonly TEntity _entity;
//        private readonly Func<TEntity> _entityProvider = () => null;

//        public TEntity Entity => _entity ?? _entityProvider();

//        public Id(TEntity entity)
//        {
//            _entity = entity;
//        }

//        public Id(Func<TEntity> entityProvider)
//        {
//            _entityProvider = entityProvider;
//        }
//    }

//    public class IdConverter : JsonConverter
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public IdConverter(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        public override bool CanConvert(Type objectType) => objectType.IsGenericType && objectType.GetGenericTypeDefinition() != typeof(Id<,>);

//        public override bool CanRead => true;

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            if (!CanConvert(objectType))
//                return null;

//            if (!(reader.Value is string value) || value.IsNullOrEmpty())
//                return null;

//            var converter = CreateConverterImpl(objectType);
//            return converter.ReadJson(reader, objectType, existingValue, serializer);
//        }

//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            if (value is null) return;

//            var type = value.GetType();
//            if (!CanConvert(type)) return;

//            var converter = CreateConverterImpl(type);
//            converter.WriteJson(writer, value, serializer);
//        }

//        private JsonConverter CreateConverterImpl(Type type)
//        {
//            var converterType = typeof(ConverterImpl<,>).MakeGenericType(type.GetGenericArguments());
//            var converter = (JsonConverter) _serviceProvider.GetRequiredService(converterType);

//            return converter;
//        }

//        private class ConverterImpl<TEntity, TId> : JsonConverter
//            where TEntity : class, IEntity<TId>
//            where TId : struct
//        {
//            private readonly DbContext _context;
//            private static readonly TypeConverter _valueConverter = TypeDescriptor.GetConverter(typeof(TId));

//            public ConverterImpl(DbContext context)
//            {
//                _context = context;
//            }

//            public override bool CanConvert(Type objectType) => true;

//            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//            {
//                var id = ((Id<TEntity, TId>) value).Entity.Id;
//                WriteJson(writer, id, serializer);
//            }

//            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//            {
//                if (!_valueConverter.IsValid(reader.Value)) return null;

//                var id = (TId)_valueConverter.ConvertFrom(reader.Value);
//                var entity = _context.Set<TEntity>().FirstOrDefault(x => Equals(x.Id, id));

//                if (entity == null) return null;

//                return new Id<TEntity, TId>(entity);
//            }
//        }
//    }

//    public class ValidatableJsonConverter : JsonConverter
//    {
//        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//        }

//        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        {
//            return null;
//        }

//        public override bool CanConvert(Type objectType) => true;
//    }
//}
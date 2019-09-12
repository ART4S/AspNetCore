using System;
using System.ComponentModel.DataAnnotations;
using WebFeatures.Application.Infrastructure.Validation;
using WebFeatures.Common.Extensions;

namespace WebFeatures.WebApi.Attributes
{
    /// <summary>
    /// Атрибут для проверки на null или default значение
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class RequiredAttribute : ValidationAttribute
    {
        private readonly bool _allowEmptyStrings;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public RequiredAttribute(bool allowEmptyStrings = false) : base(ValidationErrorMessages.NotEmpty)
        {
            _allowEmptyStrings = allowEmptyStrings;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var type = value.GetType();

            if (type.IsClass)
            {
                if (!_allowEmptyStrings)
                {
                    if (value is string str && str.IsNullOrWhiteSpace())
                    {
                        return false;
                    }
                }

                return true;
            }

            if (type.IsValueType)
            {
                var defaultValue = Activator.CreateInstance(type);
                return !Equals(value, defaultValue);
            }

            return false;
        }
    }
}

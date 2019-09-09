using System;
using System.ComponentModel.DataAnnotations;
using WebFeatures.Application.Infrastructure.Validation;

namespace WebFeatures.WebApi.Attributes
{
    /// <summary>
    /// Атрибут для проверки на null значение
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class RequiredAttribute : ValidationAttribute
    {
        private readonly System.ComponentModel.DataAnnotations.RequiredAttribute _attr;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public RequiredAttribute(bool allowEmptyStrings = false)
        {
            _attr = new System.ComponentModel.DataAnnotations.RequiredAttribute
            {
                AllowEmptyStrings = allowEmptyStrings,
                ErrorMessage = ValidationErrorMessages.NotEmpty
            };
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public override bool IsValid(object value)
        {
            return _attr.IsValid(value);
        }
    }
}

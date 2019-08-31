using System;

namespace Web.Attributes
{
    /// <summary>
    /// Атрибут для проверки на null значение
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class RequiredAttribute : ValidationAttribute
    {
        private readonly System.ComponentModel.DataAnnotations.RequiredAttribute _attr;

        public RequiredAttribute(bool allowEmptyStrings = false)
        {
            _attr = new System.ComponentModel.DataAnnotations.RequiredAttribute
            {
                AllowEmptyStrings = allowEmptyStrings,
                ErrorMessage = "Необходимо заполнить"
            };
        }
    }
}

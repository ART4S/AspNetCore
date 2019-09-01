using System;

namespace Web.Validation.Attributes
{
    /// <summary>
    /// Атрибут для проверки на null значение
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class RequiredAttribute : ValidateModelStateAttribute
    {
        private readonly System.ComponentModel.DataAnnotations.RequiredAttribute _attr;

        /// <summary>
        /// 
        /// </summary>
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

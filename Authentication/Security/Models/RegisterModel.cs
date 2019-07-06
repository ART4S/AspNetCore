using System.ComponentModel.DataAnnotations;

namespace Security.Models
{
    /// <summary>
    /// Форма для регистрации
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

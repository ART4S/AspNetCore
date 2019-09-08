using WebFeatures.Entities.Abstractions;

namespace WebFeatures.Entities.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity<int>
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswordHash { get; set; }
    }
}

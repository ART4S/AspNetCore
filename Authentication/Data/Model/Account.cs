namespace Data.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Account : BaseEntity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Хэш-пароля
        /// </summary>
        public string PasswordHash { get; set; }
    }
}

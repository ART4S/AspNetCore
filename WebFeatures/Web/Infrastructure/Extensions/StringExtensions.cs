namespace Web.Infrastructure.Extensions
{
    /// <summary>
    /// Расширения для работы со строками
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Проверить на null или пустую строку
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Проверить на null или строку состоящую только из пробельных символов
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}

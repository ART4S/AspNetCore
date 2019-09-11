using System.Collections.Generic;
using WebFeatures.Domian.Infrastructure;

namespace WebFeatures.Domian.ValueObjects
{
    /// <summary>
    /// Контактная информация пользователя
    /// </summary>
    public class ContactDetails : ValueObject
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Email;
            yield return PhoneNumber;
        }
    }
}

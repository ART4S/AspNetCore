using System.Collections.Generic;
using WebFeatures.Domian.Infrastructure;

namespace WebFeatures.Domian.ValueObjects
{
    /// <summary>
    /// Контактная информация пользователя
    /// </summary>
    public class ContactDetails : ValueObject
    {
        private ContactDetails() { }

        public static ContactDetails Create(string email, string phoneNumber)
        {
            return new ContactDetails
            {
                Email = email,
                PhoneNumber = phoneNumber
            };
        }

        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Email;
            yield return PhoneNumber;
        }
    }
}

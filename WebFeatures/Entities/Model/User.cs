using Entities.Abstractions;

namespace Entities.Model
{
    public class User : BaseEntity<int>
    {
        public string Name { get; set; }

        public string PasswordHash { get; set; }
    }
}

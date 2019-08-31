using System.ComponentModel.DataAnnotations;
using Web.Abstractions;
using Web.Infrastructure;

namespace Web.Features.Registration
{
    public class RegisterUser : ICommand<Result>
    {
        //[RegularExpression(@"^(?=[A-Za-z0-9])(?!.*[._()\[\]-]{2})[A-Za-z0-9._()\[\]-]{3,15}$", ErrorMessage = "Имя должно начинаться с буквы или числа, не должно содержать 2-х последовательных символов, должно содержать от 3-х до 15-и разрешенных символов")]
        public string Name { get; set; }

        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Херовый пароль")]
        public string Password { get; set; }
    }
}

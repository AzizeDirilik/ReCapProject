using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Password).MinimumLength(8).WithMessage("Sifre en az 8 karakter olmalidir.");
            RuleFor(u => u.Password).MaximumLength(20).WithMessage("Sifre en fazla 20 karakter olmalidir.");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Gecerli bir e-mail girilmelidir.");
            RuleFor(u => u.Email).Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");


        }
    }
}

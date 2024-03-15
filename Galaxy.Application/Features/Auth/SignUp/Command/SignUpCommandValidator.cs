using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galaxy.Domain.Constants;

namespace Galaxy.Application.Features.Auth.SignUp.Command
{
    public class SignUpCommandValidator:AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
            RuleFor(x => x.PhoneNumber).MinimumLength(11);
            RuleFor(x=>x.PhoneNumber).MaximumLength(14);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Gender).Must(x => x == Gander.Male || x == Gander.Female).WithMessage("Gender is not defined");
        }
    }
}

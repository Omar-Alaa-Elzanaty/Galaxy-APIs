using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Galaxy.Domain.Constants;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Auth.SignUp.Command
{
    public class SignUpCommandValidator:AbstractValidator<SignUpCommand>
    {
        private readonly IStringLocalizer<SignUpCommandValidator> _localization;
        public SignUpCommandValidator(IStringLocalizer<SignUpCommandValidator> localization)
        {
            _localization = localization;
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.PhoneNumber).MinimumLength(11).WithMessage("lenght not be less than 11 characters");
            RuleFor(x => x.PhoneNumber).MaximumLength(14).WithMessage("Length mus not be more than 14 charachters");
            RuleFor(x => x.Email).EmailAddress().WithMessage(_localization["EmailIsNotValidExpression"].Value);
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Should not be empty");
            RuleFor(x => x.Gender).Must(x => x == Gander.Male || x == Gander.Female).WithMessage(_localization["GanderException"].Value);
        }
    }
}

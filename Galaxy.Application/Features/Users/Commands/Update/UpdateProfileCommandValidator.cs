using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Users.Commands.Update
{
    public class UpdateProfileCommandValidator:AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileCommandValidator(IStringLocalizer<UpdateProfileCommandValidator> localization)
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage(localization["EmailIsNotValidExpression"].Value);
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(11).WithMessage("Phone number must not be less than 11 digit");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("username required");
            RuleFor(x => x.EmployeeId).NotEmpty().MaximumLength(10).WithMessage("employee id length must not be more than 10 charcters");
        }
    }
}

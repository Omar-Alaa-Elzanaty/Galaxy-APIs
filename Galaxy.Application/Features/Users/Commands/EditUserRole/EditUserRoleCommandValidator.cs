using FluentValidation;
using Galaxy.Domain.Constants;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Users.Commands.EditUserRole
{
    public class EditUserRoleCommandValidator : AbstractValidator<EditUserRoleCommand>
    {
        private readonly IStringLocalizer<EditUserRoleCommandValidator> _localization;
        public EditUserRoleCommandValidator(IStringLocalizer<EditUserRoleCommandValidator> localization)
        {
            _localization = localization;
            RuleFor(x => x.roleName).Must(x => x == Roles.MANAGER || x == Roles.OWNER || x == Roles.SALE)
                                    .WithMessage(_localization["RoleNotExist"].Value);
            RuleFor(x => x.userId).NotNull().NotEmpty().WithMessage("user id required");
        }
    }
}

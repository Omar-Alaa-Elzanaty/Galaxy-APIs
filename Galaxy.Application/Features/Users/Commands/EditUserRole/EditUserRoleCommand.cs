using Galaxy.Domain.Identity;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Users.Commands.EditUserRole
{
    public record EditUserRoleCommand : IRequest<Response>
    {
        public string userId { get; set; }
        public string roleName { get; set; }
    }

    internal class EditUserRoleCommandHandler : IRequestHandler<EditUserRoleCommand, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<EditUserRoleCommandHandler> _localization;

        public EditUserRoleCommandHandler(
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<EditUserRoleCommandHandler> localization,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _localization = localization;
            _roleManager = roleManager;
        }

        public async Task<Response> Handle(EditUserRoleCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.userId);

            if (user is null)
            {
                return await Response.FailureAsync(_localization["UserNotFound"].Value);
            }

            if (!await _roleManager.RoleExistsAsync(command.roleName))
            {
                return await Response.FailureAsync(_localization["RoleNotExist"].Value);
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            result = await _userManager.AddToRoleAsync(user, command.roleName);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            return await Response.SuccessAsync(_localization["Success"].Value);

        }
    }
}

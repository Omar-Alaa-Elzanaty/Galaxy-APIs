using FluentValidation;
using Galaxy.Domain.Constants;
using Galaxy.Domain.Identity;
using Galaxy.Shared;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Auth.SignUp.Command
{
    public record SignUpCommand : IRequest<Response>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Gander Gender { get; set; }
        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public string Role { get; set; }
    }
    internal class SignUpCommandHandler : IRequestHandler<SignUpCommand, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<SignUpCommandHandler> _localization;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IValidator<SignUpCommand> _validator;
        private readonly IMapper _mapper;
        public SignUpCommandHandler(
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<SignUpCommandHandler> localization,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            IValidator<SignUpCommand> validator)
        {
            _userManager = userManager;
            _localization = localization;
            _roleManager = roleManager;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Response> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return await Response.FailureAsync(validationResult.Errors.First().ErrorMessage);
            }


            if (await _userManager.FindByNameAsync(command.UserName) is not null)
            {
                return await Response.FailureAsync(_localization["UserNameExist"].Value);
            }

            if (await _userManager.FindByEmailAsync(command.Email) is not null)
            {
                return await Response.FailureAsync(_localization["EmailExist"].Value);
            }

            if (await _userManager.Users.AnyAsync(x => x.PhoneNumber == command.PhoneNumber))
            {
                return await Response.FailureAsync(_localization["PhoneNumberExist"].Value);
            }

            if (!await _roleManager.RoleExistsAsync(command.Role))
            {
                return await Response.FailureAsync(_localization["RoleNotExist"].Value);
            }

            if (await _userManager.Users.AnyAsync(x => x.EmployeeId == command.EmployeeId))
            {
                return await Response.FailureAsync(_localization["EmployeeIdExist"].Value);
            }

            var user = _mapper.Map<ApplicationUser>(command);

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            result = await _userManager.AddToRoleAsync(user, command.Role);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            return await Response.SuccessAsync(user.Id);
        }
    }
}

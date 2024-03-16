﻿using FluentValidation;
using Galaxy.Domain.Constants;
using Galaxy.Domain.Identity;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.VisualBasic;

namespace Galaxy.Application.Features.Users.Commands.Update
{
    public record UpdateProfileCommand : IRequest<Response>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public Gander Gander { get; set; }
    }

    internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<UpdateProfileCommandHandler> _localization;
        private readonly IValidator<UpdateProfileCommand> _validator;

        public UpdateProfileCommandHandler(
            IStringLocalizer<UpdateProfileCommandHandler> localization,
            UserManager<ApplicationUser> userManager,
            IValidator<UpdateProfileCommand> validator)
        {
            _localization = localization;
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<Response> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var validatorResult = await _validator.ValidateAsync(command);

            if(!validatorResult.IsValid)
            {
                return await Response.FailureAsync(validatorResult.Errors.First().ErrorMessage);
            }

            var user = await _userManager.FindByIdAsync(command.Id);

            if (user is null)
            {
                return await Response.FailureAsync(_localization["UserNotFound"].Value);
            }

            user.UserName = command.UserName;
            user.PhoneNumber = command.PhoneNumber;
            user.Email = command.Email;
            user.Gander = command.Gander;
            user.EmployeeId = command.EmployeeId;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return await Response.FailureAsync(result.Errors.First().Description);
            }

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}

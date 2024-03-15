using Galaxy.Domain.Identity;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Users.Commands.Delete
{
    public record DeleteAccountCommand : IRequest<Response>
    {
        public DeleteAccountCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }

    internal class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<DeleteAccountCommandHandler> _localization;
        public DeleteAccountCommandHandler(
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<DeleteAccountCommandHandler> localization)
        {
            _userManager = userManager;
            _localization = localization;
        }

        public async Task<Response> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.Id);

            if (user is null)
            {
                return await Response.FailureAsync(_localization["UserNotFound"].Value);
            }

            await _userManager.DeleteAsync(user);

            return await Response.SuccessAsync(_localization["Success"].Value);
        }
    }
}

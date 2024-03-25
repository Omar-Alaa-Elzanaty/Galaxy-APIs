using System.Net;
using Galaxy.Domain.Identity;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Users.Queries.GetPasswordByUserId
{
    public record GetPasswordByUserIdQuery : IRequest<Response>
    {
        public string userId { get; set; }
        public string password { get; set; }
    }

    internal class GetPasswordByUserIdQueryHandler : IRequestHandler<GetPasswordByUserIdQuery, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<GetPasswordByUserIdQueryHandler> _localization;
        public GetPasswordByUserIdQueryHandler(
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<GetPasswordByUserIdQueryHandler> localization)
        {
            _userManager = userManager;
            _localization = localization;
        }

        public async Task<Response> Handle(GetPasswordByUserIdQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(query.userId);

            if (user == null)
            {
                return await Response.FailureAsync(_localization["UserNotFound"].Value);
            }

            if (!await _userManager.CheckPasswordAsync(user, query.password))
            {
                return await Response.FailureAsync(_localization["Unauthorize"].Value, HttpStatusCode.Unauthorized);
            }

            return await Response.SuccessAsync(data: user.Password);

        }
    }
}

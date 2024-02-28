using Galaxy.Application.Interfaces.Repositories;
using Galaxy.Domain.Identity;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Pharamcy.Application.Interfaces.Auth;

namespace Galaxy.Application.Features.Auth.Login.LoginQueries
{
    public record LoginQuery : IRequest<Response>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    internal class LoginQueryHandler : IRequestHandler<LoginQuery, Response>
    {
        public readonly IUnitOfWork UnitOfWork;
        public readonly UserManager<ApplicationUser> _userManger;
        public readonly IAuthServices _authServices;
        public readonly IStringLocalizer<LoginQueryHandler> _localization;

        public LoginQueryHandler(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManger,
            IStringLocalizer<LoginQueryHandler> localization,
            IAuthServices authServices)
        {
            UnitOfWork = unitOfWork;
            _userManger = userManger;
            _localization = localization;
            _authServices = authServices;
        }

        public async Task<Response> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManger.FindByNameAsync(query.UserName);

            if (user == null || await _userManger.CheckPasswordAsync(user, query.Password))
            {
                return await Response.FailureAsync(_localization["InvalidLogin"].Value);
            }

            var role = _userManger.GetRolesAsync(user).Result.First();
            var token = _authServices.GenerateToken(user, role);

            var response = new LoginQueryDto()
            {
                Token = token
            };

            return await Response.SuccessAsync(response, _localization["Success"]);
        }
    }

}

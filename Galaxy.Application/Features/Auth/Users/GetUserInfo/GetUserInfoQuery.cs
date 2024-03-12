using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Domain.Identity;
using Galaxy.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Galaxy.Application.Features.Auth.Users.GetUserInfo
{
    public record GetUserInfoQuery:IRequest<Response>
    {
        public string Id { get; set; }

        public GetUserInfoQuery(string id)
        {
            Id = id;
        }
    }

    internal class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<GetUserInfoQueryHandler> _localization;

        public GetUserInfoQueryHandler(
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<GetUserInfoQueryHandler> localization)
        {
            _userManager = userManager;
            _localization = localization;
        }

        public async Task<Response> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(query.Id);

            if(entity is null)
            {
                return await Response.FailureAsync(_localization["UserNotFound"].Value);
            }

            var user = entity.Adapt<GetUserInfoQueryDto>();

            return await Response.SuccessAsync(user);
        }
    }
}

using Galaxy.Application.Extention;
using Galaxy.Domain.Constants;
using Galaxy.Domain.Identity;
using Galaxy.Shared;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Galaxy.Application.Features.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery : PaginatedRequest, IRequest<PaginatedResponse<GetAllUsersQueryDto>>
    {
        public string Id { get; set; }
        public Gander? Gander { get; set; }
    }

    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, PaginatedResponse<GetAllUsersQueryDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GetAllUsersQueryHandler(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PaginatedResponse<GetAllUsersQueryDto>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var entities = _userManager.Users.Where(x => x.Id != query.Id);

            if (query.KeyWord is not null)
            {
                entities = entities.Where(x => x.UserName!.Contains(query.KeyWord)
                                            || x.Name.Contains(query.KeyWord)
                                            || x.PhoneNumber!.Contains(query.KeyWord)
                                            || x.EmployeeId.Contains(query.KeyWord));
            }

            if (query.Gander is not null)
            {
                entities = entities.Where(x => x.Gander == query.Gander);
            }

            var users = await entities.ProjectToType<GetAllUsersQueryDto>()
                .ToPaginatedListAsync(query.PageNumber, query.PageSize, cancellationToken);

            foreach(var user in users.Data)
            {
                user.Role = _userManager.GetRolesAsync(entities.First(x => x.Id == user.Id)).Result.First();
            }

            return users;
        }
    }
}

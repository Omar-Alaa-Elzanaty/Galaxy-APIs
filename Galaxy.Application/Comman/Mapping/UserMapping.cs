using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Features.Users.Commands.Update;
using Galaxy.Application.Features.Users.Queries.GetAllUsers;
using Galaxy.Domain.Identity;
using Galaxy.Shared;
using Mapster;

namespace Galaxy.Application.Comman.Mapping
{
    public class UserMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ApplicationUser, GetAllUsersQueryDto>();
        }
    }
}

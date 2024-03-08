using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Features.Auth.SignUp.Command;
using Galaxy.Domain.Identity;
using Mapster;

namespace Galaxy.Application.Comman.Mapping
{
    public class SignUpMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SignUpCommand, ApplicationUser>();
        }
    }
}

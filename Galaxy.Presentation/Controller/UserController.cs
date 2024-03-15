using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Galaxy.Application.Features.Users.Commands.Delete;
using Galaxy.Application.Features.Users.Commands.EditUserRole;
using Galaxy.Application.Features.Users.Commands.Update;
using Galaxy.Application.Features.Users.Queries.GetAllUsers;
using Galaxy.Application.Features.Users.Queries.GetPasswordByUserId;
using Galaxy.Application.Features.Users.Queries.GetUserInfo;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController:ApiControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator meidator)
        {
            _mediator = meidator;
        }

        [HttpGet("users")]
        [Authorize(Roles = Roles.OWNER)]
        public async Task<ActionResult<GetAllUsersQueryDto>> GetAllUsers([FromQuery] GetAllUsersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("userPassword")]
        [Authorize(Roles = Roles.OWNER)]
        public async Task<ActionResult<string>> GetUserPassword([FromQuery]GetPasswordByUserIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("userInfo/{id}")]
        [Authorize]
        public async Task<ActionResult<GetUserInfoQueryDto>> GetUserInfo([FromRoute]string id)
        {
            return Ok(await _mediator.Send(new GetUserInfoQuery(id)));
        }

        [HttpDelete("deleteAccount/{id}")]
        [Authorize(Roles = Roles.OWNER)]
        public async Task<ActionResult<string>> DeleteUser([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new DeleteAccountCommand(id)));
        }

        [HttpPut("editUserRole")]
        [Authorize(Roles = Roles.OWNER)]
        public async Task<ActionResult<string>> EditUserRole([FromBody] EditUserRoleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("UpdateProfile")]
        [Authorize]
        public async Task<ActionResult<string>> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

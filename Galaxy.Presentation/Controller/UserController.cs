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
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator meidator)
        {
            _mediator = meidator;
        }

        [Authorize(Roles = $"{Roles.MANAGER},{Roles.OWNER}")]
        [HttpGet("users")]
        public async Task<ActionResult<GetAllUsersQueryDto>> GetAllUsers([FromQuery] GetAllUsersQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = $"{Roles.MANAGER},{Roles.OWNER}")]
        [HttpGet("userPassword")]
        public async Task<ActionResult<string>> GetUserPassword([FromBody] GetPasswordByUserIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [Authorize(Roles = $"{Roles.MANAGER},{Roles.OWNER}")]
        [HttpGet("userInfo/{id}")]
        public async Task<ActionResult<GetUserInfoQueryDto>> GetUserInfo([FromRoute] string id)
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

        [Authorize(Roles = $"{Roles.MANAGER},{Roles.OWNER}")]
        [HttpPut("UpdateProfile")]
        public async Task<ActionResult<string>> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

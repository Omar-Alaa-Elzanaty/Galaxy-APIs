using Galaxy.Application.Features.Auth.Login.LoginQueries;
using Galaxy.Application.Features.Auth.SignUp.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<LoginQueryDto>> Login(LoginQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult<int>>CreateAccount([FromForm]SignUpCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}

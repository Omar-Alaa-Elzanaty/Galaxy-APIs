using Galaxy.Application.Features.Customers.Querires.GetAllCustomers;
using Galaxy.Application.Features.Customers.Querires.GetCustomerById;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.OWNER)]
    public class CustomerController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllCustomersQueryDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCustomersQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerByIdQueryDto>> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetCustomerByIdQuery(id)));
        }
    }
}

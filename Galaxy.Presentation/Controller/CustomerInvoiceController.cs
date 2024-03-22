using Galaxy.Application.Features.CustomerInvoices.commands.Create;
using Galaxy.Application.Features.CustomerInvoices.Queries.GetAllCustomerInvoiceByCustomerId;
using Galaxy.Application.Features.CustomerInvoices.Queries.GetCustomerInvoiceById;
using Galaxy.Domain.Constants;
using Galaxy.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize()]
    public class CustomerInvoiceController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerInvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create(CreateCustomerInvoiceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<GetAllcustomerInvoiceByCustomerIdQueryDto>>> GetAllByCustomerId([FromQuery] GetAllcustomerInvoiceByCustomerIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerInvoiceByIdQuery>> Get(int id)
        {
            return Ok(await _mediator.Send(new GetCustomerInvoiceByIdQuery(id)));
        }
    }
}

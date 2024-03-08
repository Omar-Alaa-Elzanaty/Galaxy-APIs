using Galaxy.Application.Features.CustomerInvoices.commands.Create;
using Galaxy.Application.Features.CustomerInvoices.Queries.GetAllCustomerInvoiceByCustomerId;
using Galaxy.Application.Features.CustomerInvoices.Queries.GetCustomerInvoiceById;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.OWNER)]
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

        [HttpGet("customerInvoices/{customerId}")]
        public async Task<ActionResult<List<GetAllcustomerInvoiceByCustomerIdQuery>>> GetAllByCustomerId(int customerId)
        {
            return Ok(await _mediator.Send(new GetAllcustomerInvoiceByCustomerIdQuery(customerId)));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerInvoiceByIdQuery>> Get(int id)
        {
            return Ok(await _mediator.Send(new GetCustomerInvoiceByIdQuery(id)));
        }
    }
}

using Galaxy.Application.Features.CustomerInvoices.commands.Create;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.OWNER)]
    public class CustomerInvoiceController:ApiControllerBase
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
    }
}

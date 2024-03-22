using Galaxy.Application.Features.SupplierInvoices.Queries.GetPurchaseInvoiceById;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [Authorize(Roles =Roles.OWNER)]
    public class PurchaseInvoiceController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseInvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<GetPurchaseInvoiceByIdDto>>>Get(int id)
        {
            return Ok(await _mediator.Send(new GetPurchaseInvoiceByIdQuery(id)));
        }
    }
}

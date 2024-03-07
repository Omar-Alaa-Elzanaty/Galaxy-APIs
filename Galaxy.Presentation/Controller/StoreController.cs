using System.Runtime.InteropServices;
using Galaxy.Application.Features.Stores.Commands.TransferItem;
using Galaxy.Application.Features.Stores.Queries.GetProductByBarCode;
using Galaxy.Application.Features.SupplierInvoices.Create;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.OWNER)]
    public class StoreController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("checkBarCode/{barCode}")]
        public async Task<ActionResult<bool>>CheckBarCode(string barCode)
        {
            return Ok(await _mediator.Send(new CheckItemInStockByBarcode(barCode)));
        }

        [HttpPut("changeProductsPlace")]
        public async Task<ActionResult<string>> MoveToSale(TransferItemCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddItems(CreateSupplierInvoice command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}

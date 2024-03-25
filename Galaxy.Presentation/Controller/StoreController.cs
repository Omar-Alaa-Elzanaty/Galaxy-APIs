using System.Security.Claims;
using Galaxy.Application.Features.Stores.Commands.TransferItem;
using Galaxy.Application.Features.Stores.Queries.CheckItemByBarCode;
using Galaxy.Application.Features.Stores.Queries.GetLowInventories;
using Galaxy.Application.Features.SupplierInvoices.Commands.Create;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("checkBarCode/{barCode}")]
        public async Task<ActionResult<bool>> CheckBarCode(string barCode)
        {
            return Ok(await _mediator.Send(new CheckItemInStockByBarcodeQuery(barCode)));
        }

        [Authorize]
        [HttpPut("changeProductsPlace")]
        public async Task<ActionResult<string>> MoveToSale(TransferItemCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId.IsNullOrEmpty())
            {
                return Unauthorized("un Authorized user");
            }

            command.UserId = userId;

            return Ok(await _mediator.Send(command));
        }

        [Authorize(Roles = $"{Roles.MANAGER},{Roles.OWNER}")]
        [HttpPost]
        public async Task<ActionResult<string>> AddItems(CreateSupplierInvoiceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [Authorize]
        [HttpGet("needToImport")]
        public async Task<ActionResult<List<GetLowInventoriesQueryDto>>> LowInventory()
        {
            return Ok(await _mediator.Send(new GetLowInventoriesQuery()));
        }

    }
}

using Galaxy.Application.Features.Suppliers.Commands.Create;
using Galaxy.Application.Features.Suppliers.Queries.GetAllLatestPruchases;
using Galaxy.Application.Features.Suppliers.Queries.GetAllSuppliers;
using Galaxy.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxy.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.OWNER)]
    public class Suppliercontroller : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public Suppliercontroller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromForm] CreateSupplierCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<GetAllSupplierQueryDto>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllSupplierQuery()));
        }

        [HttpGet("latestPurchases")]
        public async Task<ActionResult<GetAllLatestPruchasesQueryDto>> GetallLatestPurchases()
        {
            return Ok(await _mediator.Send(new GetAllLatestSupplierPruchasesQuery()));
        }

    }
}

using Galaxy.Application.Features.Suppliers.Commands.Create;
using Galaxy.Application.Features.Suppliers.Commands.Delete;
using Galaxy.Application.Features.Suppliers.Commands.Update;
using Galaxy.Application.Features.Suppliers.Queries.GetAllLatestPruchases;
using Galaxy.Application.Features.Suppliers.Queries.GetAllSuppliers;
using Galaxy.Application.Features.Suppliers.Queries.GetSupplierById;
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
        public async Task<ActionResult<PaginatedResponse<GetAllSupplierQueryDto>>> GetAll([FromQuery] GetAllSupplierQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("latestPurchases")]
        public async Task<ActionResult<PaginatedResponse<GetAllLatestPruchasesQueryDto>>> GetallLatestPurchases([FromQuery] GetAllLatestSupplierPruchasesQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetSupplierByIdQueryDto>>Get(int id)
        {
            return Ok(await _mediator.Send(new GetSupplierByIdQuery(id)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>>Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteSupplierCommandByIdCommand(id)));
        }

        [HttpPut]
        public async Task<ActionResult<string>>Update([FromForm]UpdateSupplierCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
